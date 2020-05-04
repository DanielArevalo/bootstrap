using EnpactoBloqueoTarjetaWindowService.Clases_Enpacto;
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
using System.Web.Script.Serialization;
using Xpinn.Comun.Entities;
using Xpinn.Comun.Services;
using Xpinn.Interfaces.Entities;
using Xpinn.Interfaces.Services;
using Xpinn.TarjetaDebito.Entities;
using Xpinn.TarjetaDebito.Services;
using Xpinn.Util;
using System.Collections.Specialized;

namespace EnpactoBloqueoTarjetaWindowService
{
    public partial class EnpactoService : ServiceBase
    {
        TarjetaService _tarjetaService = new TarjetaService();
        CuentaService _cuentaService = new CuentaService();
        EnpactoServices _enpactoService = new EnpactoServices();
        Usuario _usuario;
        Timer _timer;

        readonly string _strQuote = Encoding.ASCII.GetString(new byte[] { 34 });   // Es usada para colocar comilla doble dentro de las variables tipo string
        string _strHost = "";
        string _convenio = "";
        readonly string _llave = "0123456789ABCDEFFEDCBA9876543210";
        readonly string _vector = "00000000000000000000000000000000";
        readonly string _padding = "4";
        readonly string _urlArchivoLog = @"C:\Publica\LogEnpacto\LogBloqueoEjecutado.txt";
        readonly int _timeout = 0;
        string _HostAppliance = "";
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
                EscribirLog(": Se Inicializa el Windows Services. Convenio:" + _convenio + " Host:" + _HostAppliance);
                EjecutarGestionTarjetas();
            }
            catch
            { }
        }

        protected override void OnStart(string[] args)
        {
            _timer = new Timer(TimeSpan.FromHours(8).TotalMilliseconds); // every 8 hours
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
                using (StreamWriter streamWriter = new StreamWriter(_urlArchivoLog, true))
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
                EjecutarGestionTarjetas();
            }
            catch (Exception ex)
            {
                email("¡ALERTA!  Error en la gestión de las tarjetas se recomienda revisa el error generado en el servicio de Bloqueo de Tarjetas, con un error: " + ex, true);
                EscribirLog(": Error" + ex.Message);
            }
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
                    email("¡ALERTA!  Error en la gestión de las tarjetas se recomienda revisa el error generado en el servicio de Bloqueo de Tarjetas, Error al determinar el usuario de base de datos. Error:" + pError, true);
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
                        _HostAppliance = lsttarjetaConvenio[0].ip_appliance;
                        usuarioAppliance = lsttarjetaConvenio[0].usuario_appliance;
                        claveAppliance = lsttarjetaConvenio[0].usuario_appliance;
                    }
                }
                catch (Exception ex)
                {
                    email("¡ALERTA!  Error en la gestión de las tarjetas se recomienda revisa el error generado en el servicio de Bloqueo de Tarjetas, : Error al determinar convenio." + ex.Message, true);
                    EscribirLog(": Error al determinar convenio." + ex.Message);
                }
            }
            catch (Exception ex)
            {
                email("¡ALERTA!  Error en la gestión de las tarjetas se recomienda revisa el error generado en el servicio de Bloqueo de Tarjetas, : Error al determinar el usuario " + ex.Message, true);
                EscribirLog(": Error al determinar el usuario " + ex.Message);
            }
        }

        private void EjecutarGestionTarjetas()
        {
            // PROCESO PARA BLOQUEAR
            List<Tarjeta> listaTarjetasParaBloquear = ListarTarjetasEnMoraYNoBloqueadas();
            List<Tarjeta> lstCuentasCredito = new List<Tarjeta>(); //Agregada para Separar las cuentas de credito de las de ahorro en el bloqueo
            List<Tarjeta> lstCuentasCreditoxSaldo = new List<Tarjeta>(); //Agregada para Separar las cuentas de credito de las de ahorro en el bloqueo
            List<Tarjeta> lstCuentasAhorros = new List<Tarjeta>(); //Agregada para Separar las cuentas de ahorro de las de credito 
            EscribirLog(": Se consulto para bloquear tarjetas en Financial");

            //Consultamos el Parametro si se bloquea la tarjeta o se llevan el saldo disponible a 0 de la cuenta credito
            GeneralService generalService = new GeneralService();
            General general = generalService.ConsultarGeneral(102, _usuario); //Determina tipo bloqueo tarjeta 1=Bloqueo Cupo, Otro Valor=Bloqueo Tarjeta

            if (listaTarjetasParaBloquear != null)
                EscribirLog(": Número de Tarjetas para Bloquear:" + listaTarjetasParaBloquear.Count);
            else
                EscribirLog(": No se pudo consultar tarjetas para bloquear");

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Determinar si existen tarjetas para bloquear
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (listaTarjetasParaBloquear != null && listaTarjetasParaBloquear.Count > 0)
            {
                EscribirLog(": Tipo de bloqueo de tarjeta: ->" + general.valor + "<-");
                if (general.valor.Trim() == "1") //|| general.valor.Trim() == "2"
                {
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    // BLOQUEAR AHORRO
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    foreach (Tarjeta tarjeta in listaTarjetasParaBloquear)
                    {
                        if (tarjeta.tipo_cuenta == "1" || tarjeta.tipo_cuenta == "A" || tarjeta.tipo_cuenta == "")
                        {
                            lstCuentasAhorros.Add(tarjeta);
                        }
                        if (tarjeta.tipo_cuenta == "2" || tarjeta.tipo_cuenta == "C" || tarjeta.tipo_cuenta == "Credito Rotativo")
                        {
                            lstCuentasCredito.Add(tarjeta);
                        }
                    }

                    if (lstCuentasAhorros != null && lstCuentasAhorros.Count > 0)
                    {
                        List<Tarjeta> listaTarjetaBloqueadasSatisfactoriamenteAhorro = BloquearTarjetasEnpacto(lstCuentasAhorros);

                        if (listaTarjetaBloqueadasSatisfactoriamenteAhorro != null && listaTarjetaBloqueadasSatisfactoriamenteAhorro.Count > 0)
                        {
                            EscribirLog(": Se bloqueo tarjetas en Enpacto - Ahorro");
                            BloquearTarjetasFinancial(listaTarjetaBloqueadasSatisfactoriamenteAhorro);
                            EscribirLog(": Se bloqueo tarjetas en Financial - Ahorro");
                        }
                        else
                        {
                            EscribirLog(": No hay tarjetas de cuentas de ahorros para bloquear en ENPACTO");
                        }
                    }

                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //BLOQUEAR SALDO DEL CREDITO ROTATIVO
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //lstCuentasCredito = ListarTarjetasEnMoraYNoBloqueadasXSaldo();
                    if (lstCuentasCredito != null && lstCuentasCredito.Count > 0)
                    {
                        foreach (Tarjeta tarjetaBS in lstCuentasCredito)
                        {
                            tarjetaBS.saldo_disponible = 0; //Se indica 0 al saldo disponible para bloquearlas
                            tarjetaBS.estado_saldo = 1;
                            lstCuentasCreditoxSaldo.Add(tarjetaBS);
                        }
                        EscribirLog(": Tarjetas de créditos rotativos para bloquear:" + lstCuentasCreditoxSaldo.Count);
                    }
                    else
                    {
                        EscribirLog(": No hay tarjetas de créditos para bloquer");
                    }
                    if (lstCuentasCreditoxSaldo != null && lstCuentasCreditoxSaldo.Count > 0)
                    {
                        EscribirLog(": Iniciando proceso de bloqueo de tarjetas");
                        List<Tarjeta> listaTarjetasConCuentasSinSaldo = SaldoFinancial(lstCuentasCreditoxSaldo);
                        if (listaTarjetasConCuentasSinSaldo != null && listaTarjetasConCuentasSinSaldo.Count > 0)
                        {
                            EscribirLog(": Se bloqueo saldo tarjetas de rotativos en Financial - Credito");
                            SaldoEnpacto(listaTarjetasConCuentasSinSaldo, "B");
                            EscribirLog(": Se bloqueo saldo tarjetas de rotativos en Enpacto - Credito");
                        }
                        else
                        {
                            EscribirLog(": No se pudo bloquear tarjetas sin saldo no hay registros para bloquear");
                        }
                    }
                    else
                    {
                        EscribirLog(": No se pudo bloquear tarjetas. No hay tarjetas..");
                    }
                }

                // Se realiza proceso de bloqueo de la tarjeta en ENPACTO según parámetro general.
                EscribirLog(": Verificando tipo de bloqueo. Tipo:" + general.valor);
                if (general.valor.Trim() != "1")
                {
                    EscribirLog(": Existen " + listaTarjetasParaBloquear.Count + " para bloquear en ENPACTO");
                    List<Tarjeta> listaTarjetaBloqueadasSatisfactoriamente = BloquearTarjetasEnpacto(listaTarjetasParaBloquear);
                    if (listaTarjetaBloqueadasSatisfactoriamente != null && listaTarjetaBloqueadasSatisfactoriamente.Count > 0)
                    {
                        EscribirLog(": Se bloqueo tarjetas en Enpacto");
                        BloquearTarjetasFinancial(listaTarjetaBloqueadasSatisfactoriamente);
                        EscribirLog(": Se bloqueo tarjetas en Financial");
                    }
                    else
                    {
                        EscribirLog(": No hay tarjetas para bloquear en ENPACTO");
                    }
                }
            }
            else
            {
                EscribirLog(": No hay tarjetas para bloquear");
            }

            EscribirLog(": Proceso de Bloqueo Terminado");

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // PROCESO PARA DESBLOQUEO
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (general.valor == "1") //|| general.valor == "2"
            {
                lstCuentasCredito = null;
                lstCuentasAhorros = null;

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //DESBLOQUEAR AHORRO
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                List<Tarjeta> listaTarjetasParaDesbloquear = ListarTarjetasBloqueadasYAlDia();
                EscribirLog(": Se consulto para desbloquear tarjetas en Financial");
                if (listaTarjetasParaDesbloquear != null || listaTarjetasParaDesbloquear.Count > 0)
                {
                    foreach (Tarjeta tarjetaD in listaTarjetasParaDesbloquear)
                    {
                        if (tarjetaD.tipo_cuenta == "1" || tarjetaD.tipo_cuenta == "A" || tarjetaD.tipo_cuenta == "")
                        {
                            lstCuentasAhorros.Add(tarjetaD);
                        }
                        if (tarjetaD.tipo_cuenta == "2" || tarjetaD.tipo_cuenta == "C" || tarjetaD.tipo_cuenta == "Credito Rotativo")
                        {
                            lstCuentasCredito.Add(tarjetaD);
                        }

                    }
                    if (lstCuentasAhorros != null && lstCuentasAhorros.Count > 0)
                    {
                        List<Tarjeta> listaTarjetaDesbloqueadasAhorros = DesbloquearTarjetasEnpacto(lstCuentasAhorros);

                        if (listaTarjetaDesbloqueadasAhorros != null && listaTarjetaDesbloqueadasAhorros.Count > 0)
                        {
                            EscribirLog(": Se desbloqueo tarjetas en Enpacto - Ahorro");
                            DesbloquearTarjetasFinancial(listaTarjetaDesbloqueadasAhorros);
                            EscribirLog(": Se desbloqueo tarjetas en Financial - Ahorro");
                        }
                    }

                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    // DESBLOQUEAR SALDO DEL CREDITO
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //lstCuentasCredito = ListarTarjetasBloqueadasYAlDiaXSaldo();
                    if (lstCuentasCredito != null && lstCuentasCredito.Count > 0)
                    {
                        foreach (Tarjeta tarjetaBS in lstCuentasCredito)
                        {
                            tarjetaBS.saldo_disponible = 1; //Se indica un saldo diferente a 0 para verificar por la auditoria
                            tarjetaBS.estado_saldo = 0;
                            lstCuentasCreditoxSaldo.Add(tarjetaBS);
                        }
                    }

                    if (lstCuentasCreditoxSaldo != null && lstCuentasCreditoxSaldo.Count > 0)
                    {
                        List<Tarjeta> listaTarjetasConCuentasSinSaldo = SaldoFinancial(lstCuentasCreditoxSaldo);
                        if (listaTarjetasConCuentasSinSaldo != null && listaTarjetasConCuentasSinSaldo.Count > 0)
                        {
                            EscribirLog(": Se desbloqueo tarjetas en Financial - Credito");
                            SaldoEnpacto(lstCuentasCreditoxSaldo, "D");
                            EscribirLog(": Se desbloqueo tarjetas en Enpacto - Credito");
                        }
                    }
                }
            }
            // Se realiza proceso de desbloqueo de la tarjeta en ENPACTO según el parámetro general
            if (general.valor != "1")
            {
                // Desbloqueo de la tarjeta en ENPACTO
                List<Tarjeta> listaTarjetasParaDesbloquear = ListarTarjetasBloqueadasYAlDia();
                EscribirLog(": Se consulto para desbloquear tarjetas en Financial");
                if (listaTarjetasParaDesbloquear != null && listaTarjetasParaDesbloquear.Count > 0)
                {
                    List<Tarjeta> listaTarjetaDesbloqueadasSatisfactoriamente = DesbloquearTarjetasEnpacto(listaTarjetasParaDesbloquear);
                        EscribirLog(": Se desbloqueo tarjetas en Enpacto");
                    if (listaTarjetaDesbloqueadasSatisfactoriamente != null && listaTarjetaDesbloqueadasSatisfactoriamente.Count > 0)
                    {
                        DesbloquearTarjetasFinancial(listaTarjetaDesbloqueadasSatisfactoriamente);
                        EscribirLog(": Se desbloqueo tarjetas en Financial");
                    }
                }
                else
                {
                    EscribirLog(": No hay Tarjetas en condiciones para Desbloquear");
                }
            }
            EscribirLog(": Proceso de Bloqueo Terminado");
            EscribirLog(": Se confirma con exito la gestión de las tarjetas");
            email("Se confirma con exito la gestión de las tarjetas", false);
        }

        #region Consumo WebService


        bool GenerarTransaccionENPACTO(string pConvenio, Xpinn.TarjetaDebito.Entities.TransaccionEnpacto pDatos, bool pReverso, ref Xpinn.TarjetaDebito.Entities.RespuestaEnpacto pRespuesta, ref string pError)
        {
            string sAux = "";
            // Validando
            string requestXmlString = "";
            pError = "";

            // Validaciones
            bool bCorrecto = true;
            if (pDatos.fecha.Length != 6)
            {
                pError += "la fecha debe ser de 6 digitos";
                bCorrecto = false;
            }
            else
            {
                int mes = Convert.ToInt16(pDatos.fecha.Substring(2, 2));
                if (mes < 0 || mes > 12)
                {
                    pError += "El mes de la fecha es incorrecto debe estar entre 0 a 12. Actual:" + mes;
                    bCorrecto = false;
                }
            }
            if (pDatos.hora.Length != 6)
            {
                pError += "la hora debe ser de 6 digitos";
                bCorrecto = false;
            }
            if (pDatos.secuencia.Length <= 0 || pDatos.secuencia == null)
            {
                pError += "la secuencia debe ser de 12 digitos";
                bCorrecto = false;
            }
            else
            {
                if (pDatos.secuencia.Length <= 12)
                {
                    pDatos.secuencia = new String('0', 12 - pDatos.secuencia.Length) + pDatos.secuencia;
                }
            }
            if (pDatos.tarjeta == "")
            {
                pError += "debe especificar la tarjeta de la transacción";
                bCorrecto = false;
            }
            if (pDatos.cuenta == "")
            {
                pError += "debe especificar la cuenta de la transacción";
                bCorrecto = false;
            }
            if (pDatos.tipo == "A")
            {
                if (!(pDatos.tipo_identificacion == "C") && !(pDatos.tipo_identificacion == "E") && !(pDatos.tipo_identificacion == "P") &&
                    !(pDatos.tipo_identificacion == "R") && !(pDatos.tipo_identificacion == "T"))
                {
                    pError += "El tipo de identificación debe ser C, E, P, R, T";
                    bCorrecto = false;
                }
                if (!(pDatos.tipo_cuenta == "A") && !(pDatos.tipo_cuenta == "C"))
                {
                    pError += "El tipo de cuenta debe ser A, C";
                    bCorrecto = false;
                }
                if (!(pDatos.defecto == "0") && !(pDatos.defecto == "1"))
                {
                    pError += "El tipo de cuenta por defecto debe ser 0, 1";
                    bCorrecto = false;
                }
            }

            // Si no cumple las validaciones
            if (!bCorrecto)
                return false;

            // Encriptando los datos
            string datos;
            if (pDatos.tipo == "A")
                datos = "{" + _strQuote + "tran" + _strQuote + ":{" +
                        _strQuote + "tipo" + _strQuote + ":" + _strQuote + pDatos.tipo + _strQuote + "," +
                        _strQuote + "fecha" + _strQuote + ":" + _strQuote + pDatos.fecha + _strQuote + "," +
                        _strQuote + "hora" + _strQuote + ":" + _strQuote + pDatos.hora + _strQuote + "," +
                        _strQuote + "secuencia" + _strQuote + ":" + _strQuote + pDatos.secuencia + _strQuote + "," +
                        _strQuote + "nombre" + _strQuote + ":" + _strQuote + pDatos.nombre + _strQuote + "," +
                        _strQuote + "identificacion" + _strQuote + ":" + _strQuote + pDatos.identificacion + _strQuote + "," +
                        _strQuote + "tipo_identificacion" + _strQuote + ":" + _strQuote + pDatos.tipo_identificacion + _strQuote + "," +
                        _strQuote + "tarjeta" + _strQuote + ":" + _strQuote + pDatos.tarjeta + _strQuote + "," +
                        _strQuote + "cuenta" + _strQuote + ":" + _strQuote + pDatos.cuenta + _strQuote + "," +
                        _strQuote + "tipo_cuenta" + _strQuote + ":" + _strQuote + pDatos.tipo_cuenta + _strQuote + "," +
                        _strQuote + "defecto" + _strQuote + ":" + _strQuote + pDatos.defecto + _strQuote + "," +
                        _strQuote + "cupo_retiros" + _strQuote + ":" + _strQuote + pDatos.cupo_retiros + _strQuote + "," +
                        _strQuote + "max_retiros" + _strQuote + ":" + _strQuote + pDatos.max_retiros + _strQuote + "," +
                        _strQuote + "cupo_compras" + _strQuote + ":" + _strQuote + pDatos.cupo_compras + _strQuote + "," +
                        _strQuote + "max_compras" + _strQuote + ":" + _strQuote + pDatos.max_compras + _strQuote + "," +
                        _strQuote + "saldo_disponible" + _strQuote + ":" + _strQuote + pDatos.saldo_disponible + _strQuote + "," +
                        _strQuote + "saldo_total" + _strQuote + ":" + _strQuote + pDatos.saldo_total + _strQuote + "}}";
            else
                datos = "{" + _strQuote + "tran" + _strQuote + ":{" +
                        _strQuote + "tipo" + _strQuote + ":" + _strQuote + pDatos.tipo + _strQuote + "," +
                        (pReverso ? _strQuote + "reverso" + _strQuote + ":" + "true" + "," : "") +
                        _strQuote + "fecha" + _strQuote + ":" + _strQuote + pDatos.fecha + _strQuote + "," +
                        _strQuote + "hora" + _strQuote + ":" + _strQuote + pDatos.hora + _strQuote + "," +
                        _strQuote + "secuencia" + _strQuote + ":" + _strQuote + pDatos.secuencia + _strQuote + "," +
                        _strQuote + "cuenta" + _strQuote + ":" + _strQuote + pDatos.cuenta + _strQuote + "," +
                        _strQuote + "tarjeta" + _strQuote + ":" + _strQuote + pDatos.tarjeta + _strQuote + "," +
                        _strQuote + "monto" + _strQuote + ":" + _strQuote + pDatos.monto + _strQuote + "}}  ";
            requestXmlString = "conv=" + pConvenio + "&data=" + EncriptarDatos(datos, ref sAux);
            if (sAux != "") pError += sAux;

            // Consumir el webservices 
            string responseFromServer = ConsumirWEBSERVICES("Http://" + _strHost + "/webservice/transaccion", "POST", requestXmlString, _timeout, ref sAux);
            if (sAux != "") pError += sAux;

            try
            {
                // Creamos el algoritmo encriptador
                Aes algoritmo = Aes.Create();
                ConfigurarAlgoritmo(algoritmo, Convert.ToInt32(_padding), 128);
                GenerarClave(algoritmo);
                GenerarIV(algoritmo);

                // Desencriptamos los datos
                byte[] mensajeEncriptado;
                mensajeEncriptado = StringToByteArray(responseFromServer);
                string mensj = "", respuesta = "";
                try
                {
                    byte[] mensajeDesencriptado = Desencriptar(mensajeEncriptado, algoritmo);
                    mensj += Encoding.ASCII.GetString(mensajeDesencriptado);

                    foreach (char l in mensj)
                    {
                        if (l.ToString() != "\0")
                            respuesta += l;
                    }
                }
                catch (Exception ex)
                {
                    pError += ex.Message;
                    return false;
                }

                // Cerramos algoritmo
                algoritmo.Clear();

                // Generar la entidad con los datos de la respuesta
                pRespuesta = (Xpinn.TarjetaDebito.Entities.RespuestaEnpacto)DeserializeTran(respuesta);
                if (pRespuesta != null)
                {
                    pRespuesta.tran.datos = datos;
                    pRespuesta.tran.datos_encriptados = requestXmlString;
                    pRespuesta.tran.respuesta_encriptada = responseFromServer;
                    pRespuesta.tran.drespuesta = respuesta;
                }

                // Validar la entidad con datos de la respuesta
                if (pRespuesta == null)
                    pRespuesta = new Xpinn.TarjetaDebito.Entities.RespuestaEnpacto();
                if (pRespuesta.tran == null)
                    pRespuesta.tran = new Xpinn.TarjetaDebito.Entities.Respuesta();

                // Determinar la transacción fue realizada
                if (pRespuesta.tran.estado == "OK")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                pError = ex.Message + " " + responseFromServer;
                return false;
            }
        }

        /// <summary>
        /// Este método me permite realizar el consumo de un WEBSERVICES
        /// </summary>
        /// <param name="pUrl"></param>
        /// <param name="pMetodo"></param>
        /// <param name="requestXmlString"></param>
        /// <returns></returns>
        string ConsumirWEBSERVICES(string pUrl, string pMetodo, string pRequestXmlString, int pTimeOut, ref string pError)
        {
            string responseFromServer = "";
            try
            {
                // Crear una solicitud a través de una dirección URL que puede recibir un mensaje. 
                WebRequest request = WebRequest.Create(pUrl);
                if (pTimeOut > 0)
                    request.Timeout = pTimeOut;
                request.Method = pMetodo;
                request.ContentType = "application/x-www-form-urlencoded";
                // Convertir el mensaje a bytes y luego determinar la longitud del contenido
                byte[] byteArray = Encoding.UTF8.GetBytes(pRequestXmlString);
                request.ContentLength = byteArray.Length;
                // Asignar los datos a un STREAM para poderlos colocar en el webrequest.
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                // Obtener la respuesta
                WebResponse response = request.GetResponse();
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
                // Cerrar las conexiones
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                pError = "ConsumirWEBSERVICES Error:" + ex.Message;
            }
            return responseFromServer;
        }


        #endregion

        #region Configuracion


        /// <summary>
        /// Método para convertir el response del webservices que esta en XML en una entidad
        /// </summary>
        /// <param name="pRespuesta"></param>
        /// <returns></returns>
        Xpinn.TarjetaDebito.Entities.RespuestaEnpacto DeserializeTran(string pRespuesta)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            Xpinn.TarjetaDebito.Entities.RespuestaEnpacto myNames = ser.Deserialize<Xpinn.TarjetaDebito.Entities.RespuestaEnpacto>(pRespuesta);
            return myNames;
        }

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

        #region Metodos Bloqueo de tarjetas

        // Buscamos todas las tarjetas que estan en mora y necesitan bloquearse segun el parametro general
        private List<Tarjeta> ListarTarjetasEnMoraYNoBloqueadas()
        {
            //Consulta los dias apartir se debe bloquear la tarjeta 
            GeneralService generalService = new GeneralService();
            General general = generalService.ConsultarGeneral(41, _usuario);

            if (!string.IsNullOrWhiteSpace(general.valor))
            {
                int numeroDeDiasParaBloquearTarjetas = Convert.ToInt32(general.valor);

                //Consultamos si tiene encuenta todos los productos para bloquear la cuenta o por lo contrario solo el rotativo  
                General generalProductos = generalService.ConsultarGeneral(106, _usuario);
                string ProductosenCuentaparaBloqueo = generalProductos.valor.ToString();

                //Consultamos el tipo de Bloqueo que se esta utilizando 1=Bloqueo Cupo, Otro Valor=Bloqueo Tarjeta
                General tipobloqueo = generalService.ConsultarGeneral(102, _usuario);
                int tipo_bloqueo = Convert.ToInt32(tipobloqueo.valor);

                List<Tarjeta> listaTarjetasParaBloquear = _tarjetaService.ListarTarjetasEnMoraYNoBloqueadas(numeroDeDiasParaBloquearTarjetas, ProductosenCuentaparaBloqueo, tipo_bloqueo, _usuario);

                return listaTarjetasParaBloquear;
            }
            else
            {
                return new List<Tarjeta>();
            }
        }

        // Mandamos las tarjetas a bloquear a Enpacto
        private List<Tarjeta> BloquearTarjetasEnpacto(List<Tarjeta> listaTarjetasParaBloquear)
        {
            List<Tarjeta> listaTarjetaBloqueadasSatisfactoriamente = new List<Tarjeta>();
            try
            {
                int contador = 0;
                foreach (Tarjeta tarjeta in listaTarjetasParaBloquear)
                {
                    if (contador <= 385)
                    {
                        DateTime dateToReport = DateTime.Now;
                        string secuencia = dateToReport.Ticks.ToString().Substring(0, 12);

                        Xpinn.TarjetaDebito.Entities.TransaccionEnpacto transaccion = new Xpinn.TarjetaDebito.Entities.TransaccionEnpacto
                        {
                            tipo = "B",
                            fecha = dateToReport.ToString("yyMMdd"),
                            hora = dateToReport.Hour.ToString("D2") + dateToReport.Minute.ToString("D2") + "00",
                            secuencia = secuencia,
                            cuenta = tarjeta.numero_cuenta,
                            tarjeta = tarjeta.numtarjeta,
                            monto = "0"
                        };

                        string error = string.Empty;
                        Xpinn.TarjetaDebito.Entities.RespuestaEnpacto respuesta = new Xpinn.TarjetaDebito.Entities.RespuestaEnpacto();

                        bool operacionExitosa = GenerarTransaccionENPACTO(_convenio, transaccion, false, ref respuesta, ref error);

                        if (operacionExitosa && string.IsNullOrWhiteSpace(error))
                        {
                            listaTarjetaBloqueadasSatisfactoriamente.Add(tarjeta);
                        }

                        try
                        {
                            Enpacto_Aud enpactoAud = new Enpacto_Aud
                            {
                                exitoso = operacionExitosa && string.IsNullOrWhiteSpace(error) ? 1 : 0,
                                jsonentidadpeticion = transaccion != null ? JsonConvert.SerializeObject(transaccion) : string.Empty,
                                jsonentidadrespuesta = respuesta != null ? JsonConvert.SerializeObject(respuesta) : string.Empty,
                                tipooperacion = 3 // 3 - WebServices EnpactoSVC Bloqueo/Desbloqueo
                            };
                            _enpactoService.CrearEnpacto_Aud(enpactoAud, _usuario);
                        }
                        catch (Exception ex)
                        { EscribirLog(": Se presentó error al generar el log. Error:" + ex.Message); }
                        EscribirLog(": " + contador + " Tarjeta " + tarjeta.numtarjeta + " cuenta " + tarjeta.numero_cuenta + " bloqueada");
                    }
                    contador += 1;
                }
                EscribirLog(": Proceso de bloqueo en ENPACTO terminado");
            }
            catch (Exception ex)
            { EscribirLog(": Se presentó error al procesar bloqueos en ENPACTO. Error:" + ex.Message); }
            return listaTarjetaBloqueadasSatisfactoriamente;
        }

        // Segun las tarjetas bloqueadas por Enpacto, ahora las bloqueamos en Financial
        void BloquearTarjetasFinancial(List<Tarjeta> listaTarjetaBloqueadasSatisfactoriamente)
        {
            foreach (Tarjeta tarjeta in listaTarjetaBloqueadasSatisfactoriamente)
            {
                _tarjetaService.CambiarEstadoTarjeta(tarjeta, EstadoTarjetaEnpacto.Bloqueada, _usuario);
            }
        }

        #endregion

        #region Metodos Desbloqueo de tarjetas

        // Listamos las tarjetas que fueron o estan bloqueadas pero estan al dia
        List<Tarjeta> ListarTarjetasBloqueadasYAlDia()
        {
            GeneralService generalService = new GeneralService();
            General pGeneral = generalService.ConsultarGeneral(102, _usuario);
            List<Tarjeta> listaTarjetasParaDesbloquear = _tarjetaService.ListarTarjetasBloqueadasYAlDia(Convert.ToInt32(pGeneral.valor), _usuario);
            return listaTarjetasParaDesbloquear;
        }

        // Mandamos las tarjetas a desbloquer a Enpacto
        List<Tarjeta> DesbloquearTarjetasEnpacto(List<Tarjeta> listaTarjetasParaDesbloquear)
        {
            List<Tarjeta> listaTarjetaDesbloqueadasSatisfactoriamente = new List<Tarjeta>();

            foreach (Tarjeta tarjeta in listaTarjetasParaDesbloquear)
            {
                DateTime dateToReport = DateTime.Now;
                string secuencia = dateToReport.Ticks.ToString().Substring(0, 12);

                Xpinn.TarjetaDebito.Entities.TransaccionEnpacto transaccion = new Xpinn.TarjetaDebito.Entities.TransaccionEnpacto
                {
                    tipo = "D",
                    fecha = dateToReport.ToString("yyMMdd"),
                    hora = dateToReport.Hour.ToString("D2") + dateToReport.Minute.ToString("D2") + "00",
                    secuencia = secuencia,
                    cuenta = tarjeta.numero_cuenta,
                    tarjeta = tarjeta.numtarjeta,
                    monto = "0"
                };

                string error = string.Empty;
                Xpinn.TarjetaDebito.Entities.RespuestaEnpacto respuesta = new Xpinn.TarjetaDebito.Entities.RespuestaEnpacto();

                bool operacionExitosa = GenerarTransaccionENPACTO(_convenio, transaccion, false, ref respuesta, ref error);

                if (operacionExitosa && string.IsNullOrWhiteSpace(error))
                {
                    listaTarjetaDesbloqueadasSatisfactoriamente.Add(tarjeta);
                }

                Enpacto_Aud enpactoAud = new Enpacto_Aud
                {
                    exitoso = operacionExitosa && string.IsNullOrWhiteSpace(error) ? 1 : 0,
                    jsonentidadpeticion = transaccion != null ? JsonConvert.SerializeObject(transaccion) : string.Empty,
                    jsonentidadrespuesta = respuesta != null ? JsonConvert.SerializeObject(respuesta) : string.Empty,
                    tipooperacion = 3 // 3 - WebServices EnpactoSVC Bloqueo/Desbloqueo
                };
                _enpactoService.CrearEnpacto_Aud(enpactoAud, new Usuario());
            }

            return listaTarjetaDesbloqueadasSatisfactoriamente;
        }

        // Segun las tarjetas desbloqueadas por Enpacto, ahora las desbloqueamos en Financial
        void DesbloquearTarjetasFinancial(List<Tarjeta> listaTarjetaDesbloqueadasSatisfactoriamente)
        {
            foreach (Tarjeta tarjeta in listaTarjetaDesbloqueadasSatisfactoriamente)
            {
                _tarjetaService.CambiarEstadoTarjeta(tarjeta, EstadoTarjetaEnpacto.Activa, _usuario);
            }
        }

        #endregion

        #region CambiodeSaldo
        List<Tarjeta> SaldoEnpacto(List<Tarjeta> listaTarjetasSaldoCero, string ProcesoEnp)
        {
            EscribirLog(": Iniciando proceso de bloqueos de saldos en Enpacto");
            List<Tarjeta> ListTarjetasSaldoACero = new List<Tarjeta>();
            string proceso = "";
            string error = "";
            if (listaTarjetasSaldoCero != null && listaTarjetasSaldoCero.Count > 0)
            {
                string ruta = "C:\\Publica\\LogEnpacto";
                string archivo = _convenio + DateTime.Now.ToString("ddMMyyyy") + ".cls";
                string rutayarchivo = ruta + "\\" + archivo;
                EscribirLog(": Generando archivo de actualizacion de saldos " + rutayarchivo);
                System.IO.StreamWriter newfile = new StreamWriter(rutayarchivo);
                string separador = ";";

                foreach (Tarjeta tarjeta in listaTarjetasSaldoCero)
                {
                    Cuenta cuenta = new Cuenta();
                    if (!string.IsNullOrEmpty(tarjeta.numero_cuenta))
                    {
                        cuenta.numero_cuenta = Convert.ToString(tarjeta.numero_cuenta);
                        cuenta.tipocuenta = tarjeta.tipo_cuenta;
                    }

                    List<Cuenta> entidad = new List<Cuenta>();
                    EscribirLog(": Consultar la cuenta " + cuenta.numero_cuenta + " tipo de cuenta " + cuenta.tipocuenta);

                    GeneralService generalService = new GeneralService();
                    General pGeneral = generalService.ConsultarGeneral(107, _usuario);

                    entidad = _cuentaService.ListarCuenta(cuenta, pGeneral.valor, _usuario);

                    if (entidad != null && ProcesoEnp == "B")
                    {
                        entidad[0].saldodisponible = 0;
                    }

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
                    EscribirLog(": Se creo Archivo para ejecutar WS de Enpacto");
                    RespuestaEnpactoClientes respuestaEnpacto = null;
                    ////VAMOS AQUI debe consumir los valores del archivo por el web service
                    EscribirLog(": Ejecutando WS de Enpacto");
                    ServicioCLIENTESENPACTO(_convenio, archivo, rutayarchivo, ref proceso, ref error, ref respuestaEnpacto);
                    EscribirLog(": Respuesta WS de Enpacto " + respuestaEnpacto.mensaje);

                    // Verificamos que se halla podido transformar la respuesta del servicio a la entidad respectiva y que halla cuentas que revisar
                    if (respuestaEnpacto != null && respuestaEnpacto.relaciones != null && respuestaEnpacto.relaciones.Count > 0)
                    {
                        foreach (RelacionClienteEnpacto relacion in respuestaEnpacto.relaciones)
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
                    file.Close();
                }
                return ListTarjetasSaldoACero;
            }
            return null;
        }

        private List<Tarjeta> SaldoFinancial(List<Tarjeta> listaTarjetasSaldo)
        {
            string Error = "";
            List<Tarjeta> LstFull = new List<Tarjeta>();
            foreach (Tarjeta tarjeta in listaTarjetasSaldo)
            {
                try
                {
                    Tarjeta tarjetaR = _tarjetaService.ActualizarSaldoTarjeta(tarjeta, ref Error, _usuario);
                    if ((tarjeta.saldo_disponible == 0 || tarjeta.saldo_disponible == 1) && (tarjeta.tipo_cuenta == "2" || tarjeta.tipo_cuenta == "Credito Rotativo"))
                    {
                        LstFull.Add(tarjetaR);
                    }
                    else
                    {
                        EscribirLog(": Bloqueando/Desbloqueando la tarjeta " + tarjeta.idtarjeta + " Error:" + Error);
                    }
                }
                catch (Exception ex)
                {
                    EscribirLog(": No se pudo bloquear la tarjeta " + tarjeta.numtarjeta + " en financial, saldo:" + tarjeta.saldo_disponible + " ->" + Error + "<- Error:" + ex.Message);
                }
            }

            return LstFull;
        }

        //private List<Tarjeta> ListarTarjetasEnMoraYNoBloqueadasXSaldo()
        //{
        //    //Consulta los dias apartir se debe bloquear la tarjeta 
        //    GeneralService generalService = new GeneralService();
        //    General general = generalService.ConsultarGeneral(41, _usuario);

        //    if (!string.IsNullOrWhiteSpace(general.valor))
        //    {
        //        int numeroDeDiasParaBloquearTarjetas = Convert.ToInt32(general.valor);

        //        List<Tarjeta> listaTarjetasParaBloquear = _tarjetaService.ListarTarjetasEnMoraYNoBloqueadasXSaldo(numeroDeDiasParaBloquearTarjetas, _usuario);

        //        return listaTarjetasParaBloquear;
        //    }
        //    else
        //    {
        //        return new List<Tarjeta>();
        //    }
        //}

        //private List<Tarjeta> ListarTarjetasBloqueadasYAlDiaXSaldo()
        //{
        //    List<Tarjeta> listaTarjetasParaDesbloquear = _tarjetaService.ListarTarjetasBloqueadasYAlDiaXSaldo(_usuario);

        //    return listaTarjetasParaDesbloquear;
        //}

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

        public bool ServicioCLIENTESENPACTO(string pConvenio, string pNomArchivo, string pRutYNomArchivo, ref string pProceso, ref string pError, ref RespuestaEnpactoClientes respuestaEntidad)
        {
            bool resultado = ServicioCLIENTESENPACTO(pConvenio, pNomArchivo, pRutYNomArchivo, ref pProceso, ref pError);

            // Si la respuesta del servicio es valida y no hay errores, materializamos la entidad de respuesta para obtener las relaciones
            if (!string.IsNullOrWhiteSpace(pProceso) && string.IsNullOrWhiteSpace(pError))
            {
                respuestaEntidad = JsonConvert.DeserializeObject<RespuestaEnpactoClientes>(pProceso);
            }

            return resultado;
        }

        public bool ServicioCLIENTESENPACTO(string pConvenio, string pNomArchivo, string pRutYNomArchivo, ref string pProceso, ref string pError)
        {
            WebClient web = new WebClient();
            web.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            NameValueCollection datos = new NameValueCollection();
            web.QueryString = datos;


            byte[] responseArray = web.UploadFile("http://" + _HostAppliance + "/webservice/clientes?usuario=" + usuarioAppliance + "&clave=" + claveAppliance + "&archivo=" + pNomArchivo + "&relacion=1", "POST", pRutYNomArchivo);
            pProceso = System.Text.Encoding.ASCII.GetString(responseArray);

            return true;
        }

        #endregion

        #region Email

        //mensaje = captura la información a remitir en el email
        //asunto = indica si es un error o es satisfactorio el proceso true= errores, false = sin errores
        public void email(string mensaje, bool asunto)
        {
            TarjetaConvenioService _serviceConvenio = new TarjetaConvenioService();
            string tema = "Gestión - Bloqueo / Desbloqueo";

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
                        EscribirLog(": No hay Destinatarios para realizar el envio de Alertas, Gestión - Bloqueo / Desbloqueo");
                    }
                }
            }
            else
            {
                EscribirLog(": No hay Remitentes para realizar el envio de Alertas, Gestión - Bloqueo / Desbloqueo");
            }
        }
        #endregion

    }


    public class RespuestaEnpactoClientes
    {
        public bool estado { get; set; }
        public string mensaje { get; set; }
        public List<RelacionClienteEnpacto> relaciones { get; set; }

    }

    public class RelacionClienteEnpacto
    {
        public string documento { get; set; }
        public string cuenta { get; set; }
        public string tarjeta { get; set; }
    }
}