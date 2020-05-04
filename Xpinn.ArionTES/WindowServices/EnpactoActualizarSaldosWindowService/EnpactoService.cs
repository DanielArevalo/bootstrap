using EnpactoActualizarSaldosWindowService.Clases_Enpacto;
using EnpactoWindowService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Web;
using Xpinn.TarjetaDebito.Entities;
using Xpinn.TarjetaDebito.Services;
using Xpinn.Util;
using System.Collections.Specialized;
using Xpinn.Comun.Entities;
using Xpinn.Comun.Services;



namespace EnpactoActualizarSaldosWindowService
{
    public partial class EnpactoService : ServiceBase
    {

        CuentaService _cuentaService = new CuentaService();
        Usuario _usuario;
        Timer _timer;
        int tipoOpe = 124;
        DateTime FechaProceso = DateTime.Now;
        public static string gFormatoFecha = "dd/MM/yyyy";
        public static string gSeparadorMiles = ".";
        readonly string _strQuote = Encoding.ASCII.GetString(new byte[] { 34 });   // Es usada para colocar comilla doble dentro de las variables tipo string
        string _strHost = "";
        string _convenio = "";
        string _entidad = "";
        readonly string _llave = "0123456789ABCDEFFEDCBA9876543210";
        readonly string _vector = "00000000000000000000000000000000";
        readonly string _padding = "4";
        readonly string _urlArchivoLog = @"C:\Publica\LogEnpacto\LogActualizarSaldosEjecutado.txt";
        readonly int _timeout = 0;
        string HostAppliance = "";
        string usuarioAppliance = "";
        string claveAppliance = "";



        public EnpactoService()
        {
            InitializeComponent();
            // Cargar el dato del convenio
            try
            {
                // Determinar datos del convenio
                EstablecerConvenio();
                EscribirLog(": Se Inicializa el Windows Services Actualización. Convenio:" + _convenio + " Host:" + _strHost + " Entidad:" + _entidad);
                email(": Se Inicializa el Windows Services.Convenio:" + _convenio + " Host: " + _strHost, false);
                EjecutarGestionMovimientos(0);
            }
            catch
            { }
        }
        protected void EstablecerConvenio()
        {
            try
            {
                // Determinar el usuario de conexión
                ConexionWinSer conexion = new ConexionWinSer();
                Usuario usuario;
                String pError = "";
                usuario = conexion.DeterminarUsuario(ref pError);
                if (usuario == null)
                {
                    email("¡ALERTA!  Error en la gestión de los saldos se recomienda revisar el error generado en el servicio de Actualizar Saldos, Error al determinar el usuario de base de datos. Error:" + pError, true);
                    EscribirLog(": Error al determinar el usuario de base de datos. Error:" + pError);
                    return;
                }
                try
                {
                    _usuario = usuario;

                    TarjetaConvenioService convenioServicio = new TarjetaConvenioService();
                    List<TarjetaConvenio> lsttarjetaConvenio = convenioServicio.ListarTarjetaConvenio(new TarjetaConvenio(), usuario);

                    if (lsttarjetaConvenio != null && lsttarjetaConvenio.Count > 0 && lsttarjetaConvenio[0] != null)
                    {
                        _convenio = lsttarjetaConvenio[0].codigo_bin;
                        _strHost = lsttarjetaConvenio[0].ip_switch;
                        _entidad = lsttarjetaConvenio[0].encargado;
                        HostAppliance = lsttarjetaConvenio[0].ip_appliance;
                        usuarioAppliance = lsttarjetaConvenio[0].usuario_appliance;
                        claveAppliance = lsttarjetaConvenio[0].usuario_appliance;
                    }
                }
                catch (Exception ex)
                {
                    email("¡ALERTA!  Error en la gestión de los saldos se recomienda revisar el error generado en el servicio de Actualizar Saldos, Error al determinar convenio." + ex.Message, true);
                    EscribirLog(": Error al determinar convenio." + ex.Message);
                }
            }
            catch (Exception ex)
            {
                email("¡ALERTA!  Error en la gestión de los saldos se recomienda revisar el error generado en el servicio de Actualizar Saldos, Error al determinar el usuario " + ex.Message, true);
                EscribirLog(": Error al determinar el usuario " + ex.Message);
            }
        }
        protected override void OnStart(string[] args)
        {
            _timer = new Timer(TimeSpan.FromHours(24).TotalMilliseconds); // every 24 hours
            _timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            _timer.AutoReset = true;
            _timer.Start();
        }
        protected override void OnStop()
        {
            _timer.Dispose();
        }
        bool EscribirLog(string pMensaje)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(_urlArchivoLog + (_entidad == null ? "" : _entidad) + ".txt", true))
                {
                    string mesageParaFormatear = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + pMensaje;
                    streamWriter.WriteLine(mesageParaFormatear);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        void timer_Elapsed(object sender, EventArgs e)
        {
            try
            {
                EscribirLog(": Se ejecutó el Windows Services");
                EjecutarGestionMovimientos(0);
            }
            catch (Exception ex)
            {
                email("¡ALERTA!  Error en la gestión de los saldos se recomienda revisar el error generado en el servicio de Actualizar Saldos, con un error: " + ex, true);
                EscribirLog(": Error" + ex.Message);
            }
        }

        void EjecutarGestionMovimientos(int ptipo_convenio)
        {
            bool Aplicofinan = false;
            List<archivoSIC> LstMovimientosEnpacto = new List<archivoSIC>();

            //Establecemos la fecha de Aplicar los Movimientos
            //Validamos exista el proceso contable para realizar el proceso
            if (ValidarProcesoContable(FechaProceso, tipoOpe) == false)
            {
                email("¡ALERTA!  Error en la gestión de los saldos se recomienda revisar el error generado en el servicio de Actualizar Saldos, No se encontró parametrización contable por procesos para el tipo de operación " + tipoOpe + " = Transacciones Con Tarjeta Debito", true);
                EscribirLog("No se encontró parametrización contable por procesos para el tipo de operación " + tipoOpe + "= Transacciones Con Tarjeta Debito");
            }
            else
            {
                LstMovimientosEnpacto = ConsultarMoviEnpac();
                if (LstMovimientosEnpacto != null && LstMovimientosEnpacto.Count > 0)
                {
                    List<Movimiento> ListMovimientos = new List<Movimiento>();
                    ListMovimientos = TrasladarList(ptipo_convenio, LstMovimientosEnpacto);
                    if (ListMovimientos != null && ListMovimientos.Count > 0)
                    {
                        Aplicofinan = AplicarMovimientosfinancial(ptipo_convenio, ListMovimientos);
                    }
                }
            }

            if (Aplicofinan == true)
            {
                //Actualiza Saldos en Enpacto provenientes de Financial
                List<Tarjeta> lsttarjetaFinancial = new List<Tarjeta>();
                lsttarjetaFinancial = ConsultarMoviFinan();

                if (lsttarjetaFinancial != null && lsttarjetaFinancial.Count > 0)
                {
                    List<Tarjeta> listResult = new List<Tarjeta>();
                    listResult = ActSaldosEnpacto(lsttarjetaFinancial);

                    if (listResult != null && listResult.Count > 0)
                    {
                        email("Se confirma con exito la gestión de los Saldos", false);
                    }
                }
            }
            else
            {
                email("¡ALERTA! No se pudo realizar la gestión de los Saldos", true);
            }

        }

        #region Enpacto
        public bool ServicioSICENPACTO(DateTime pFecha, string pConvenio, ref string pProceso, ref List<archivoSIC> lstArchivo, ref string pError)
        {
            // Haciendo la petición
            WebRequest request = WebRequest.Create("Http://" + HostAppliance + "/webservice/sic");
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            // Cargando los datos con que se hace la solicitud
            byte[] byteArray = Encoding.UTF8.GetBytes("usuario=" + usuarioAppliance + "&clave=" + claveAppliance + "&fecha=" + pFecha.ToString("dd/MM/yyyy"));
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            // Ejecutar el webservices y leer la respuesta
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            // Cargando los datos de los movimientos en un list.
            lstArchivo = new List<archivoSIC>();
            while (reader.Peek() >= 0)
            {
                string readLine = reader.ReadLine();
                pProceso += readLine + "\n";
                if (!readLine.Contains("{" + _strQuote + "estado" + _strQuote + ":false,"))
                {
                    string[] arrayline = readLine.Split(Convert.ToChar(";"));
                    if (arrayline.Length >= 1)
                    {
                        archivoSIC entidad = new archivoSIC();
                        entidad.fecha = arrayline[0];
                        entidad.hora = arrayline[1];
                        entidad.documento = arrayline[2];
                        entidad.nrocuenta = arrayline[3];
                        entidad.tarjeta = arrayline[4];
                        entidad.tipotransaccion = arrayline[5];
                        entidad.descripcion = arrayline[6];
                        entidad.monto = arrayline[7];
                        entidad.comision = arrayline[8];
                        entidad.lugar = arrayline[9];
                        entidad.operacion = arrayline[10];
                        entidad.red = arrayline[11];
                        lstArchivo.Add(entidad);
                    }
                    else
                    {
                        pError = "error en linea :" + readLine;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        //Se consulta Movimientos de enpacto para aplicar en financial
        public List<archivoSIC> ConsultarMoviEnpac()
        {
            DateTime FechaMovimientos = DateTime.Today.AddDays(-1);
            List<archivoSIC> lstArchivo = new List<archivoSIC>();
            string pError = "";
            string pRequestXmlString = "";

            if (!ServicioSICENPACTO(FechaMovimientos, _convenio, ref pRequestXmlString, ref lstArchivo, ref pError))
            {
                email("¡ALERTA!  Error en la gestión de los saldos se recomienda revisar el error generado en el servicio de Actualizar Saldos, Se presento error." + pError + " " + pRequestXmlString, true);
                EscribirLog("Se presento error." + pError + " " + pRequestXmlString);
            }
            if (lstArchivo.Count <= 0)
            {
                EscribirLog(pRequestXmlString);
            }
            return lstArchivo;
        }

        /// <summary>
        /// Método para actualización de los saldos de tarjeta débito
        /// </summary>
        /// <param name="listaTarjetasSaldos"></param>
        /// <returns></returns>
        List<Tarjeta> ActSaldosEnpacto(List<Tarjeta> listaTarjetasSaldos)
        {
            List<Tarjeta> ListTarjetasSaldoACero = new List<Tarjeta>();
            string proceso = "";
            string error = "";
            string ruta = "";
            //ruta = Server.MapPath(ruta);

            //ruta = System.Reflection.Assembly.GetEntryAssembly().Location;
            ruta = System.Web.HttpContext.Current.Server.MapPath(ruta);
            string archivo = _convenio + DateTime.Now.ToString("ddMMyyyy") + ".cls";
            string rutayarchivo = ruta + "\\" + archivo;
            System.IO.StreamWriter newfile = new StreamWriter(rutayarchivo);
            string separador = ";";

            foreach (Tarjeta tarjeta in listaTarjetasSaldos)
            {
                Cuenta cuenta = new Cuenta();
                if (!string.IsNullOrEmpty(tarjeta.tipo_cuenta))
                    cuenta.tipocuenta = tarjeta.tipo_cuenta;
                if (!string.IsNullOrEmpty(cuenta.numero_cuenta))
                    cuenta.numero_cuenta = Convert.ToString(cuenta.numero_cuenta);

                List<Cuenta> entidad = new List<Cuenta>();

                GeneralService generalService = new GeneralService();
                General pGeneral = generalService.ConsultarGeneral(107, _usuario);

                entidad = _cuentaService.ListarCuenta(cuenta, pGeneral.valor, _usuario);

                if (entidad[0].tipocuenta == "C" || entidad[0].tipocuenta == "R" || entidad[0].tipocuenta == "2")
                {
                    string linea = "";
                    linea = entidad[0].identificacion + separador + entidad[0].nombres.Trim() + separador + EsNulo(entidad[0].direccion, "").Trim() + separador + EsNulo(entidad[0].telefono, "").Trim() + separador +
                               EsNulo(entidad[0].email, "").Trim() + separador + entidad[0].tipocuenta + separador + entidad[0].nrocuenta.Trim() + separador + Math.Round(entidad[0].saldodisponible) + separador +
                               Math.Round(entidad[0].saldototal) + separador + entidad[0].fechasaldo.ToString("dd/MM/yyyy");
                    newfile.WriteLine(linea);
                }

            }
            newfile.Close();
            // Verificar que el archivo se creeo correctamente
            System.IO.StreamReader file = new System.IO.StreamReader(rutayarchivo);
            if (file != null)
            {
                Xpinn.TarjetaDebito.Entities.RespuestaEnpactoClientes respuestaEnpacto = null;
                ////VAMOS AQUI debe consumir los valores del archivo por el web service
                ServicioCLIENTESENPACTO(_convenio, archivo, rutayarchivo, ref proceso, ref error, ref respuestaEnpacto);

                // Verificamos que se halla podido transformar la respuesta del servicio a la entidad respectiva y que halla cuentas que revisar
                if (respuestaEnpacto != null && respuestaEnpacto.relaciones != null && respuestaEnpacto.relaciones.Count > 0)
                {
                    foreach (Xpinn.TarjetaDebito.Entities.RelacionClienteEnpacto relacion in respuestaEnpacto.relaciones)
                    {
                        if (relacion.tarjeta != null && relacion.cuenta != null)
                        {
                            // Pasamos la info a una entidad del sistema
                            Tarjeta tarjeta = new Tarjeta
                            {
                                numtarjeta = relacion.tarjeta,
                                numero_cuenta = relacion.cuenta
                            };

                            // Verificamos si la tarjeta existe en nuestro sistema
                            bool existe = _cuentaService.VerificarSiTarjetaExiste(tarjeta, _usuario);

                            // Si no existe la creamos, este SP crea la tarjeta segun la informacion de la cuenta asociada
                            // Consultara si es un Ahorro o un Credito y creara la tarjeta segun sea el caso
                            if (!existe)
                            {
                                tarjeta = _cuentaService.CrearTarjeta(tarjeta, _usuario);
                            }
                        }
                    }
                }
            }
            return ListTarjetasSaldoACero;
        }

        public bool ServicioCLIENTESENPACTO(string pConvenio, string pNomArchivo, string pRutYNomArchivo, ref string pProceso, ref string pError, ref Xpinn.TarjetaDebito.Entities.RespuestaEnpactoClientes respuestaEntidad)
        {
            bool resultado = ServicioCLIENTESENPACTO(pConvenio, pNomArchivo, pRutYNomArchivo, ref pProceso, ref pError);

            // Si la respuesta del servicio es valida y no hay errores, materializamos la entidad de respuesta para obtener las relaciones
            if (!string.IsNullOrWhiteSpace(pProceso) && string.IsNullOrWhiteSpace(pError))
            {
                respuestaEntidad = JsonConvert.DeserializeObject<Xpinn.TarjetaDebito.Entities.RespuestaEnpactoClientes>(pProceso);
            }

            return resultado;
        }
        public bool ServicioCLIENTESENPACTO(string pConvenio, string pNomArchivo, string pRutYNomArchivo, ref string pProceso, ref string pError)
        {
            WebClient web = new WebClient();
            web.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            NameValueCollection datos = new NameValueCollection();
            web.QueryString = datos;

            byte[] responseArray = web.UploadFile("http://" + HostAppliance + "/webservice/clientes?usuario=" + usuarioAppliance + "&clave=" + claveAppliance + "&archivo=" + pNomArchivo + "&relacion=1", "POST", pRutYNomArchivo);
            pProceso = System.Text.Encoding.ASCII.GetString(responseArray);

            return true;
        }

        #endregion

        #region Financial
        public bool ValidarProcesoContable(DateTime pFecha, Int64 pTipoOpe)
        {
            // Validar que exista la parametrización contable por procesos
            Xpinn.Contabilidad.Services.ComprobanteService compServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
            List<Xpinn.Contabilidad.Entities.ProcesoContable> lstProceso = new List<Xpinn.Contabilidad.Entities.ProcesoContable>();
            lstProceso = compServicio.ConsultaProceso(0, pTipoOpe, pFecha, _usuario);
            if (lstProceso == null)
            {
                return false;
            }
            if (lstProceso.Count <= 0)
            {
                return false;
            }
            return true;
        }

        public bool AplicarMovimientosfinancial(int ptipo_convenio, List<Movimiento> lstConsulta)
        {
            string Error = "";

            if (lstConsulta.Count > 0)
            {
                int contar = 0;

                // Homologando tipo de transacción y validando la cuenta
                foreach (Movimiento entidad in lstConsulta)
                {
                    // Si el tipo de cuenta no esta especificado por defecto ahorros
                    string errorcon = "";
                    int? _cod_ofi = null;
                    entidad.tipocuenta = _cuentaService.ConsultarTipoCuenta(_convenio, entidad.nrocuenta, ref _cod_ofi, ref errorcon, _usuario);
                    if (entidad.tipocuenta == "")
                        entidad.tipocuenta = "A";
                    // Determinar el tipo de transacción
                    entidad.tipo_tran = HomologaTipoTran(entidad.tipocuenta, entidad.tipotransaccion);
                    // Las consultas, declinadas, otras, cambio  de pin y consulta de costo no aplicar el valor
                    if (entidad.tipotransaccion == "1" || entidad.tipotransaccion == "4" || entidad.tipotransaccion == "7" || entidad.tipotransaccion == "A" || entidad.tipotransaccion == "B")
                        entidad.monto = 0;
                    else
                        entidad.monto = Math.Abs(entidad.monto);
                    entidad.esdatafono = TransaccionEsDatafono(entidad.operacion, entidad.tipotransaccion, entidad.red);
                    // Verificar si la transacción ya fue aplicada
                    if (entidad.documento != "no existe")
                    {
                        bool bAplicado = true;
                        // Verificar si la transaccion fue registrada
                        Movimiento emov = new Movimiento();
                        emov = _cuentaService.ConsultarMovimiento(ptipo_convenio, entidad.tarjeta, entidad.operacion, entidad.tipotransaccion, entidad.documento, entidad.fecha, 0, _usuario);
                        if (emov == null)
                        {
                            bAplicado = false;
                        }
                        else
                        {
                            // Verificar si el monto fue aplicado
                            string error = "";
                            if (entidad.monto != 0)
                            {
                                Movimiento eAplicacion = new Movimiento();
                                eAplicacion = _cuentaService.DatosDeAplicacion(emov.num_tran, entidad.nrocuenta, emov.cod_ope, emov.cod_cliente, ConvertirStringToDate(entidad.fecha), entidad.monto, entidad.tipo_tran, entidad.operacion, ref error, _usuario);
                                EscribirLog("eAplicacion.valor_apl:" + eAplicacion.valor_apl);
                                if (eAplicacion == null)
                                {
                                    bAplicado = false;
                                }
                                else
                                {
                                    if (eAplicacion.num_tran_apl == null || eAplicacion.num_tran_apl == 0)
                                        bAplicado = false;
                                }
                            }
                            // Verificar si la comision fue aplicado
                            if (entidad.comision != 0)
                            {
                                Movimiento eAplicacionCom = new Movimiento();
                                eAplicacionCom = _cuentaService.DatosDeAplicacion(emov.num_tran, entidad.nrocuenta, emov.cod_ope, emov.cod_cliente, ConvertirStringToDate(entidad.fecha), entidad.comision, entidad.tipo_tran, entidad.operacion, ref error, _usuario);
                                EscribirLog("eAplicacionCom.num_tran_apl" + eAplicacionCom.num_tran_apl);
                                if (eAplicacionCom == null)
                                {
                                    bAplicado = false;
                                }
                                else
                                {
                                    if (eAplicacionCom.num_tran_apl == null || eAplicacionCom.num_tran_apl == 0)
                                        bAplicado = false;
                                }
                            }
                        }
                        // Si esta pendiente por aplicar entonces validar la cuenta
                        if (!bAplicado)
                        {
                            contar += 1;
                            // Validar el saldo de la cuenta
                            if (!entidad.esdatafono)
                            {
                                if (!_cuentaService.ValidarCuenta(_convenio, entidad.tarjeta, entidad.nrocuenta, entidad.tipotransaccion, entidad.monto, entidad.fecha, false, ref Error, _usuario))
                                {
                                    // Si es transacción de cuota de manejo deja pasar        
                                    if (entidad.tipotransaccion != "M")
                                    {
                                        email("¡ALERTA!  Error en la gestión de los saldos se recomienda revisar el error generado en el servicio de Actualizar Saldos, Tarjeta:" + entidad.tarjeta + " Cuenta:" + entidad.nrocuenta + " Operaciòn:" + entidad.operacion + " " + Error, true);
                                        EscribirLog("Tarjeta:" + entidad.tarjeta + " Cuenta:" + entidad.nrocuenta + " Operaciòn:" + entidad.operacion + " " + Error);
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
                // Verificar si hay transacciones para aplicar
                if (contar == 0)
                {
                    email("¡ALERTA!  En la gestión de los saldos se recomienda revisar ya que indica que  <<No hay transacciones pendientes por aplicar>> generado en el servicio de Actualizar Saldos", true);
                    EscribirLog("No hay transacciones pendientes por aplicar");
                    return false;
                }
                else
                {
                    // Crear la tarea de ejecución del proceso        
                    Int64 pCodOpe = 0;
                    if (!_cuentaService.AplicarMovimientos(_convenio, FechaProceso, lstConsulta, _usuario, ref Error, ref pCodOpe, 1))
                    {
                        email("¡ALERTA!  Error en la gestión de los saldos se recomienda revisar el error generado en el servicio de Actualizar Saldos,  Aplicar" + Error, true);
                        EscribirLog("Aplicar " + Error);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

            }
            else
            {
                email("¡ALERTA!  En la gestión de los saldos se recomienda revisar ya que indica que  <<No hay movimientos para aplicar>> generado en el servicio de Actualizar Saldos", true);
                EscribirLog("No hay movimientos para aplicar");
                return false;
            }
        }
        protected int? HomologaTipoTran(string ptipocuenta, string tipotransaccion)
        {
            return _cuentaService.HomologaTipoTran(ptipocuenta, tipotransaccion);
        }
        protected bool TransaccionEsDatafono(string poperacion, string ptipotransaccion, string pred)
        {
            if (pred == "5" && ptipotransaccion == "9")
                return false;
            if (pred == "5" && ptipotransaccion != "9")
                return true;
            return false;
        }
        public DateTime ConvertirStringToDate(String pCadena)
        {
            try
            {
                return DateTime.ParseExact(pCadena, gFormatoFecha, null);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
        //Consulta para de financial para actualizar saldos en Enpacto
        public List<Tarjeta> ConsultarMoviFinan()
        {
            List<Tarjeta> lstConsulta = new List<Tarjeta>();
            lstConsulta = _cuentaService.ListarTarjetas(ObtenerValores(), _usuario);
            return lstConsulta;
        }
        //Para agregar alguna restricciones 
        private Tarjeta ObtenerValores()
        {
            Tarjeta entitytarjeta = new Tarjeta();

            return entitytarjeta;
        }
        private string EsNulo(string pDato, string pDefault)
        {
            if (pDato == null)
                return pDefault;
            pDato.Replace("á", "a");
            pDato.Replace("é", "e");
            pDato.Replace("í", "i");
            pDato.Replace("ó", "o");
            pDato.Replace("ú", "u");
            pDato.Replace("Á", "A");
            pDato.Replace("É", "E");
            pDato.Replace("Í", "I");
            pDato.Replace("Ó", "O");
            pDato.Replace("Ú", "U");
            return pDato;
        }
        public List<Movimiento> TrasladarList(int ptipo_convenio, List<archivoSIC> lstArchivo)
        {
            List<Movimiento> lstConsulta = new List<Movimiento>();
            foreach (archivoSIC entidad in lstArchivo)
            {
                string error = "";
                int? _cod_ofi = null; 
                Movimiento entmov = new Movimiento();
                entmov.fecha = entidad.fecha;
                entmov.hora = entidad.hora;
                entmov.documento = entidad.documento;
                entmov.nrocuenta = entidad.nrocuenta;
                entmov.tarjeta = entidad.tarjeta;
                entmov.tipotransaccion = entidad.tipotransaccion;
                entmov.tipocuenta = _cuentaService.ConsultarTipoCuenta(_convenio, entmov.nrocuenta, ref _cod_ofi, ref error, _usuario);
                if (entmov.tipocuenta == "")
                    entmov.tipocuenta = "A";
                entmov.cod_ofi = _cod_ofi;
                entmov.tipo_tran = HomologaTipoTran(entmov.tipocuenta, entmov.tipotransaccion);
                entmov.descripcion = entidad.descripcion;
                entmov.monto = ConvertirStringToDecimal(entidad.monto) / 100;
                entmov.comision = ConvertirStringToDecimal(entidad.comision) / 100;
                entmov.lugar = entidad.lugar;
                entmov.operacion = entidad.operacion;
                entmov.red = entidad.red;
                entmov.error = "";
                // Determinar saldo actual de la cuenta           
                try { entmov.saldo_total = _cuentaService.ConsultarSaldoCuenta(_convenio, entmov.nrocuenta, ref error, _usuario); } catch { }
                // Validar si aplicó el monto
                Movimiento emov = new Movimiento();
                try { emov = _cuentaService.ConsultarMovimiento(ptipo_convenio, entmov.tarjeta, entmov.operacion, entmov.tipotransaccion, entmov.documento, entmov.fecha, entmov.monto, _usuario); } catch { }
                if (emov != null)
                {
                    entmov.num_tran_verifica = emov.num_tran;
                    error = "";
                    // Determinar datos del monto
                    try
                    {
                        Movimiento eAplicacion = new Movimiento();
                        eAplicacion = _cuentaService.DatosDeAplicacion(emov.num_tran, entmov.nrocuenta, emov.cod_ope, emov.cod_cliente, ConvertirStringToDate(entmov.fecha), entmov.monto, entmov.tipo_tran, entmov.operacion, ref error, _usuario);
                        entmov.cod_ope_apl = eAplicacion.cod_ope_apl;
                        entmov.num_tran_apl = eAplicacion.num_tran_apl;
                        entmov.valor_apl = eAplicacion.valor_apl;
                        entmov.num_comp_apl = eAplicacion.num_comp_apl;
                        entmov.tipo_comp_apl = eAplicacion.tipo_comp_apl;
                    }
                    catch
                    {
                        email("¡ALERTA!  Error en la gestión de los saldos se recomienda revisar el error generado en el servicio de Actualizar Saldos, Error.:" + error, true);
                        EscribirLog("Error.:" + error);
                    }
                    // Determinar datos de la comisión
                    try
                    {
                        int? tipo_tran = _cuentaService.TipoTranComision(entmov.tipocuenta, entmov.tipo_tran);
                        Movimiento eAplicacionCom = new Movimiento();
                        eAplicacionCom = _cuentaService.DatosDeAplicacion(emov.num_tran, entmov.nrocuenta, emov.cod_ope, emov.cod_cliente, ConvertirStringToDate(entmov.fecha), entmov.comision, tipo_tran, entmov.operacion, ref error, _usuario);
                        if (entmov.cod_ope_apl == null)
                            entmov.cod_ope_apl = eAplicacionCom.cod_ope_apl;
                        entmov.comision_apl = eAplicacionCom.valor_apl;
                    }
                    catch (Exception ex)
                    {
                        email("¡ALERTA!  Error en la gestión de los saldos se recomienda revisar el error generado en el servicio de Actualizar Saldos, Error..:" + error + ex.Message, true);
                        EscribirLog("Error..:" + error + ex.Message);
                    }
                }
                // Cargar datos
                lstConsulta.Add(entmov);
            }
            return lstConsulta;
        }
        public decimal ConvertirStringToDecimal(String pCadena)
        {
            if (pCadena == "")
                return 0;
            try
            {
                return Convert.ToDecimal(pCadena.Replace("$", "").Replace(gSeparadorMiles, ""));
            }
            catch
            {
                return 0;
            }
        }
        #endregion.

        #region Configuracion


        /// <summary>
        /// Método para convertir el response del webservices que esta en XML en una entidad
        /// </summary>
        /// <param name="pRespuesta"></param>
        /// <returns></returns>
        /// 

        //RespuestaEnpacto DeserializeTran(string pRespuesta)
        //{
        //    JavaScriptSerializer ser = new JavaScriptSerializer();
        //    RespuestaEnpacto myNames = ser.Deserialize<RespuestaEnpacto>(pRespuesta);
        //    return myNames;
        //}

        /// <summary>
        /// Método para realizizar la encriptación de los datos
        /// </summary>
        /// <param name="pDatos"></param>
        /// <returns></returns>
        string EncriptarDatos(string pDatos, ref string pError)
        {
            try
            {
                // Creamos el algoritmo encriptador
                Aes algoritmo = Aes.Create();
                ConfigurarAlgoritmo(algoritmo, Convert.ToInt32(_padding), 128);
                GenerarClave(algoritmo);
                GenerarIV(algoritmo);
                byte[] mensajeEncriptado;
                mensajeEncriptado = Encriptar(pDatos, algoritmo);
                return ByteArrayToString(mensajeEncriptado);
            }
            catch (Exception ex)
            {
                pError = "EncriptarDatos Error:" + ex.Message + " " + pDatos.Length;
            }
            return null;
        }

        /// <summary>
        /// Méotodo que determina los parámetros del algoritmo de encriptación. Se usa AES-CBC
        /// </summary>
        /// <param name="algoritmo"></param>
        /// <param name="padding"></param>
        void ConfigurarAlgoritmo(Aes algoritmo, int padding, int longitudbloque)
        {
            algoritmo.BlockSize = longitudbloque;
            algoritmo.Mode = CipherMode.CBC;
            if (padding == 0)
                algoritmo.Padding = PaddingMode.None;
            else if (padding == 1)
                algoritmo.Padding = PaddingMode.ANSIX923;
            else if (padding == 2)
                algoritmo.Padding = PaddingMode.ISO10126;
            else if (padding == 3)
                algoritmo.Padding = PaddingMode.PKCS7;
            else if (padding == 4)
                algoritmo.Padding = PaddingMode.Zeros;
            else
                algoritmo.Padding = PaddingMode.None;
        }

        /// <summary>
        /// Se determina la clave o llave de encriptación de la entidad
        /// </summary>
        /// <param name="algoritmo"></param>
        void GenerarClave(Aes algoritmo)
        {
            algoritmo.Key = StringToByteArray(_llave);
        }

        /// <summary>
        /// Determina el vector inicial
        /// </summary>
        /// <param name="algoritmo"></param>
        void GenerarIV(Aes algoritmo)
        {
            algoritmo.IV = StringToByteArray(_vector);
        }

        /// <summary>
        /// Método para convertir un array de bytes en un string
        /// </summary>
        /// <param name="ba"></param>
        /// <returns></returns>
        string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }

        /// <summary>
        /// Método para convertir un string que representa un hexadecimal en un array de bytes
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        /// <summary>
        /// Método para encriptar los datos según configuración del algoritmo
        /// </summary>
        /// <param name="mensajeSinEncriptar"></param>
        /// <param name="algoritmo"></param>
        /// <returns></returns>
        byte[] Encriptar(string mensajeSinEncriptar, Aes algoritmo)
        {
            ICryptoTransform encriptador = algoritmo.CreateEncryptor();
            byte[] textoPlano = Encoding.UTF8.GetBytes(mensajeSinEncriptar);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encriptador, CryptoStreamMode.Write);
            cryptoStream.Write(textoPlano, 0, textoPlano.Length);
            cryptoStream.FlushFinalBlock();
            memoryStream.Close();
            cryptoStream.Close();
            return memoryStream.ToArray();
        }

        /// <summary>
        /// Método para desencriptar un mensaje según configuración del algoritmo
        /// </summary>
        /// <param name="mensajeEncriptado"></param>
        /// <param name="algoritmo"></param>
        /// <returns></returns>
        byte[] Desencriptar(byte[] mensajeEncriptado, Aes algoritmo)
        {
            int numeroBytesDesencriptados = 0;
            byte[] mensajeDesencriptado = new byte[mensajeEncriptado.Length];
            ICryptoTransform desencriptador = algoritmo.CreateDecryptor();
            MemoryStream memoryStream = new MemoryStream(mensajeEncriptado);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, desencriptador, CryptoStreamMode.Read);
            numeroBytesDesencriptados = cryptoStream.Read(mensajeDesencriptado, 0, mensajeDesencriptado.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return mensajeDesencriptado;
        }


        #endregion

        #region Email

        //mensaje = captura la información a remitir en el email
        //asunto = indica si es un error o es satisfactorio el proceso true= errores, false = sin errores
        public void email(string mensaje, bool asunto)
        {
            TarjetaConvenioService _serviceConvenio = new TarjetaConvenioService();
            string tema = "Actualización de Saldos";

            //Lista de Emails
            List<Email> lstEmail = new List<Email>();
            List<Email> lstRemitente = new List<Email>();
            List<string> lstCorreoDestino = new List<string>();

            //Consultamos los Email tanto Remitentes como Destinatarios
            lstEmail = _serviceConvenio.ListaEmailAlerta(_usuario);

            //Separamos los Remitentes de los Destinatarios
            foreach (Email EvalEmail in lstEmail)
            {
                if (EvalEmail.tipo_email == 1 && EvalEmail.estado == 1 && EvalEmail.Clave != "")
                {
                    if (EvalEmail.email != "" && EvalEmail.email.Contains("@") && EvalEmail.email != null)
                        lstRemitente.Add(EvalEmail);
                }
                if (EvalEmail.tipo_email == 2 && EvalEmail.estado == 1)
                {
                    if (EvalEmail.email != "" && EvalEmail.email.Contains("@") && EvalEmail.email != null)
                        lstCorreoDestino.Add(EvalEmail.email);
                }
            }

            //Determina el asunto del correo
            if (asunto == true)
            {
                tema = "¡ALERTA! " + tema;
            }
            else
            {
                tema = "¡PROCESO REALIZADO CON EXITO! " + tema;
            }


            //Valida si hay por lo menos un Remitente
            if (lstRemitente.Count > 0)
            {
                foreach (Email EmailRemitente in lstRemitente)
                {
                    //Valida si hay por lo menos un Destinatario
                    if (lstCorreoDestino.Count > 0)
                    {
                        foreach (string CorreoDst in lstCorreoDestino)
                        {
                            //Destinatario -- Remitente -Clave Remitente  
                            CorreoHelper UtilCorreo = new CorreoHelper(CorreoDst, EmailRemitente.email.Trim(), EmailRemitente.Clave.Trim());

                            string nombreEnviador = "Financial  - " + _usuario.empresa;
                            bool estado = UtilCorreo.EnviarCorreoSinHTML(mensaje, Xpinn.Util.Correo.Gmail, tema, nombreEnviador);

                            if (estado == true)
                                EscribirLog(": Se Envio Correo a: " + CorreoDst + ", Sobre: " + tema);
                            else
                                EscribirLog(": No fue posible el envio del correo a: " + CorreoDst + ", Sobre: " + tema);
                        }
                    }
                    else
                    {
                        EscribirLog(": No hay Destinatarios para realizar el envio de Alertas, Gestión - Actualización de Saldos");
                    }
                }
            }
            else
            {
                EscribirLog(": No hay Remitentes para realizar el envio de Alertas, Gestión - Actualización de Saldos");
            }

        }
        #endregion

    }
}