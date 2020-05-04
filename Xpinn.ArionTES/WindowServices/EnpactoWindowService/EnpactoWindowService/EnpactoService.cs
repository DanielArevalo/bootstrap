using EnpactoWindowService.Clases_Enpacto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using System.Xml.Linq;
using Xpinn.Interfaces.Entities;
using Xpinn.Interfaces.Services;
using Xpinn.TarjetaDebito.Entities;
using Xpinn.TarjetaDebito.Services;
using Xpinn.Util;
using Xpinn.Comun.Entities;
using Xpinn.Comun.Services;

namespace EnpactoWindowService
{
    public partial class EnpactoService : ServiceBase
    {
        Timer _timer;
        readonly string _strQuote = Encoding.ASCII.GetString(new byte[] { 34 });   // Es usada para colocar comilla doble dentro de las variables tipo string
        string _strHost = "";
        string _convenio = "";
        string _nombreconvenio = "";
        string _entidad = "";
        int _tipo_convenio;
        readonly string _llave = "0123456789ABCDEFFEDCBA9876543210";
        readonly string _vector = "00000000000000000000000000000000";
        readonly string _padding = "4";
        readonly string _urlArchivoLog = @"C:\Publica\LogEnpacto\LogEjecutado";
        Usuario usuario;

        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>        
        // Configuración al COMPILAR
        // 1. Colocar en el ProjectInstaller el nombre de la entidad.
        // 2. Colocar en el nombre del setup el nombre de la entidad en ProductName
        // 3. Cambiar en el SETUP el product code
        // 4. En la solución colocar RELEASE y X64 y compilar toda la solución.
        // 5. Activar la conexión según la entidad en el App.config
        // 6. Para COOTRAEMCALI en EnpactoWindowServe colocar "Prefer 32-Bit" en el instalador colocar Tarjet Plataform a X86
        // 7. Compilar y generar el instalador.
        /// </summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public EnpactoService()
        {
            InitializeComponent();
            // Cargar el dato del convenio
            try
            {
                // Determinar datos del(os) convenio(s)
                EstablecerConvenio(0);
                EstablecerConvenio(1);
            }
            catch
            { }
        }

        /// <summary>
        /// Establece la configuraci{on del convenio: codigo, host, entidad, tipo de convenio
        /// </summary>
        protected void EstablecerConvenio(int ptipo_convenio)
        {
            try
            {
                _convenio = "";
                _nombreconvenio = "";
                _strHost = "";
                _entidad = "";
                _tipo_convenio = -1;
                // Determinar el usuario de conexión
                ConexionWinSer conexion = new ConexionWinSer();
                Usuario usuario;
                String pError = "";
                usuario = conexion.DeterminarUsuario(ref pError);
                if (usuario == null)
                {
                    EscribirLog(": Error al determinar el usuario de base de datos. Error:" + pError);
                    return;
                }
                try
                {
                    TarjetaConvenioService convenioServicio = new TarjetaConvenioService();
                    TarjetaConvenio entidad = new TarjetaConvenio();
                    entidad.tipo_convenio = ptipo_convenio;
                    List<TarjetaConvenio> lsttarjetaConvenio = convenioServicio.ListarTarjetaConvenio(entidad, usuario);
                    if (lsttarjetaConvenio != null)
                    {
                        if (lsttarjetaConvenio.Count > 0 && lsttarjetaConvenio[0] != null)
                        { 
                            _convenio = lsttarjetaConvenio[0].codigo_bin;
                            _nombreconvenio = lsttarjetaConvenio[0].nombre;
                            _strHost = lsttarjetaConvenio[0].ip_switch;
                            _entidad = lsttarjetaConvenio[0].encargado;
                            _tipo_convenio = (lsttarjetaConvenio[0].tipo_convenio == null ? 0: Convert.ToInt32(lsttarjetaConvenio[0].tipo_convenio));
                            EscribirLog(": Se Inicializa el Windows Services. Nombre Convenio:" + _nombreconvenio + " Cod.Convenio:" + _convenio + "  Tipo de Convenio:" + _tipo_convenio + " Host:" + _strHost + " Entidad:" + _entidad);
                        }
                    }
                }
                catch (Exception ex)
                {
                    EscribirLog(": Error al determinar convenio." + ex.Message);
                }
            }
            catch (Exception ex)
            {
                EscribirLog(": Error al determinar el usuario " + ex.Message);
            }
        }

        /// <summary>
        ///  Determinar si existe el convenio
        /// </summary>
        /// <param name="ptipo_convenio"></param>
        protected bool ExisteConvenio(int ptipo_convenio)
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
                    return false;
                }
                try
                {
                    TarjetaConvenioService convenioServicio = new TarjetaConvenioService();
                    TarjetaConvenio entidad = new TarjetaConvenio();
                    entidad.tipo_convenio = ptipo_convenio;
                    List<TarjetaConvenio> lsttarjetaConvenio = convenioServicio.ListarTarjetaConvenio(new TarjetaConvenio(), usuario);
                    if (lsttarjetaConvenio != null && lsttarjetaConvenio.Count > 0 && lsttarjetaConvenio[0] != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch 
                {
                    return false;
                }
            }
            catch 
            {
                return false;
            }
        }

        protected override void OnStart(string[] args)
        {
            _timer = new Timer(TimeSpan.FromMinutes(1).TotalMilliseconds); // every 2 min
            _timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            _timer.AutoReset = true;
            _timer.Start();
        }

        protected override void OnStop()
        {
            _timer.Dispose();
        }

        /// <summary>
        ///  Deja los mensajes en el archivo plano del servidor
        /// </summary>
        /// <param name="pMensaje"></param>
        /// <returns></returns>
        bool EscribirLog(string pMensaje)
        {
            try
            {
                // Validar convenio
                if (_convenio.Trim() == "")
                {
                    EstablecerConvenio(_tipo_convenio);
                    if (_convenio.Trim() == "")
                    {
                        EscribirLog(": Consumo del WebServices de " + _nombreconvenio + ". No se pudo establecer el código del convenio");
                    }
                }
            }
            catch (Exception ex)
            {
                EscribirLog(": Error:" + ex.Message);
            }
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(_urlArchivoLog + (_entidad == null ? "" : _entidad) + ".txt", true))
                {
                    string mesageParaFormatear = (pMensaje != "." ? DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " " + _nombreconvenio : "") + " " + pMensaje;
                    streamWriter.WriteLine(mesageParaFormatear);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Temporizador que me determina cada cuento se ejecuta el llamado de las transacciones
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Elapsed(object sender, EventArgs e)
        {
            try
            {
                EscribirLog(".");                
                if (ExisteConvenio (1))
                {
                    EstablecerConvenio(1);
                    EscribirLog(": Se ejecutó el Windows Services. Tipo de convenio:" + _tipo_convenio);
                    SolicitudTransaccionesEnLineaCOOPCENTRAL();
                }
                EscribirLog(".");
                if (ExisteConvenio(0))
                {
                    EstablecerConvenio(0);
                    EscribirLog(": Se ejecutó el Windows Services. Tipo de convenio:" + _tipo_convenio);
                    SolicitudTransaccionesEnLineaBBOGOTA();
                }
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                // Si se ejecutan transacciones en linea
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                EscribirLog(".");
                if (_tipo_convenio == 1 || _tipo_convenio == -1)
                    EscribirLog(": No implementado");
                else                    
                    ActualizarTransacciones(_tipo_convenio);
            }
            catch (Exception ex)
            {
                EscribirLog(": Error. Solicitud de Transacciones en Línea. Convenio:" +  _nombreconvenio + " " + ex.Message);
            }
        }

        #region Auditoria Enpacto
        /// <summary>
        /// Inserta en la tabla de auditoria para las transacciones BANCO DE BOGOTA
        /// </summary>
        /// <param name="movimientos"></param>
        /// <param name="movimientosresp"></param>
        /// <param name="pError"></param>
        /// <returns></returns>
        bool GenerarAuditoria(SolicitudEnpacto movimientos, RepuestaNotificacionEnpacto movimientosresp, ref string pError)
        {
            try
            {
                EnpactoServices enpactoService = new EnpactoServices();
                Enpacto_Aud enpactoAud = new Enpacto_Aud
                {
                    exitoso = string.IsNullOrWhiteSpace(pError) && movimientos != null && movimientosresp != null ? 1 : 0,
                    jsonentidadpeticion = movimientos != null ? JsonConvert.SerializeObject(movimientos) : string.Empty,
                    jsonentidadrespuesta = movimientosresp != null ? JsonConvert.SerializeObject(movimientosresp) : string.Empty,
                    tipooperacion = (_tipo_convenio == 1 ? 20 : 2) // 2 - WebServices EnpactoSVC
                };
                enpactoService.CrearEnpacto_Aud(enpactoAud, new Usuario());
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        /// <summary>
        /// Inserta en la tabla de auditoria para las transacciones COOPCENTRAL
        /// </summary>
        /// <param name="movimientoscc"></param>
        /// <param name="movimientosresp"></param>
        /// <param name="pError"></param>
        /// <returns></returns>
        bool GenerarAuditoria(SolicitudCoopcentral movimientoscc, RepuestaNotificacionEnpacto movimientosresp, ref string pError)
        {
            try
            {
                EnpactoServices enpactoService = new EnpactoServices();
                Enpacto_Aud enpactoAud = new Enpacto_Aud
                {
                    exitoso = string.IsNullOrWhiteSpace(pError) && movimientoscc != null && movimientosresp != null ? 1 : 0,
                    jsonentidadpeticion = movimientoscc != null ? JsonConvert.SerializeObject(movimientoscc) : string.Empty,
                    jsonentidadrespuesta = movimientosresp != null ? JsonConvert.SerializeObject(movimientosresp) : string.Empty,
                    tipooperacion = (_tipo_convenio == 1 ? 20 : 2) // 2 - WebServices EnpactoSVC
                };
                enpactoService.CrearEnpacto_Aud(enpactoAud, new Usuario());
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Consumo del WEBSERVICES transacciones enpactosvc
        /// <summary>
        /// Este método me permite realizar el consumo de un WEBSERVICES
        /// </summary>
        /// <param name="pUrl"></param>
        /// <param name="pMetodo"></param>
        /// <param name="requestXmlString"></param>
        /// <returns></returns>
        string ConsumirWEBSERVICES(string pUrl, string pMetodo, string requestXmlString, ref string pError)
        {
            string responseFromServer = "";
            try
            {
                // Crear una solicitud a través de una dirección URL que puede recibir un mensaje. 
                WebRequest request = WebRequest.Create(pUrl);
                request.Method = pMetodo;
                request.ContentType = "application/x-www-form-urlencoded";
                // Convertir el mensaje a bytes y luego determinar la longitud del contenido
                byte[] byteArray = Encoding.UTF8.GetBytes(requestXmlString);
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
                pError = ex.Message;
                email("¡ALERTA! Error en la ejecución del servicio de transacciones en linea,  Error. Solicitud de Transacciones en Línea" + ex.Message, true);
                EscribirLog(": Error al consumir el web services. URL " + pUrl + " Error:" + ex.Message);
            }
            return responseFromServer;
        }

        /// <summary>
        /// Webservice “enpactosvc” reporta todas las transacciones realizadas en la red de cajeros y datáfonos de la red financiera, para ser registradas en el aplicativo de la entidad, tales como :
        ///     Consultas con valor de comisión, Retiro, Compras, Reversión Retiro, Reversión Compra
        /// </summary>
        /// <param name="pConvenio"></param>
        /// <returns></returns>
        bool WebServicesSwitchEnpactoSVC(string pConvenio, ref string responseFromServer, ref string respuesta, ref string pError)
        {
            // Validar el host
            if (_strHost == "")
            {
                EscribirLog(": No se pudo determinar el host en convenio:" + pConvenio);
            }

            // Validando        
            pError = "";

            // Consumir el webservices 
            responseFromServer = ConsumirWEBSERVICES("Http://" + _strHost + "/webservice/enpactosvc", "POST", "conv=" + pConvenio, ref pError);
            EscribirLog(": Host:" + _strHost + " Convenio:" + pConvenio + " Error:" + pError);

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
                string mensj = "";
                respuesta = "";
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
                    pError += "\n" + ex.Message;
                    EscribirLog(": Consumo del WebServices de " + _nombreconvenio + ". Error: " + pError);
                    return false;
                }

                // Cerramos algoritmo
                algoritmo.Clear();
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                EscribirLog(": Consumo del WebServices de " + _nombreconvenio + ". Error en WebService Switch EnpactoSVC: " + ex.Message);
                return false;
            }
            return true;
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
        public string ByteArrayToString(byte[] ba)
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

        #region Email de Notificación
        //mensaje = captura la información a remitir en el email
        //asunto = indica si es un error o es satisfactorio el proceso true= errores, false = sin errores
        public void email(string mensaje, bool asunto)
        {
            string tema = "Gestión - Transaciones en Línea";

            //Lista de Emails
            List<Email> lstEmail = new List<Email>();
            List<Email> lstRemitente = new List<Email>();
            List<string> lstCorreoDestino = new List<string>();
            Xpinn.TarjetaDebito.Services.TarjetaConvenioService convenioServicio = new TarjetaConvenioService();
            lstEmail = convenioServicio.ListaEmailAlerta(usuario);

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

                            string nombreEnviador = "Financial  - " + usuario.empresa;
                            bool estado = UtilCorreo.EnviarCorreoSinHTML(mensaje, Xpinn.Util.Correo.Gmail, tema, nombreEnviador);

                            if (estado == true)
                                EscribirLog(": Se Envio Correo a: " + CorreoDst + ", Sobre: " + tema);
                            else
                                EscribirLog(": No fue posible el envio del correo a: " + CorreoDst + ", Sobre: " + tema);
                        }
                    }
                    else
                    {
                        EscribirLog(": No hay Destinatarios para realizar el envio de Alertas, Gestión Transaciones en Línea");
                    }
                }
            }
            else
            {
                EscribirLog(": No hay Remitentes para realizar el envio de Alertas, Gestión Transaciones en Línea");
            }
        }
        #endregion

        #region Enpacto BANCO DE BOGOTA

        /// <summary>
        /// Método para consultar las transacciones realizadas en ENPACTO-BANCO DE BOGOTA  a través red de cajeros
        /// </summary>
        public void SolicitudTransaccionesEnLineaBBOGOTA()
        {
            string responseFromServer = "";
            string respuesta = "";
            string respuestadelaNotificacion = "";
            SolicitudEnpacto movimientos = new SolicitudEnpacto();
            RepuestaNotificacionEnpacto movimientosresp = new RepuestaNotificacionEnpacto();
            string pError = string.Empty;

            if (_convenio.Trim() == "")
            {
                EstablecerConvenio(0);
                if (_convenio.Trim() == "")
                {
                    EscribirLog(": Consumo del WebServices de Enpacto. No se pudo establecer el código del convenio");
                    return;
                }
            }

            WebServicesSwitchEnpactoSVC(_convenio, ref responseFromServer, ref respuesta, ref pError);
            EscribirLog(": Consumo del WebServices de " + _nombreconvenio + ". Reportado: " + respuesta);
            try { movimientos = (SolicitudEnpacto)DeserializeSolicitud(respuesta); } 
            catch (Exception ex) 
            {
                EscribirLog(": Se presento error al deserializar transacciones." + ex.Message);
                GenerarAuditoria(movimientos, movimientosresp, ref pError);
                return;
            }

            if (!string.IsNullOrWhiteSpace(pError.Trim()))
            {
                EscribirLog(": Se presento error al solicitar transacciones." + pError);
                GenerarAuditoria(movimientos, movimientosresp, ref pError);
                return;
            }
            if (movimientos == null)
            {
                EscribirLog(": No hay transacciones en ENPACTO para aplicar en FINANCIAL (1).");
                GenerarAuditoria(movimientos, movimientosresp, ref pError);
                return;
            }
            if (movimientos.trans == null)
            {
                EscribirLog(": No hay transacciones en ENPACTO para aplicar en FINANCIAL (2).");
                GenerarAuditoria(movimientos, movimientosresp, ref pError);
                return;
            }
            if (movimientos.trans.Count <= 0)
            {
                EscribirLog(": No hay transacciones en ENPACTO para aplicar en FINANCIAL (3).");
                GenerarAuditoria(movimientos, movimientosresp, ref pError);
                return;
            }

            try
            {
                // Determinar el usuario de conexión
                ConexionWinSer conexion = new ConexionWinSer();
                usuario = conexion.DeterminarUsuario(ref pError);

                // Aplicar la transacción
                NotificacionEnpacto movimientosapl = new NotificacionEnpacto();
                CuentaService cuentaService = new CuentaService();
                long codOpe = 0;

                // Verificar datos de las transacciones
                foreach (Movimiento movimiento in movimientos.trans)
                {
                    // Cargar movimientos para notificar. Manejar formato de fecha YYYY-MM-DD
                    if (movimiento.fecha.Length == 4)
                        movimiento.fecha = DateTime.Now.Year + "-" + movimiento.fecha.Substring(0, 2) + "-" + movimiento.fecha.Substring(2, 2);
                    if (movimiento.fecha.Length == 6)
                        movimiento.fecha = DateTime.Now.Year.ToString().Substring(0, 2) + movimiento.fecha.Substring(0, 2) + "-" + movimiento.fecha.Substring(2, 2) + "-" + movimiento.fecha.Substring(4, 2);
                    if (movimiento.cuenta.Length >= 3)
                        movimiento.nrocuenta = movimiento.cuenta.Substring(3, movimiento.cuenta.Length - 3);
                    else
                        movimiento.nrocuenta = movimiento.cuenta;
                    movimiento.esdatafono = false;
                    string errorcon = "";
                    int? _cod_ofi = null;
                    movimiento.tipocuenta = cuentaService.ConsultarTipoCuenta(_convenio, movimiento.nrocuenta, ref _cod_ofi, ref errorcon, usuario); ;
                    movimiento.operacion = movimiento.secuencia;
                    movimiento.documento = movimiento.secuencia;
                    movimiento.cod_ofi = _cod_ofi;
                    movimiento.tipotransaccion = HomologarTipoEnpacto(movimiento.tipo);
                    // Para las transacciones declinadas no aplicar el valor, solo comisión.
                    // REVERSADAS. 
                    if (movimiento.error == "200")
                    {
                        if (movimiento.tipo == "1")        // Reversadas (Retiros).
                        {
                            movimiento.tipo_tran = HomologarTipoTran(movimiento.tipocuenta, "5");
                            movimiento.tipotransaccion = "5";
                        }
                        else if (movimiento.tipo == "2")   // Reversadas (Compras).
                        {
                            movimiento.tipo_tran = HomologarTipoTran(movimiento.tipocuenta, "5");
                            movimiento.tipotransaccion = "5";
                        }
                        else if (movimiento.tipo == "3")   // Reversadas (Consignaciones).
                        {
                            movimiento.tipo_tran = HomologarTipoTran(movimiento.tipocuenta, "6");
                            movimiento.tipotransaccion = "6";
                        }
                        else if (movimiento.tipo == "4")   // Reversadas (Anulación Compra).
                        {
                            movimiento.tipo_tran = HomologarTipoTran(movimiento.tipocuenta, "5");
                            movimiento.tipotransaccion = "5";
                        }
                        // Para las reversiones de retiros y compras verificar que la secuencia existe en caso contrario no hacer nada
                        if (movimiento.tipo == "1" || movimiento.tipo == "2")
                        {
                            Movimiento movaplicado = new Movimiento();
                            movaplicado = cuentaService.ConsultarMovimiento(0, movimiento.tarjeta, movimiento.operacion, HomologarTipoEnpacto(movimiento.tipo), movimiento.documento, movimiento.fecha, movimiento.monto, usuario);
                            if (movaplicado == null)
                            {
                                movimiento.monto = 0;
                                movimiento.comision = 0;
                            }
                        }
                    }
                    // DECLINADAS. Debitar de la cuenta del asociado el valor de la comisión
                    else if (movimiento.error == "103")
                    {
                        movimiento.monto = 0;
                        if (movimiento.tipo == "1")          // Retiro.................
                        {
                            movimiento.tipo_tran = HomologarTipoTran(movimiento.tipocuenta, "4");
                            movimiento.tipotransaccion = "4";
                        }
                        else if (movimiento.tipo == "2")     // Compra
                        {
                            movimiento.tipo_tran = HomologarTipoTran(movimiento.tipocuenta, "4");
                            movimiento.tipotransaccion = "4";
                        }
                        else if (movimiento.tipo == "3")     // Consignacion
                        {
                            movimiento.tipo_tran = HomologarTipoTran(movimiento.tipocuenta, "4");
                            movimiento.tipotransaccion = "4";
                        }
                        else if (movimiento.tipo == "4")     // Anulación de Compra.....
                        {
                            movimiento.tipo_tran = HomologarTipoTran(movimiento.tipocuenta, "4");
                            movimiento.tipotransaccion = "4";
                        }
                        else
                        {
                            movimiento.tipo_tran = HomologarTipoTran(movimiento.tipocuenta, "5");
                            movimiento.tipotransaccion = "5";
                        }
                    }
                    // NORMALES
                    else
                    {
                        movimiento.tipo_tran = HomologarTipoTran(movimiento.tipocuenta, movimiento.tipo);
                    }
                }

                // Validar si la aplicación se aplica con fecha del dia siguiente
                DateTime fecha_operacion = DateTime.Today;
                if (fecha_operacion.Hour >= 20)   // Verificar si la transacción pasa de las 8pm entonces queda para el siguiente día
                {
                    fecha_operacion = fecha_operacion.AddDays(1);
                    EscribirLog(": Consumo del WebServices de Enpacto. Cambio Fecha: " + fecha_operacion);
                }

                // Realizar la aplicación de los movimientos. Se tiene un método especifico para transacciones ENPACTO-BANCO DE BOGOTA
                if (!cuentaService.AplicarMovimientos(_convenio, fecha_operacion, movimientos.trans, usuario, ref pError, ref codOpe, 0))
                {
                    if (pError.Trim() != "")
                        EscribirLog(": Aplicaciòn de los Movimientos de Banco de Bogotá. Error=>" + pError);
                }

                // Una vez aplicadas las transacciones de notifica a ENPACTO que ya fueron aplicadas
                if (string.IsNullOrWhiteSpace(pError))
                {
                    foreach (Movimiento entidad in movimientos.trans)
                    {
                        // Cargar movimientos para notificar. La fecha es formato MMDD                        
                        trans item = new trans();
                        if (entidad.fecha.Length >= 10)
                            item.fecha = entidad.fecha.Substring(5, 2) + entidad.fecha.Substring(8, 2);
                        else
                            item.fecha = entidad.fecha;
                        item.hora = entidad.hora;
                        item.secuencia = entidad.secuencia;
                        item.tarjeta = entidad.tarjeta;
                        if (movimientosapl.trans == null)
                            movimientosapl.trans = new List<trans>();
                        movimientosapl.trans.Add(item);
                    }
                    responseFromServer = "";
                    respuestadelaNotificacion = "";
                    pError = "";

                    NotificacionTransaccionEnLineaENPACTO(_convenio, movimientosapl, ref responseFromServer, ref respuestadelaNotificacion, ref movimientosresp, ref pError);
                }
                else
                {
                    EscribirLog(": Consumo del WebServices de Enpacto. Convenio:" + _nombreconvenio + " " + _convenio + ". Error al aplicar: " + pError);
                }
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                EscribirLog(": Consumo del WebServices de Enpacto. Error proceso aplicar. Error: " + ex.Message);
            }
            finally
            {
                EscribirLog(": Creando registro de auditoria");
                JavaScriptSerializer ser = new JavaScriptSerializer();
                EnpactoServices enpactoService = new EnpactoServices();
                Enpacto_Aud enpactoAud = new Enpacto_Aud
                {
                    exitoso = string.IsNullOrWhiteSpace(pError) && movimientos != null && movimientosresp != null ? 1 : 0,
                    jsonentidadpeticion = movimientos != null ? respuesta : string.Empty,
                    jsonentidadrespuesta = movimientosresp != null ? respuestadelaNotificacion : string.Empty,
                    tipooperacion = 2 // 2 - WebServices EnpactoSVC
                };
                enpactoService.CrearEnpacto_Aud(enpactoAud, new Usuario());
            }

        }

        /// <summary>
        /// Método para convertir el response del webservices en una entidad
        /// </summary>
        /// <param name="pRespuesta"></param>
        /// <returns></returns>
        SolicitudEnpacto DeserializeSolicitud(string pRespuesta)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            SolicitudEnpacto myNames = ser.Deserialize<SolicitudEnpacto>(pRespuesta);
            return myNames;
        }


        /// <summary>
        /// Homologar las transacciones en línea con las transacciones de archivo para poder compararlas
        /// </summary>
        /// <param name="tipoTransaccion"></param>
        /// <returns></returns>
        string HomologarTipoEnpacto(string tipoTransaccion)
        {
            string tipoTranHomologada = "";

            switch (tipoTransaccion.Trim())
            {
                case "0": // Consulta
                    tipoTranHomologada = "1";
                    break;
                case "1": // Retiro debito
                    tipoTranHomologada = "2";
                    break;
                case "2": // Pago o Compra    Débito
                    tipoTranHomologada = "3";
                    break;
                case "3": // Consignacion
                    tipoTranHomologada = "8";
                    break;
                case "4": // Anulacion de compra
                    tipoTranHomologada = "4";
                    break;
                default:
                    tipoTranHomologada = tipoTransaccion;
                    break;
            }

            return tipoTranHomologada;
        }

        /// <summary>
        /// Homologa del tipo de transacción de ENPACTO en línea al tipo de transacción de FINANCIAL
        /// </summary>
        /// <param name="tipoTransaccion"></param>
        /// <returns></returns>
        int HomologarTipoTran(string tipoCuenta, string tipoTransaccion)
        {
            int tipoTranHomologada = 0;

            if (tipoCuenta == "C" || tipoCuenta == "R")
            {
                if (tipoTransaccion == "5" || tipoTransaccion == "8")  // Crédito
                    tipoTranHomologada = 983;
                else
                    tipoTranHomologada = 982;
            }
            else
            {
                switch (tipoTransaccion.Trim())
                {
                    case "0": // Consulta
                        tipoTranHomologada = 230;
                        break;
                    case "1": // Retiro debito
                        tipoTranHomologada = 231;
                        break;
                    case "2": // Pago o Compra Débito
                        tipoTranHomologada = 235;
                        break;
                    case "3": // Consignacion
                        tipoTranHomologada = 237;
                        break;
                    case "4": // Anulacion de compra
                        tipoTranHomologada = 233;
                        break;
                    case "5": // Ajuste Crédito
                        tipoTranHomologada = 234;
                        break;
                    case "6": // Ajuste Débito
                        tipoTranHomologada = 232;
                        break;
                    case "7":
                        tipoTranHomologada = 236;
                        break;
                    case "9":
                        tipoTranHomologada = 238;
                        break;
                    case "A":
                        tipoTranHomologada = 239;
                        break;
                    case "B":
                        tipoTranHomologada = 240;
                        break;
                    case "M":
                        tipoTranHomologada = 241;
                        break;
                    case "P":
                        tipoTranHomologada = 242;
                        break;
                }
            }

            return tipoTranHomologada;
        }

        #region notificación a ENPACTO de las transacciones que fueron aplicadas

        /// <summary>
        /// Consumo del web services de enpacto para notificarle las transacciones que fueron aplicadas
        /// </summary>
        /// <param name="pConvenio"></param>
        /// <param name="movimientos"></param>
        /// <param name="responseFromServer"></param>
        /// <param name="respuesta"></param>
        /// <param name="movimientosresp"></param>
        /// <param name="pError"></param>
        /// <returns></returns>
        bool NotificacionTransaccionEnLineaENPACTO(string pConvenio, NotificacionEnpacto movimientos, ref string responseFromServer, ref string respuesta, ref RepuestaNotificacionEnpacto movimientosresp, ref string pError)
        {
            // Validando        
            pError = "";
            JavaScriptSerializer ser = new JavaScriptSerializer();
            string datos = ser.Serialize(movimientos);
            string data = EncriptarDatos(datos, ref pError);

            // Consumir el webservices 
            responseFromServer = ConsumirWEBSERVICES("Http://" + _strHost + "/webservice/enpactosvc", "POST", "conv=" + pConvenio + "&confirmar=true&data=" + data, ref pError);

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
                string mensj = "";
                respuesta = "";
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
                    pError += "\n" + ex.Message;
                    return false;
                }

                // Cerramos algoritmo
                algoritmo.Clear();

                movimientosresp = (RepuestaNotificacionEnpacto)DeserializeNotificacion(respuesta);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                EscribirLog(": Error Fatal " + _nombreconvenio + ". Error: " + pError);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Método para realizizar la encriptación de los datos
        /// </summary>
        /// <param name="pDatos"></param>
        /// <returns></returns>
        public string EncriptarDatos(string pDatos, ref string pError)
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
                pError = ex.Message;
            }

            return null;
        }

        /// <summary>
        /// Método para convertir el response de las notificaciones en una entidad
        /// </summary>
        /// <param name="pRespuesta"></param>
        /// <returns></returns>
        RepuestaNotificacionEnpacto DeserializeNotificacion(string pRespuesta)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            RepuestaNotificacionEnpacto myNames = ser.Deserialize<RepuestaNotificacionEnpacto>(pRespuesta);
            return myNames;
        }

        #endregion

        #region Aplicar en ENPACTO las transacciones generadas en FINANCIAL

        /// <summary>
        /// Listar las transacciones generadas en FINANCIAL y aplicarlas en ENPACTO
        /// </summary>
        /// <returns></returns>
        bool ActualizarTransacciones(int ptipo_convenio)
        {
            string error = "";
            // Validar convenio
            if (_convenio.Trim() == "")
            {
                EstablecerConvenio(ptipo_convenio);
                if (_convenio.Trim() == "")
                {
                    EscribirLog(": Consumo del WebServices de Enpacto. No se pudo establecer el código del convenio");
                    return false;
                }
            }
            // Determinar el usuario de conexión
            ConexionWinSer conexion = new ConexionWinSer();
            usuario = conexion.DeterminarUsuario(ref error);
            // Realizar el proceso            
            Xpinn.TarjetaDebito.Services.CuentaService servicioCuenta = new CuentaService();
            EscribirLog(": Verificando si se aplicación transacciones de FINANCIAL a " + _nombreconvenio + " --->" + servicioCuenta.TiposAplicacionEnpacto(ptipo_convenio, _convenio, ref error, usuario) + "<----");
            if (servicioCuenta.TiposAplicacionEnpacto(ptipo_convenio, _convenio, ref error, usuario) == 1)
            {
                int lcontrol = 0;
                List<Xpinn.TarjetaDebito.Entities.TransaccionFinancial> lstTransacciones = new List<TransaccionFinancial>();
                lstTransacciones = servicioCuenta.ListarTransaccionesPendientesAplicarEnpacto(ptipo_convenio, _convenio, usuario);
                foreach (TransaccionFinancial item in lstTransacciones)
                {
                    string Error = "";
                    string Tarjeta = "";
                    if (ptipo_convenio == 1)
                        AplicarTransaccionCoopcentral(item.tipoIdentificacion, item.identificacion, item.nombre, item.tipoProducto, item.numeroproducto, item.tipoMov, item.valor, Convert.ToInt64(item.codOpe), item.observaciones, ref Tarjeta, ref Error);
                    else
                        AplicarTransaccionEnpacto(item.tipoIdentificacion, item.identificacion, item.nombre, item.tipoProducto, item.numeroproducto, item.tipoMov, item.valor, Convert.ToInt64(item.codOpe), item.observaciones, ref Tarjeta, ref Error);
                    if (Error.Trim() != "")
                    {
                        EscribirLog(": Error. Aplicación Transacciones en Línea. " + Error);
                    }
                    else
                    {
                        // Actualizar registro de control
                        item.numero_tarjeta = Tarjeta;
                        servicioCuenta.CrearControlOperacion(_convenio, item, ref Error, usuario);
                        if (Error.Trim() != "")
                            EscribirLog(": Error. Actualizar control de operación. Ope:" + item.codOpe + " Error:" + Error);
                        else
                            EscribirLog(": Transacción. Se aplica transacción en línea. Ope:" + item.codOpe + " Tarjeta:" + item.numero_tarjeta + " Cuenta:" + item.numeroproducto + " Valor:" + item.valor + " TipoMov:" + item.tipoMov);
                    }
                    lcontrol += 1;
                }
                if (lcontrol == 0)
                    EscribirLog(": No se encontraron transacciones en FINANCIAL para aplicar en ENPACTO." + error);
            }
            return true;
        }

        public readonly string _tipoOperacionRetiroEnpacto = "1";
        public readonly string _tipoOperacionDepositoEnpacto = "3";

        /// <summary>
        /// Aplicar en ENPACTO cada una de las transacciones generadas en FINANCIAL
        /// </summary>
        /// <param name="pTipoIdentificacion"></param>
        /// <param name="pIdentificacion"></param>
        /// <param name="pNombreCliente"></param>
        /// <param name="pCodTipoProducto"></param>
        /// <param name="pNumeroProducto"></param>
        /// <param name="pTipoMov"></param>
        /// <param name="pValor"></param>
        /// <param name="pCodOpe"></param>
        /// <param name="pObservaciones"></param>
        /// <param name="pTarjeta"></param>
        /// <param name="pError"></param>
        /// <returns></returns>
        public bool AplicarTransaccionEnpacto(string pTipoIdentificacion, string pIdentificacion, string pNombreCliente, string pCodTipoProducto, string pNumeroProducto, string pTipoMov, decimal pValor, Int64 pCodOpe, string pObservaciones, ref string pTarjeta, ref string pError)
        {
            pError = "";
            pTarjeta = "";
            try
            {
                // Busco la homologacion de la cedula para los tipos de cedula de enpacto               
                HomologacionServices homologaService = new HomologacionServices();
                Homologacion homologacion = homologaService.ConsultarHomologacionTipoIdentificacion(pTipoIdentificacion, usuario);

                // Si no tengo los datos para homologar la cedula no hago nada y me voy
                if (!(homologacion != null && !string.IsNullOrWhiteSpace(homologacion.tipo_identificacion_enpacto)))
                {
                    pError = "No existe homologación para el tipo de identificación " + pTipoIdentificacion;
                    return false;
                }

                // Realizar la instanciación de la interfaz y de los servicios
                Xpinn.TarjetaDebito.Services.TarjetaService tarjetaService = new Xpinn.TarjetaDebito.Services.TarjetaService();
                Xpinn.Interfaces.Services.EnpactoServices enpactoService = new Xpinn.Interfaces.Services.EnpactoServices();

                // Reviso todas las transacciones para aplicar
                string codigoTipoProducto = pCodTipoProducto;
                TipoDeProducto tipoDeProducto = codigoTipoProducto.ToEnum<TipoDeProducto>();

                // Si no soy ahorro vista no hago nada, siguiente vuelta. Pendiente crédito rotativo.
                if (!(tipoDeProducto == TipoDeProducto.AhorrosVista || tipoDeProducto == TipoDeProducto.Credito))
                {
                    return true;
                }

                // Determinar si el producto tiene una tarjeta débito asignada
                string nroprod = Convert.ToString(pNumeroProducto);
                Xpinn.TarjetaDebito.Entities.Tarjeta tarjetaDeLaPersona = tarjetaService.ConsultarTarjetaDeUnaCuenta(nroprod, usuario);
                pTarjeta = tarjetaDeLaPersona.numtarjeta;

                // Si el producto no tiene tarjeta entonces no hacer nada
                if (tarjetaDeLaPersona == null || string.IsNullOrWhiteSpace(tarjetaDeLaPersona.numtarjeta))
                {
                    return true;
                }

                // Generar la transacción en ENPACTO
                TransaccionEnpacto transaccionEnpacto = new TransaccionEnpacto();
                long tipomov = long.Parse(pTipoMov);
                string nomtipomov = (tipomov == 2 ? "INGRESO" : "EGRESO");
                if (tipomov == 2) // Tipo movimiento = 2 (Deposito) - 1 = (Retiro)
                {
                    transaccionEnpacto.tipo = _tipoOperacionDepositoEnpacto;
                }
                else
                {
                    transaccionEnpacto.tipo = _tipoOperacionRetiroEnpacto;
                }
                transaccionEnpacto.fecha = DateTime.Now.ToString("yyMMdd");
                transaccionEnpacto.hora = DateTime.Now.Hour.ToString("D2") + DateTime.Now.Minute.ToString("D2") + "00";
                transaccionEnpacto.reverso = "false";
                transaccionEnpacto.secuencia = pCodOpe.ToString();
                transaccionEnpacto.nombre = pNombreCliente;
                transaccionEnpacto.identificacion = pIdentificacion;
                transaccionEnpacto.tipo_identificacion = homologacion.tipo_identificacion_enpacto;
                transaccionEnpacto.tarjeta = tarjetaDeLaPersona.numtarjeta;
                transaccionEnpacto.cuenta = _convenio + tarjetaDeLaPersona.numero_cuenta;
                transaccionEnpacto.tipo_cuenta = tarjetaDeLaPersona.tipo_cuenta;
                transaccionEnpacto.monto = (pValor * 100).ToString();  // Sin carácter decimal, los últimos 2 dígitos son los centavos

                RespuestaEnpacto respuesta = new RespuestaEnpacto();
                string error = string.Empty;

                try
                {
                    // Consumir el WEBSERVICES para aplicar la transacción en ENPACTO
                    GenerarTransaccionENPACTO(_convenio, transaccionEnpacto, false, ref respuesta, ref error);

                    // Si la transacción fue aplicada sin errores entonces grabar los datos en TRAN_TARJETA.
                    if (string.IsNullOrWhiteSpace(error) && respuesta != null && respuesta.tran != null)
                    {
                        string fechaTransaccionFormato = DateTime.Now.ToString("yyyy") + "-" + DateTime.Now.Month.ToString("D2") + "-" + DateTime.Now.Day.ToString("D2");

                        Xpinn.TarjetaDebito.Entities.Movimiento movimiento = new Xpinn.TarjetaDebito.Entities.Movimiento
                        {
                            fecha = fechaTransaccionFormato,
                            hora = transaccionEnpacto.hora,
                            documento = transaccionEnpacto.identificacion,
                            nrocuenta = transaccionEnpacto.cuenta,
                            tarjeta = transaccionEnpacto.tarjeta,
                            tipotransaccion = transaccionEnpacto.tipo,
                            descripcion = pObservaciones,
                            monto = Convert.ToDecimal(transaccionEnpacto.monto) / 100,
                            lugar = usuario.direccion,
                            operacion = respuesta.tran.secuencia,
                            comision = 0,
                            red = "9",
                            cod_ope = pCodOpe,
                            saldo_total = !string.IsNullOrWhiteSpace(respuesta.tran.saldo_total) ? Convert.ToDecimal(respuesta.tran.saldo_total) / 100 : default(decimal?),
                            cod_cliente = tarjetaDeLaPersona.cod_persona
                        };

                        Xpinn.TarjetaDebito.Services.CuentaService cuentaService = new Xpinn.TarjetaDebito.Services.CuentaService();
                        cuentaService.CrearMovimiento(movimiento, pCodOpe, usuario);
                    }

                    respuesta.Error = error;
                    pError = error;

                    // Dejar registro de auditoria del consumo del WEBSERVICES.
                    Xpinn.Interfaces.Entities.Enpacto_Aud enpactoEntity = new Xpinn.Interfaces.Entities.Enpacto_Aud
                    {
                        exitoso = string.IsNullOrWhiteSpace(error) ? 1 : 0,
                        jsonentidadpeticion = Newtonsoft.Json.JsonConvert.SerializeObject(transaccionEnpacto),
                        jsonentidadrespuesta = Newtonsoft.Json.JsonConvert.SerializeObject(respuesta),
                        tipooperacion = 3 // 1- WebServices Transacciones
                    };

                    // Creo la auditoria para enpacto
                    enpactoService.CrearEnpacto_Aud(enpactoEntity, usuario);

                    return true;
                }
                catch (Exception ex)
                {
                    pError = ex.Message;
                    // Buildeo la entidad para la auditoria
                    Xpinn.Interfaces.Entities.Enpacto_Aud enpactoEntity = new Xpinn.Interfaces.Entities.Enpacto_Aud
                    {
                        exitoso = 0,
                        jsonentidadpeticion = Newtonsoft.Json.JsonConvert.SerializeObject(transaccionEnpacto),
                        jsonentidadrespuesta = Newtonsoft.Json.JsonConvert.SerializeObject(ex),
                        tipooperacion = 3 // 1- WebServices Transacciones
                    };

                    // Creo la auditoria para enpacto
                    enpactoService.CrearEnpacto_Aud(enpactoEntity, usuario);

                    return false;
                }
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return false;
            }

        }

        /// <summary>
        /// Consumir web services de enpacto para aplicar una transacción
        /// </summary>
        /// <param name="pConvenio"></param>
        /// <param name="pDatos"></param>
        /// <param name="pReverso"></param>
        /// <param name="pRespuesta"></param>
        /// <param name="pError"></param>
        /// <returns></returns>
        public bool GenerarTransaccionENPACTO(string pConvenio, TransaccionEnpacto pDatos, bool pReverso, ref RespuestaEnpacto pRespuesta, ref string pError)
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
                if (pDatos.tipo_cuenta == "1")
                    pDatos.tipo_cuenta = "A";
                if (pDatos.tipo_cuenta == "2")
                    pDatos.tipo_cuenta = "C";
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
            string responseFromServer = ConsumirWEBSERVICES("Http://" + _strHost + "/webservice/transaccion", "POST", requestXmlString, ref sAux);
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
                pRespuesta = (RespuestaEnpacto)DeserializeTran(respuesta);
                if (pRespuesta != null)
                {
                    pRespuesta.tran.datos = datos;
                    pRespuesta.tran.datos_encriptados = requestXmlString;
                    pRespuesta.tran.respuesta_encriptada = responseFromServer;
                    pRespuesta.tran.drespuesta = respuesta;
                }

                // Validar la entidad con datos de la respuesta
                if (pRespuesta == null)
                    pRespuesta = new RespuestaEnpacto();
                if (pRespuesta.tran == null)
                    pRespuesta.tran = new Respuesta();

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
        /// Método para convertir el response del webservices que esta en XML en una entidad
        /// </summary>
        /// <param name="pRespuesta"></param>
        /// <returns></returns>
        /// <summary>
        /// Método para convertir el response del webservices que esta en XML en una entidad
        /// </summary>
        /// <param name="pRespuesta"></param>
        /// <returns></returns>
        public RespuestaEnpacto DeserializeTran(string pRespuesta)
        {
            JavaScriptSerializer ser = new JavaScriptSerializer();
            RespuestaEnpacto myNames = ser.Deserialize<RespuestaEnpacto>(pRespuesta);
            return myNames;
        }

        protected General ConsultarParametroGeneral(long codigo, Usuario usuario = null)
        {
            if (usuario == null) usuario = this.usuario;

            GeneralService generalService = new GeneralService();
            General general = generalService.ConsultarGeneral(codigo, usuario);
            return general;
        }

        #endregion

        #endregion

        #region Enpacto COOPCENTRAL

        /// <summary>
        /// Método para consultar las transacciones realizadas en ENPACTO-COOPCENTRAL  a través red de cajeros
        /// </summary>
        public void SolicitudTransaccionesEnLineaCOOPCENTRAL()
        {
            string responseFromServer = "";
            string respuesta = "";
            string respuestadelaNotificacion = "";
            SolicitudCoopcentral movimientos = new SolicitudCoopcentral();            
            RepuestaNotificacionEnpacto movimientosresp = new RepuestaNotificacionEnpacto();
            string pError = string.Empty;

            if (_convenio.Trim() == "")
            {
                EstablecerConvenio(1);
                if (_convenio.Trim() == "")
                {
                    EscribirLog(": Consumo del WebServices de Coopcentral . No se pudo establecer el código del convenio");
                    return;
                }
            }

            // Solicitar a ENPACTO con el WebServices las transacciones que hay para aplicar
            WebServicesSwitchEnpactoSVC(_convenio, ref responseFromServer, ref respuesta, ref pError);
            EscribirLog(": Consumo del WebServices de " + _nombreconvenio + ". Reportado: " + respuesta);
            if (!string.IsNullOrWhiteSpace(pError.Trim()))
            {
                EscribirLog(": Se presento error al solicitar transacciones." + pError);
                GenerarAuditoria(movimientos, movimientosresp, ref pError);
                return;
            }

            // Convertir la respuesta del webservices en un list
            try { movimientos = DeserializeMovimientoEnLinea(respuesta); }
            catch (Exception ex) { EscribirLog(": Error el deserializar las transacciones. " + ex.Message + " Respuesta:" + respuesta + "<-"); return; }

            if (movimientos == null)
            {
                EscribirLog(": No hay transacciones en COOPCENTRAL para aplicar en FINANCIAL (1).");
                GenerarAuditoria(movimientos, movimientosresp, ref pError);
                return;
            }
            if (movimientos.trans == null)
            {
                EscribirLog(": No hay transacciones en COOPCENTRAL para aplicar en FINANCIAL (2).");
                GenerarAuditoria(movimientos, movimientosresp, ref pError);
                return;
            }
            if (movimientos.trans.Count <= 0)
            {
                EscribirLog(": No hay transacciones en COOPCENTRAL para aplicar en FINANCIAL (3).");
                GenerarAuditoria(movimientos, movimientosresp, ref pError);
                return;
            }

            // Aplicar los movimientos
            try
            {
                // Determinar el usuario de conexión
                ConexionWinSer conexion = new ConexionWinSer();
                usuario = conexion.DeterminarUsuario(ref pError);

                // Aplicar la transacción
                CuentaService cuentaService = new CuentaService();
                long codOpe = 0;

                // Verificar datos de las transacciones
                List<MovimientoCoopcentral> lstMovimiento = new List<MovimientoCoopcentral>();
                foreach (MovimientoCoopcentralEnLinea movimiento in movimientos.trans)
                {
                    // Cargar movimientos para notificar. Manejar formato de fecha YYYY-MM-DD
                    if (movimiento.fecha.Length == 4)
                        movimiento.fecha = DateTime.Now.Year + "-" + movimiento.fecha.Substring(0, 2) + "-" + movimiento.fecha.Substring(2, 2);
                    if (movimiento.fecha.Length == 6)
                        movimiento.fecha = DateTime.Now.Year.ToString().Substring(0, 2) + movimiento.fecha.Substring(0, 2) + "-" + movimiento.fecha.Substring(2, 2) + "-" + movimiento.fecha.Substring(4, 2);
                    string errorcon = "";
                    int? _cod_ofi = null;
                    movimiento.tipocuenta = cuentaService.ConsultarTipoCuenta(_convenio, movimiento.cuenta_origen, ref _cod_ofi, ref errorcon, usuario); ;
                    movimiento.operacion = movimiento.secuencia;
                    movimiento.documento = movimiento.secuencia;
                    movimiento.cod_ofi = _cod_ofi;
                    movimiento.tipotransaccion = movimiento.transaccion;
                    // Para las transacciones declinadas no aplicar el valor, solo comisión.
                    movimiento.tipo_tran = HomologarTipoTranCoopcentral(movimiento.tipocuenta, movimiento.tipotransaccion, movimiento.grupo_trans);
                    // Para las reversiones de retiros y compras verificar que la secuencia existe en caso contrario no hacer nada
                    if (movimiento.grupo_trans == "400" || movimiento.grupo_trans == "420")
                    {                        
                        if (movimiento.transaccion == "01" || movimiento.transaccion == "02")
                        {
                            Movimiento movaplicado = new Movimiento();
                            movaplicado = cuentaService.ConsultarMovimiento(1, movimiento.tarjeta, movimiento.operacion, movimiento.transaccion, movimiento.documento, movimiento.fecha, movimiento.monto, usuario);
                            if (movaplicado == null)
                            {
                                movimiento.monto = 0;
                                movimiento.comision = 0;
                            }
                        }
                    }
                    lstMovimiento.Add(ConvertirMovimientoCoopcentral(movimiento));
                }

                // Validar si la aplicación se aplica con fecha del dia siguiente
                DateTime fecha_operacion = DateTime.Today;
                if (fecha_operacion.Hour >= 20)   // Verificar si la transacción pasa de las 8pm entonces queda para el siguiente día
                {
                    fecha_operacion = fecha_operacion.AddDays(1);
                    EscribirLog(": Consumo del WebServices de Enpacto. Cambio Fecha: " + fecha_operacion);
                }
                
                // Aplicar los movimientos
                if (!cuentaService.AplicarMovimientosCoopcentral(_convenio, fecha_operacion, lstMovimiento, usuario, ref pError, ref codOpe, 0))
                {
                    if (pError.Trim() != "")
                        EscribirLog(": Aplicaciòn de los Movimientos de Coopcentral. Error=>" + pError);
                }
                
                //Notificar a enpacto que las transacciones fueron aplciadas
                if (string.IsNullOrWhiteSpace(pError))
                {
                    NotificacionCoopcentral movimientosapl = new NotificacionCoopcentral();
                    foreach (MovimientoCoopcentralEnLinea entidad in movimientos.trans)
                    {
                        // Cargar movimientos para notificar. La fecha es formato MMDD                        
                        NotificacionCoopcentral.tran item = new NotificacionCoopcentral.tran();
                        item.fecha = entidad.fecha;
                        item.hora = entidad.hora;
                        item.secuencia = entidad.secuencia;
                        if (movimientosapl.trans == null)
                            movimientosapl.trans = new List<NotificacionCoopcentral.tran>();
                        movimientosapl.trans.Add(item);
                    }
                    responseFromServer = "";
                    pError = "";

                    NotificacionTransaccionEnLineaCOOPCENTRAL(_convenio, movimientosapl, ref responseFromServer, ref respuestadelaNotificacion, ref pError);
                }
                else
                {
                    EscribirLog(": Consumo del WebServices de Coopcentral. Convenio:" + _convenio + ". Error al aplicar: " + pError);
                }
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                EscribirLog(": Consumo del WebServices de Enpacto. Error proceso aplicar. Error: " + ex.Message);
            }
            finally
            {
                EscribirLog(": Creando registro de auditoria");
                JavaScriptSerializer ser = new JavaScriptSerializer();
                EnpactoServices enpactoService = new EnpactoServices();
                Enpacto_Aud enpactoAud = new Enpacto_Aud
                {
                    exitoso = string.IsNullOrWhiteSpace(pError) && movimientos != null && movimientosresp != null ? 1 : 0,
                    jsonentidadpeticion = (respuesta == "" || respuesta == null) ? " " : respuesta,
                    jsonentidadrespuesta = (respuestadelaNotificacion == "" || respuestadelaNotificacion == null) ? " " : respuestadelaNotificacion,
                    tipooperacion = 20 
                };
                enpactoService.CrearEnpacto_Aud(enpactoAud, new Usuario());
            }
        }

        /// <summary>
        /// Convertir el XML en la entidad correspondiente
        /// </summary>
        /// <param name="pRespuesta"></param>
        /// <returns></returns>
        public SolicitudCoopcentral DeserializeMovimientoEnLinea(string pRespuesta)
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "root";
            xRoot.IsNullable = true;
            SolicitudCoopcentral myNames = new SolicitudCoopcentral();
            XmlSerializer serializer = new XmlSerializer(typeof(SolicitudCoopcentral), xRoot);
            using (TextReader reader = new StringReader(pRespuesta))
            {
                myNames = (SolicitudCoopcentral)serializer.Deserialize(reader);
            }
            return myNames;
        }

        /// <summary>
        /// Determinar que tipo de transacción en FINANCIAL corresponde con COOPCENTRAL
        /// </summary>
        /// <param name="tipoCuenta"></param>
        /// <param name="tipoTransaccion"></param>
        /// <param name="grupoTransaccional"></param>
        /// <returns></returns>
        int HomologarTipoTranCoopcentral(string tipoCuenta, string tipoTransaccion, string grupoTransaccional)
        {
            int? tipoTranHomologada = 0;
            if (grupoTransaccional == "400" || grupoTransaccional == "420")
                tipoTransaccion = "R" + tipoTransaccion;
            else
                tipoTransaccion = "E" + tipoTransaccion;

            CuentaService cuentaService = new CuentaService();
            tipoTranHomologada = cuentaService.HomologaTipoTran(tipoCuenta, tipoTransaccion, 1);
            return Convert.ToInt32(tipoTranHomologada);
        }

        /// <summary>
        /// Reporta a ENPACTO-COOPCENTRAL las transacciones que ya fueron aplicadas para que no las vuelva a reportar
        /// </summary>
        /// <param name="pConvenio"></param>
        /// <param name="movimientos"></param>
        /// <param name="responseFromServer"></param>
        /// <param name="respuesta"></param>
        /// <param name="pError"></param>
        /// <returns></returns>
        bool NotificacionTransaccionEnLineaCOOPCENTRAL(string pConvenio, NotificacionCoopcentral movimientos, ref string responseFromServer, ref string respuesta, ref string pError)
        {
            // Validando        
            pError = "";
            // JavaScriptSerializer ser = new JavaScriptSerializer();
            // string datos = ser.Serialize(movimientos);
            string datos = /*"<?xml version=" + strQuote + "1.0" + strQuote + "?>" +*/ "<root><trans>";
            foreach (transCoopcentral item in movimientos.trans)
            {
                datos += "<tran>" + 
                         "<fecha>"      + item.fecha     + "</fecha>" +
                         "<hora>"       + item.hora      + "</hora>" +
                         "<secuencia>"  + item.secuencia + "</secuencia>" +
                         "</tran>";
            }
            datos += "</trans></root>";
            string data = EncriptarDatos(datos, ref pError);

            // Consumir el webservices 
            responseFromServer = ConsumirWEBSERVICES("Http://" + _strHost + "/webservice/enpactosvc", "POST", "conv=" + pConvenio + "&confirmar=true&data=" + data, ref pError);

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
                string mensj = "";
                respuesta = "";
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
                    pError += "\n" + ex.Message;
                    return false;
                }

                // Cerramos algoritmo
                algoritmo.Clear();

                // Obtener la respuesta
                XmlRootAttribute xRoot = new XmlRootAttribute();
                xRoot.ElementName = "root";
                xRoot.IsNullable = true;
                RepuestaNotificacionCoopcentral eRespuesta = new RepuestaNotificacionCoopcentral();
                XmlSerializer serializer = new XmlSerializer(typeof(RepuestaNotificacionCoopcentral), xRoot);
                using (TextReader reader = new StringReader(respuesta))
                {
                    eRespuesta = (RepuestaNotificacionCoopcentral)serializer.Deserialize(reader);
                }
                if (eRespuesta != null)
                    if (eRespuesta.estado != null)
                        if (eRespuesta.estado != "true")
                            pError = eRespuesta.estado + " " + eRespuesta.mensaje;
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                EscribirLog(": Error Fatal. Error: " + pError);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Homologa la entity de transacciones en línea con la de transacciones de archivo
        /// </summary>
        /// <param name="emov"></param>
        /// <returns></returns>
        MovimientoCoopcentral ConvertirMovimientoCoopcentral(MovimientoCoopcentralEnLinea emov)
        {
            MovimientoCoopcentral nuevaEntidad = new MovimientoCoopcentral();
            nuevaEntidad.idasociado = emov.documento_origen;
            nuevaEntidad.tarjeta = emov.tarjeta;
            nuevaEntidad.cuenta_origen = emov.cuenta_origen;
            nuevaEntidad.tipo_cuenta_origen = emov.tipo_cuenta_origen;
            nuevaEntidad.cuenta_destino = emov.cuenta_destino;
            nuevaEntidad.tipo_cuenta_destino = emov.tipo_cuenta_destino;
            nuevaEntidad.fecha_transaccion = emov.fecha;
            nuevaEntidad.hora_transaccion = emov.hora;
            nuevaEntidad.fecha_contable = emov.fecha_contable;
            nuevaEntidad.transaccion = emov.transaccion + ((emov.grupo_trans == "0400" || emov.grupo_trans == "0420") ? "R" : "E");
            nuevaEntidad.descripcion = "Grupo:" + emov.grupo_trans + "|Origen:" + emov.origen_trans + "|Entidad:" + emov.entidad_origen + "|Destino:" + emov.entidad_destino + "|Banco Origen:" + emov.banco_origen + "|Banco Destino:" + emov.banco_destino + "|Inter:" + emov.inter;
            nuevaEntidad.escenario = emov.adquiriente;
            nuevaEntidad.valor = emov.monto;
            nuevaEntidad.comision_asociado = emov.comision;
            nuevaEntidad.utilidad_entidad = 0;
            nuevaEntidad.secuencia = emov.secuencia;
            nuevaEntidad.descripcion_terminal = emov.lugar + " " + emov.usuario_terminal;
            nuevaEntidad.codigo_terminal = emov.canal;
            nuevaEntidad.tipo_terminal = emov.tipo_terminal;
            nuevaEntidad.comision_asumida = 0;
            nuevaEntidad.ubicacion_terminal = (emov.numero_terminal != "" ? emov.numero_terminal : emov.lugar);
            nuevaEntidad.tipocuenta = emov.tipo_cuenta_origen;
            return nuevaEntidad;
        }

        public readonly string _tipoOperacionRetiroCoopcentral = "3";
        public readonly string _tipoOperacionDepositoCoopcentral = "4";

        public bool AplicarTransaccionCoopcentral(string pTipoIdentificacion, string pIdentificacion, string pNombreCliente, string pCodTipoProducto, string pNumeroProducto, string pTipoMov, decimal pValor, Int64 pCodOpe, string pObservaciones, ref string pTarjeta, ref string pError)
        {
            pError = "";
            pTarjeta = "";
            try
            {
                // Busco la homologacion de la cedula para los tipos de cedula de enpacto               
                HomologacionServices homologaService = new HomologacionServices();
                Homologacion homologacion = homologaService.ConsultarHomologacionTipoIdentificacion(pTipoIdentificacion, usuario);

                // Si no tengo los datos para homologar la cedula no hago nada y me voy
                if (!(homologacion != null && !string.IsNullOrWhiteSpace(homologacion.tipo_identificacion_enpacto)))
                {
                    pError = "No existe homologación para el tipo de identificación " + pTipoIdentificacion;
                    return false;
                }

                // Realizar la instanciación de la interfaz y de los servicios
                Xpinn.TarjetaDebito.Services.TarjetaService tarjetaService = new Xpinn.TarjetaDebito.Services.TarjetaService();
                Xpinn.Interfaces.Services.EnpactoServices enpactoService = new Xpinn.Interfaces.Services.EnpactoServices();

                // Reviso todas las transacciones para aplicar
                string codigoTipoProducto = pCodTipoProducto;
                TipoDeProducto tipoDeProducto = codigoTipoProducto.ToEnum<TipoDeProducto>();

                // Si no soy ahorro vista no hago nada, siguiente vuelta. Pendiente crédito rotativo.
                if (!(tipoDeProducto == TipoDeProducto.AhorrosVista || tipoDeProducto == TipoDeProducto.Credito))
                {
                    return true;
                }

                // Determinar si el producto tiene una tarjeta débito asignada
                string nroprod = Convert.ToString(pNumeroProducto);
                Xpinn.TarjetaDebito.Entities.Tarjeta tarjetaDeLaPersona = tarjetaService.ConsultarTarjetaDeUnaCuenta(nroprod, usuario);
                pTarjeta = tarjetaDeLaPersona.numtarjeta;

                // Si el producto no tiene tarjeta entonces no hacer nada
                if (tarjetaDeLaPersona == null || string.IsNullOrWhiteSpace(tarjetaDeLaPersona.numtarjeta))
                {
                    return true;
                }

                // Generar la transacción en COOPCENTRAL
                TransaccionCoopcentral transaccionCoopcentral = new TransaccionCoopcentral();
                long tipomov = long.Parse(pTipoMov);
                string nomtipomov = (tipomov == 2 ? "INGRESO" : "EGRESO");
                if (tipomov == 2) // Tipo movimiento = 2 (Deposito) - 1 = (Retiro)
                {
                    transaccionCoopcentral.tipo = _tipoOperacionDepositoCoopcentral;
                }
                else
                {
                    transaccionCoopcentral.tipo = _tipoOperacionRetiroCoopcentral;
                }
                transaccionCoopcentral.fecha = DateTime.Now.ToString("yyMMdd");
                transaccionCoopcentral.hora = DateTime.Now.Hour.ToString("D2") + DateTime.Now.Minute.ToString("D2") + "00";
                transaccionCoopcentral.reverso = "false";
                transaccionCoopcentral.secuencia = pCodOpe.ToString();
                transaccionCoopcentral.nombre = pNombreCliente;
                transaccionCoopcentral.identificacion = pIdentificacion;
                transaccionCoopcentral.tipo_identificacion = homologacion.tipo_identificacion_enpacto;
                transaccionCoopcentral.tarjeta = tarjetaDeLaPersona.numtarjeta;
                transaccionCoopcentral.cuenta = _convenio + tarjetaDeLaPersona.numero_cuenta;
                transaccionCoopcentral.tipo_cuenta = tarjetaDeLaPersona.tipo_cuenta;
                transaccionCoopcentral.monto = (pValor * 100).ToString();  // Sin carácter decimal, los últimos 2 dígitos son los centavos

                RespuestaCoopcentral respuesta = new RespuestaCoopcentral();
                string error = string.Empty;

                try
                {
                    // Consumir el WEBSERVICES para aplicar la transacción en ENPACTO
                    GenerarTransaccionCOOPCENTRAL(_convenio, transaccionCoopcentral, false, ref respuesta, ref error);

                    // Si la transacción fue aplicada sin errores entonces grabar los datos en TRAN_TARJETA.
                    if (string.IsNullOrWhiteSpace(error) && respuesta != null)
                    {
                        string fechaTransaccionFormato = DateTime.Now.ToString("yyyy") + "-" + DateTime.Now.Month.ToString("D2") + "-" + DateTime.Now.Day.ToString("D2");

                        Xpinn.TarjetaDebito.Entities.Movimiento movimiento = new Xpinn.TarjetaDebito.Entities.Movimiento
                        {
                            fecha = fechaTransaccionFormato,
                            hora = transaccionCoopcentral.hora,
                            documento = transaccionCoopcentral.identificacion,
                            nrocuenta = transaccionCoopcentral.cuenta,
                            tarjeta = transaccionCoopcentral.tarjeta,
                            tipotransaccion = transaccionCoopcentral.tipo,
                            descripcion = pObservaciones,
                            monto = Convert.ToDecimal(transaccionCoopcentral.monto) / 100,
                            lugar = usuario.direccion,
                            operacion = respuesta.secuencia,
                            comision = 0,
                            red = "9",
                            cod_ope = pCodOpe,
                            saldo_total = !string.IsNullOrWhiteSpace(respuesta.saldo_total) ? Convert.ToDecimal(respuesta.saldo_total) / 100 : default(decimal?),
                            cod_cliente = tarjetaDeLaPersona.cod_persona
                        };

                        Xpinn.TarjetaDebito.Services.CuentaService cuentaService = new Xpinn.TarjetaDebito.Services.CuentaService();
                        cuentaService.CrearMovimiento(movimiento, pCodOpe, usuario);
                    }

                    respuesta.mensaje = error;
                    pError = error;

                    // Dejar registro de auditoria del consumo del WEBSERVICES.
                    Xpinn.Interfaces.Entities.Enpacto_Aud enpactoEntity = new Xpinn.Interfaces.Entities.Enpacto_Aud
                    {
                        exitoso = string.IsNullOrWhiteSpace(error) ? 1 : 0,
                        jsonentidadpeticion = Newtonsoft.Json.JsonConvert.SerializeObject(transaccionCoopcentral),
                        jsonentidadrespuesta = Newtonsoft.Json.JsonConvert.SerializeObject(respuesta),
                        tipooperacion = 30 // 1- WebServices Transacciones
                    };

                    // Creo la auditoria para enpacto
                    enpactoService.CrearEnpacto_Aud(enpactoEntity, usuario);

                    return true;
                }
                catch (Exception ex)
                {
                    pError = ex.Message;
                    // Buildeo la entidad para la auditoria
                    Xpinn.Interfaces.Entities.Enpacto_Aud enpactoEntity = new Xpinn.Interfaces.Entities.Enpacto_Aud
                    {
                        exitoso = 0,
                        jsonentidadpeticion = Newtonsoft.Json.JsonConvert.SerializeObject(transaccionCoopcentral),
                        jsonentidadrespuesta = Newtonsoft.Json.JsonConvert.SerializeObject(ex),
                        tipooperacion = 30 // 1- WebServices Transacciones
                    };

                    // Creo la auditoria para enpacto
                    enpactoService.CrearEnpacto_Aud(enpactoEntity, usuario);

                    return false;
                }
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return false;
            }

        }

        /// <summary>
        /// Método para construir los XML y generar cada transacción para COOPCENTRAL
        /// </summary>
        /// <param name="pConvenio"></param>
        /// <param name="pDatos"></param>
        /// <param name="pReverso"></param>
        /// <param name="pRespuesta"></param>
        /// <param name="pError"></param>
        /// <returns></returns>
        public bool GenerarTransaccionCOOPCENTRAL(string pConvenio, TransaccionCoopcentral pDatos, bool pReverso, ref RespuestaCoopcentral pRespuesta, ref string pError)
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
                if (pDatos.tipo_cuenta == "1")
                    pDatos.tipo_cuenta = "A";
                if (pDatos.tipo_cuenta == "2")
                    pDatos.tipo_cuenta = "C";
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
            string datos = /*"<?xml version=" + strQuote + "1.0" + strQuote + "?>" +*/ "<root>";
            if (pDatos.tipo == "0")
            {
                datos += "<documento>" + pDatos.identificacion + "</documento>" +
                         "<fecha_expedicion>" + pDatos.fecha_expedicion + "</fecha_expedicion>" +
                         "<actualizar>" + pDatos.actualizar + "</actualizar>" +
                         "<tipo_documento>" + pDatos.tipo_identificacion + "</tipo_documento>" +
                         "<nombre1>" + pDatos.nombre1 + "</nombre1>" +
                         "<nombre2>" + pDatos.nombre2 + "</nombre2>" +
                         "<apellido1>" + pDatos.apellido1 + "</apellido1>" +
                         "<apellido2>" + pDatos.apellido2 + "</apellido2>" +
                         "<direccion_casa>" + pDatos.direccion_casa + "</direccion_casa>" +
                         "<direccion_trabajo>" + pDatos.direccion_trabajo + "</direccion_trabajo>" +
                         "<telefono_casa>" + pDatos.telefono_casa + "</telefono_casa>" +
                         "<telefono_trabajo>" + pDatos.telefono_trabajo + "</telefono_trabajo>" +
                         "<celular>" + pDatos.celular + "</celular>" +
                         "<fecha_nacimiento>" + pDatos.fecha_nacimiento + "</fecha_nacimiento>" +
                         "<sexo>" + pDatos.sexo + "</sexo>" +
                         "<email>" + pDatos.email + "</email>" +
                         "<cuenta>" + pDatos.cuenta + "</cuenta>" +
                         "<tipo_cuenta>" + pDatos.tipo_cuenta + "</tipo_cuenta>" +
                         "<cuenta_defecto>" + pDatos.cuenta_defecto + "</cuenta_defecto>" +
                         "<saldo_total>" + pDatos.saldo_total + "</saldo_total>" +
                         "<saldo_disponible>" + pDatos.saldo_disponible + "</saldo_disponible>" +
                         "<tarjeta>" + pDatos.tarjeta + "</tarjeta>" +
                         "<canal>" + pDatos.canal + "</canal>" +
                         "<noperaciones>" + pDatos.noperaciones + "</noperaciones>" +
                         "<monto_maximo>" + pDatos.monto_maximo + "</monto_maximo>" +
                         "<bancodebogota>" + pDatos.bancodebogota + "</bancodebogota>" +
                         "<conv_bogota>" + pDatos.conv_bogota + "</conv_bogota>" +
                         "<pago_minimo>" + pDatos.pago_minimo + "</pago_minimo>" +
                         "<pago_total>" + pDatos.pago_total + "</pago_total>" +
                         "<fecha_vencimiento>" + pDatos.fecha_vencimiento + "</fecha_vencimiento>" +
                         "<sms>" + pDatos.sms + "</sms>";
            }
            else if (pDatos.tipo == "1")
            {
                datos += "<tarjeta>" + pDatos.tarjeta + "</tarjeta>" +
                         "<documento>" + pDatos.identificacion + "</documento>" +
                         "<tipo_documento>" + pDatos.tipo_identificacion + "</tipo_documento>" +
                         "<bloqueo>" + pDatos.bloqueo + "</bloqueo>";
            }
            else if (pDatos.tipo == "3")
            {
                datos += "<documento>" + pDatos.identificacion + "</documento>" +
                         "<cuenta>" + pDatos.cuenta + "</cuenta>" +
                         "<tipo_cuenta>" + pDatos.tipo_cuenta + "</tipo_cuenta>" +
                         "<monto>" + pDatos.monto + "</monto>";
            }
            else if (pDatos.tipo == "4")
            {
                datos += "<documento>" + pDatos.identificacion + "</documento>" +
                         "<cuenta>" + pDatos.cuenta + "</cuenta>" +
                         "<tipo_cuenta>" + pDatos.tipo_cuenta + "</tipo_cuenta>" +
                         "<monto>" + pDatos.monto + "</monto>";
            }
            else if (pDatos.tipo == "5")
            {
                datos += "<documento>" + pDatos.identificacion + "</documento>" +
                         "<cuenta>" + pDatos.cuenta + "</cuenta>";
            }
            else if (pDatos.tipo == "6")
            {
                datos += "<documento>" + pDatos.identificacion + "</documento>" +
                         "<tipo_documento>" + pDatos.tipo_identificacion + "</tipo_documento>" +
                         "<cuenta>" + pDatos.cuenta + "</cuenta>" +
                         "<tipo_cuenta>" + pDatos.tipo_cuenta + "</tipo_cuenta>" +
                         "<tarjeta>" + pDatos.tarjeta + "</tarjeta>";
            }
            else
            {
                pError = "Tipo de transacción " + pDatos.tipo + " no implementada";
                return false;
            }
            datos += "</root>";
            requestXmlString = "conv=" + pConvenio + "&data=" + EncriptarDatos(datos, ref sAux);
            if (sAux != "") pError += sAux;

            // Consumir el webservices 
            string responseFromServer = ConsumirWEBSERVICES("Http://" + _strHost + "/webservice", "POST", requestXmlString, ref sAux);
            if (sAux != "") pError += sAux;

            try
            {
                // Generar la entidad con los datos de la respuesta
                pRespuesta = DeserializeRespuestaCoopcentral(responseFromServer);

                if (pRespuesta != null)
                {
                    pRespuesta.datos= datos;
                    pRespuesta.datos_encriptados = requestXmlString;
                    pRespuesta.respuesta = responseFromServer;
                }

                // Validar la entidad con datos de la respuesta
                if (pRespuesta == null)
                    pRespuesta = new RespuestaCoopcentral();

                // Determinar la transacción fue realizada
                if (pRespuesta.estado == "true")
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
        /// Método para convertir el XML en una entitity
        /// </summary>
        /// <param name="pRespuesta"></param>
        /// <returns></returns>
        public RespuestaCoopcentral DeserializeRespuestaCoopcentral(string pRespuesta)
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "root";
            xRoot.IsNullable = true;
            RespuestaCoopcentral myNames = new RespuestaCoopcentral();
            XmlSerializer serializer = new XmlSerializer(typeof(RespuestaCoopcentral), xRoot);
            using (TextReader reader = new StringReader(pRespuesta))
            {
                myNames = (RespuestaCoopcentral)serializer.Deserialize(reader);
            }
            return myNames;
        }

        #endregion


    }
}
