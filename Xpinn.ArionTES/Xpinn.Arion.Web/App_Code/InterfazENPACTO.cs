using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using Newtonsoft.Json;
using Xpinn.TarjetaDebito.Entities;

/// <summary>
/// Esta clase se usa para procesar las transacciones con el switch autorizador de ENPACTO
/// </summary>
public class InterfazENPACTO
{
    private string strQuote = Encoding.ASCII.GetString(new byte[] { 34 });  // Es usada para colocar comilla doble dentro de las variables tipo string
    private string strHost = "";                                            // Es la IP del host autorizador en donde estan los webservices
    private string HostAppliance = "";                                      // Es la IP del host appliance en donde estan los webservices
    private string usuarioAppliance = "webservice";
    private string claveAppliance = "WWW.EE.99";
    private string llave = "";                                              // Determina la llave de encriptación asignada a la entidad
    private string vector = "";                                             // Determina el vector inicial para poder desencriptar
    private int timeout = 0;                                                // Determina el tiempo máximo de espera para una respuesta
    public string padding { get; set; }                                     // Determina tipo de llenado para las cadenas de bytes del request

    public InterfazENPACTO(string pLlave, string pVector)
    {
        GlobalWeb global = new GlobalWeb();
        HostAppliance = global.IpApplianceConvenioTarjeta();
        strHost = global.IpSwitchConvenioTarjeta();
        llave = pLlave;
        vector = pVector;
        padding = "4";
    }

    public InterfazENPACTO(string pLlave, string pVector, int pTimeOut)
    {
        GlobalWeb global = new GlobalWeb();
        HostAppliance = global.IpApplianceConvenioTarjeta();
        strHost = global.IpSwitchConvenioTarjeta();
        llave = pLlave;
        vector = pVector;
        timeout = pTimeOut;
        padding = "4";
    }

    public void ConfiguracionAppliance(string pHost, string pUsuario, string pClave)
    {
        HostAppliance = pHost;
        usuarioAppliance = pUsuario;
        claveAppliance = pClave;
    }

    #region Webservice “transacción”
    /// <summary>
    /// Webservice “transacción” se utiliza para generar transacciones desde la aplicación de la entidad, hacia el “switch autorizador” tales como:
    ///     Retiro, Consignación, Consulta de saldo, Reverso retiro, Reverso consignación, Bloqueo temporal tarjeta, Desbloqueo de bloqueo temporal tarjeta
    ///     Bloqueo y eliminación por cambio de tarjeta, Bloqueo y eliminación definitivo de tarjeta, Activación en línea (plástico)
    /// </summary>
    /// <param name="pConvenio"></param>
    /// <param name="pDatos"></param>
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
            datos = "{" + strQuote + "tran" + strQuote + ":{" +
                    strQuote + "tipo" + strQuote + ":" + strQuote + pDatos.tipo + strQuote + "," +
                    strQuote + "fecha" + strQuote + ":" + strQuote + pDatos.fecha + strQuote + "," +
                    strQuote + "hora" + strQuote + ":" + strQuote + pDatos.hora + strQuote + "," +
                    strQuote + "secuencia" + strQuote + ":" + strQuote + pDatos.secuencia + strQuote + "," +
                    strQuote + "nombre" + strQuote + ":" + strQuote + pDatos.nombre + strQuote + "," +
                    strQuote + "identificacion" + strQuote + ":" + strQuote + pDatos.identificacion + strQuote + "," +
                    strQuote + "tipo_identificacion" + strQuote + ":" + strQuote + pDatos.tipo_identificacion + strQuote + "," +
                    strQuote + "tarjeta" + strQuote + ":" + strQuote + pDatos.tarjeta + strQuote + "," +
                    strQuote + "cuenta" + strQuote + ":" + strQuote + pDatos.cuenta + strQuote + "," +
                    strQuote + "tipo_cuenta" + strQuote + ":" + strQuote + pDatos.tipo_cuenta + strQuote + "," +
                    strQuote + "defecto" + strQuote + ":" + strQuote + pDatos.defecto + strQuote + "," +
                    strQuote + "cupo_retiros" + strQuote + ":" + strQuote + pDatos.cupo_retiros + strQuote + "," +
                    strQuote + "max_retiros" + strQuote + ":" + strQuote + pDatos.max_retiros + strQuote + "," +
                    strQuote + "cupo_compras" + strQuote + ":" + strQuote + pDatos.cupo_compras + strQuote + "," +
                    strQuote + "max_compras" + strQuote + ":" + strQuote + pDatos.max_compras + strQuote + "," +
                    strQuote + "saldo_disponible" + strQuote + ":" + strQuote + pDatos.saldo_disponible + strQuote + "," +
                    strQuote + "saldo_total" + strQuote + ":" + strQuote + pDatos.saldo_total + strQuote + "}}";
        else
            datos = "{" + strQuote + "tran" + strQuote + ":{" +
                    strQuote + "tipo" + strQuote + ":" + strQuote + pDatos.tipo + strQuote + "," +
                    (pReverso ? strQuote + "reverso" + strQuote + ":" + "true" + "," : "") +
                    strQuote + "fecha" + strQuote + ":" + strQuote + pDatos.fecha + strQuote + "," +
                    strQuote + "hora" + strQuote + ":" + strQuote + pDatos.hora + strQuote + "," +
                    strQuote + "secuencia" + strQuote + ":" + strQuote + pDatos.secuencia + strQuote + "," +
                    strQuote + "cuenta" + strQuote + ":" + strQuote + pDatos.cuenta + strQuote + "," +
                    strQuote + "tarjeta" + strQuote + ":" + strQuote + pDatos.tarjeta + strQuote + "," +
                    strQuote + "monto" + strQuote + ":" + strQuote + pDatos.monto + strQuote + "}}  ";
        requestXmlString = "conv=" + pConvenio + "&data=" + EncriptarDatos(datos, ref sAux);
        if (sAux != "") pError += sAux;

        // Consumir el webservices 
        string responseFromServer = ConsumirWEBSERVICES("Http://" + strHost + "/webservice/transaccion", "POST", requestXmlString, timeout, ref sAux);
        if (sAux != "") pError += sAux;

        try
        {
            // Creamos el algoritmo encriptador
            Aes algoritmo = Aes.Create();
            ConfigurarAlgoritmo(algoritmo, Convert.ToInt32(padding), 128);
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
    public RespuestaEnpacto DeserializeTran(string pRespuesta)
    {
        JavaScriptSerializer ser = new JavaScriptSerializer();
        RespuestaEnpacto myNames = ser.Deserialize<RespuestaEnpacto>(pRespuesta);
        return myNames;
    }

    public bool Consulta(string pConvenio, string pFecha, string pHora, string pSecuencia, string pCuenta, string pTarjeta, ref RespuestaEnpacto pRespuesta, ref string pError)
    {
        TransaccionEnpacto transaccion = new TransaccionEnpacto();
        transaccion.tipo = "0";
        transaccion.fecha = pFecha;
        transaccion.hora = pHora;
        transaccion.secuencia = pSecuencia;
        transaccion.cuenta = pCuenta;
        transaccion.tarjeta = pTarjeta;
        transaccion.monto = "0";
        return GenerarTransaccionENPACTO(pConvenio, transaccion, false, ref pRespuesta, ref pError);
    }

    public bool Retiro(string pConvenio, string pFecha, string pHora, string pSecuencia, string pCuenta, string pTarjeta, decimal pMonto, bool pReverso, ref RespuestaEnpacto pRespuesta, ref string pError)
    {
        TransaccionEnpacto transaccion = new TransaccionEnpacto();
        transaccion.tipo = "1";
        transaccion.fecha = pFecha;
        transaccion.hora = pHora;
        transaccion.secuencia = pSecuencia;
        transaccion.cuenta = pCuenta;
        transaccion.tarjeta = pTarjeta;
        transaccion.monto = Convert.ToString(Math.Truncate(pMonto * 100));
        return GenerarTransaccionENPACTO(pConvenio, transaccion, pReverso, ref pRespuesta, ref pError);
    }

    public bool Consignacion(string pConvenio, string pFecha, string pHora, string pSecuencia, string pCuenta, string pTarjeta, decimal pMonto, bool pReverso, ref RespuestaEnpacto pRespuesta, ref string pError)
    {
        TransaccionEnpacto transaccion = new TransaccionEnpacto();
        transaccion.tipo = "3";
        transaccion.fecha = pFecha;
        transaccion.hora = pHora;
        transaccion.secuencia = pSecuencia;
        transaccion.cuenta = pCuenta;
        transaccion.tarjeta = pTarjeta;
        transaccion.monto = Convert.ToString(Math.Truncate(pMonto * 100));
        return GenerarTransaccionENPACTO(pConvenio, transaccion, pReverso, ref pRespuesta, ref pError);
    }

    public bool Bloqueo(string pConvenio, string pFecha, string pHora, string pSecuencia, string pCuenta, string pTarjeta, ref RespuestaEnpacto pRespuesta, ref string pError)
    {
        TransaccionEnpacto transaccion = new TransaccionEnpacto();
        transaccion.tipo = "B";
        transaccion.fecha = pFecha;
        transaccion.hora = pHora;
        transaccion.secuencia = pSecuencia;
        transaccion.cuenta = pCuenta;
        transaccion.tarjeta = pTarjeta;
        transaccion.monto = "0";
        return GenerarTransaccionENPACTO(pConvenio, transaccion, false, ref pRespuesta, ref pError);
    }

    public bool DesBloqueo(string pConvenio, string pFecha, string pHora, string pSecuencia, string pCuenta, string pTarjeta, ref RespuestaEnpacto pRespuesta, ref string pError)
    {
        TransaccionEnpacto transaccion = new TransaccionEnpacto();
        transaccion.tipo = "D";
        transaccion.fecha = pFecha;
        transaccion.hora = pHora;
        transaccion.secuencia = pSecuencia;
        transaccion.cuenta = pCuenta;
        transaccion.tarjeta = pTarjeta;
        transaccion.monto = "0";
        return GenerarTransaccionENPACTO(pConvenio, transaccion, false, ref pRespuesta, ref pError);
    }


    #endregion Webservice “transacción”

    /// <summary>
    /// Webservice “enpactosvc” reporta todas las transacciones realizadas en la red de cajeros y datáfonos de la red financiera, para ser registradas en el aplicativo de la entidad, tales como :
    ///     Consultas con valor de comisión, Retiro, Compras, Reversión Retiro, Reversión Compra
    /// </summary>
    /// <param name="pConvenio"></param>
    /// <returns></returns>
    public bool SolicitiudTransaccionEnLineaENPACTO(string pConvenio, ref string responseFromServer, ref string respuesta, ref SolicitudEnpacto movimientos, ref string pError)
    {
        string sAux = "";
        // Validando        
        pError = "";

        // Consumir el webservices 
        responseFromServer = ConsumirWEBSERVICES("Http://" + strHost + "/webservice/enpactosvc", "POST", "conv=" + pConvenio, timeout, ref sAux);
        if (sAux != "") pError = sAux;

        try
        {
            // Creamos el algoritmo encriptador
            Aes algoritmo = Aes.Create();
            ConfigurarAlgoritmo(algoritmo, Convert.ToInt32(padding), 128);
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

            movimientos = (SolicitudEnpacto)DeserializeSolicitud(respuesta);
        }
        catch (Exception ex)
        {
            pError = ex.Message;
            return false;
        }
        return true;
    }

    /// <summary>
    /// Método para convertir el response del webservices en una entidad
    /// </summary>
    /// <param name="pRespuesta"></param>
    /// <returns></returns>
    public SolicitudEnpacto DeserializeSolicitud(string pRespuesta)
    {
        JavaScriptSerializer ser = new JavaScriptSerializer();
        SolicitudEnpacto myNames = ser.Deserialize<SolicitudEnpacto>(pRespuesta);
        return myNames;
    }

    #region clasesSolicitudENPACTO
    public class SolicitudEnpacto
    {
        public List<movimiento> trans { get; set; }
    }

    public class movimiento
    {
        public string fecha { get; set; }
        public string hora { get; set; }
        public string secuencia { get; set; }
        public string tipo { get; set; }
        public string tarjeta { get; set; }
        public string cuenta { get; set; }
        public string monto { get; set; }
        public string comision { get; set; }
        public string red { get; set; }
        public string lugar { get; set; }
        public string subcausal { get; set; }
        public string error { get; set; }

    }
    #endregion

    /// <summary>
    /// Confirmacion de las transacciones una vez aplicadas en financial
    /// </summary>
    /// <param name="pConvenio"></param>
    /// <param name="responseFromServer"></param>
    /// <param name="respuesta"></param>
    /// <param name="movimientos"></param>
    /// <param name="pError"></param>
    /// <returns></returns>
    public bool NotificacionTransaccionEnLineaENPACTO(string pConvenio, NotificacionEnpacto movimientos, ref string responseFromServer, ref string respuesta, ref RepuestaNotificacionEnpacto movimientosresp, ref string pError)
    {
        string sAux = "";
        // Validando        
        pError = "";
        JavaScriptSerializer ser = new JavaScriptSerializer();
        string datos = ser.Serialize(movimientos);
        string data = EncriptarDatos(datos, ref sAux);
        if (sAux != "") pError = sAux;

        // Consumir el webservices 
        responseFromServer = ConsumirWEBSERVICES("Http://" + strHost + "/webservice/enpactosvc", "POST", "conv=" + pConvenio + "&confirmar=true&data=" + data, timeout, ref sAux);
        if (sAux != "") pError = sAux;

        try
        {
            // Creamos el algoritmo encriptador
            Aes algoritmo = Aes.Create();
            ConfigurarAlgoritmo(algoritmo, Convert.ToInt32(padding), 128);
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
            return false;
        }
        return true;
    }

    /// <summary>
    /// Método para convertir el response de las notificaciones en una entidad
    /// </summary>
    /// <param name="pRespuesta"></param>
    /// <returns></returns>
    public RepuestaNotificacionEnpacto DeserializeNotificacion(string pRespuesta)
    {
        JavaScriptSerializer ser = new JavaScriptSerializer();
        RepuestaNotificacionEnpacto myNames = ser.Deserialize<RepuestaNotificacionEnpacto>(pRespuesta);
        return myNames;
    }

    #region clasesRespuestaENPACTO
    public class RepuestaNotificacionEnpacto
    {
        public List<transresp> trans { get; set; }
    }

    public class transresp
    {
        public string fecha { get; set; }
        public string hora { get; set; }
        public string secuencia { get; set; }
        public string tarjeta { get; set; }
        public string estado { get; set; }
    }


    public class NotificacionEnpacto
    {
        public List<trans> trans { get; set; }
    }

    public class trans
    {
        public string fecha { get; set; }
        public string hora { get; set; }
        public string secuencia { get; set; }
        public string tarjeta { get; set; }
    }
    #endregion

    /// <summary>
    /// Webservice “download” permite descargar el archivo de corte (movimientos, etc)
    /// </summary>
    /// <param name="pConvenio"></param>
    /// <param name="pFecha"></param>
    /// <returns></returns>
    public bool ArchivoMovimientosENPACTO(string pConvenio, string pFecha, ref string pProceso, ref string pError)
    {
        string sAux = "";
        // Validando        
        pError = "";
        pProceso = "\n" + "DATOS: " + "conv=" + pConvenio + "&fecha=" + pFecha;

        // Consumir el webservices 
        string responseFromServer = ConsumirWEBSERVICES("Http://" + strHost + "/webservice/download", "POST", "conv=" + pConvenio + "&fecha=" + pFecha, timeout, ref sAux);
        if (sAux != "") pError = sAux;
        pProceso += "\n" + responseFromServer;

        try
        {
            // Creamos el algoritmo encriptador
            Aes algoritmo = Aes.Create();
            ConfigurarAlgoritmo(algoritmo, Convert.ToInt32(padding), 128);
            GenerarClave(algoritmo);
            GenerarIV(algoritmo);

            // Desencriptamos los datos
            byte[] mensajeEncriptado;
            pProceso += "\n\n" + "RESPUESTA: ";
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
                pError += "\n" + ex.Message;
                return false;
            }
            pProceso += "\n==>" + respuesta;

            // Cerramos algoritmo
            algoritmo.Clear();

        }
        catch (Exception ex)
        {
            pError = ex.Message;
            return false;
        }
        return true;
    }

    /// <summary>
    /// Webservice “upload” permite cargar el archivo de saldos
    /// </summary>
    /// <param name="pConvenio"></param>
    /// <param name="pNomArchivo"></param>
    /// <param name="pArchivo"></param>
    /// <returns></returns>
    public bool ActualizacionSaldosENPACTO(string pConvenio, string pNomArchivo, StreamReader pArchivo, ref string pProceso, ref string pError)
    {
        string sAux = "";
        // Validando        
        pError = "";
        string data = "";
        string line = "";
        int counter = 0;
        while ((line = pArchivo.ReadLine()) != null)
        {
            data += line;
            counter++;
        }
        pProceso = "\n" + "DATOS: " + "conv=" + pConvenio + "&nombre=" + pNomArchivo + "&data=" + data;

        // Consumir el webservices 
        string responseFromServer = ConsumirWEBSERVICES("Http://" + strHost + "/webservice/upload", "POST", "conv=" + pConvenio + "&nombre=" + pNomArchivo + "&data=" + data, timeout, ref sAux);
        if (sAux != "") pError = sAux;
        pProceso += "\n" + responseFromServer;

        try
        {
            // Creamos el algoritmo encriptador
            Aes algoritmo = Aes.Create();
            ConfigurarAlgoritmo(algoritmo, Convert.ToInt32(padding), 128);
            GenerarClave(algoritmo);
            GenerarIV(algoritmo);

            // Desencriptamos los datos
            byte[] mensajeEncriptado;
            pProceso += "\n\n" + "RESPUESTA: ";
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
                pError += "\n" + ex.Message;
                return false;
            }
            pError += "\n==>" + respuesta;

            // Cerramos algoritmo
            algoritmo.Clear();

        }
        catch (Exception ex)
        {
            pError = ex.Message;
            return false;
        }
        return true;
    }

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
            if (!readLine.Contains("{" + strQuote + "estado" + strQuote + ":false,"))
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

    #region clasesRespuestaSICENPACTO
    public class archivoSIC
    {
        public string fecha { get; set; }
        public string hora { get; set; }
        public string documento { get; set; }
        public string nrocuenta { get; set; }
        public string tarjeta { get; set; }
        public string tipotransaccion { get; set; }
        public string descripcion { get; set; }
        public string monto { get; set; }
        public string comision { get; set; }
        public string lugar { get; set; }
        public string operacion { get; set; }
        public string red { get; set; }
    }

    #endregion

    public List<archivoSIC> CargarArchivo(StreamReader reader)
    {
        // Cargando los datos de los movimientos en un list.
        int i = 0;
        List<archivoSIC> lstArchivo = new List<archivoSIC>();
        while (reader.Peek() >= 0)
        {
            try
            {
                i += 1;
                string readLine = reader.ReadLine();
                string[] arrayline = readLine.Split(Convert.ToChar(";"));
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
            catch { }
        }
        return lstArchivo;
    }

    public bool ServicioCLIENTESENPACTO(string pConvenio, string pNomArchivo, string pRutYNomArchivo, string pActualizar_switch, ref string pProceso, ref string pError, ref RespuestaEnpactoClientes respuestaEntidad)
    {
        bool resultado = ServicioCLIENTESENPACTO(pConvenio, pNomArchivo, pRutYNomArchivo, pActualizar_switch, ref pProceso, ref pError);

        // Si la respuesta del servicio es valida y no hay errores, materializamos la entidad de respuesta para obtener las relaciones
        if (!string.IsNullOrWhiteSpace(pProceso) && string.IsNullOrWhiteSpace(pError))
        {
            respuestaEntidad = JsonConvert.DeserializeObject<RespuestaEnpactoClientes>(pProceso);
        }

        return resultado;
    }

    public bool ServicioCLIENTESENPACTO(string pConvenio, string pNomArchivo, string pRutYNomArchivo, ref string pProceso, ref string pError, ref RespuestaEnpactoClientes respuestaEntidad)
    {
        bool resultado = ServicioCLIENTESENPACTO(pConvenio, pNomArchivo, pRutYNomArchivo, ref pProceso, ref pError);

        // Si la respuesta del servicio es valida y no hay errores, materializamos la entidad de respuesta para obtener las relaciones
        if (!string.IsNullOrWhiteSpace(pProceso) && string.IsNullOrWhiteSpace(pError))
        {
            // Cargar respuesta del proceso
            try
            {  
                respuestaEntidad = JsonConvert.DeserializeObject<RespuestaEnpactoClientes>(pProceso);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
            }
        }

        return resultado;
    }


    public bool ServicioCLIENTESENPACTO(string pConvenio, string pNomArchivo, string pRutYNomArchivo, ref string pProceso, ref string pError)
    {
        return ServicioCLIENTESENPACTO(pConvenio, pNomArchivo, pRutYNomArchivo, "", ref pProceso, ref pError);
    }

    public bool ServicioCLIENTESENPACTO(string pConvenio, string pNomArchivo, string pRutYNomArchivo, string pActualizar_switch, ref string pProceso, ref string pError)
    {
        WebClient web = new WebClient();
        web.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
        NameValueCollection datos = new NameValueCollection();
        web.QueryString = datos;
        string url = "http://" + HostAppliance + "/webservice/clientes?usuario=" + usuarioAppliance + "&clave=" + claveAppliance + "&archivo=" + pNomArchivo + "&relacion=1" + (pActualizar_switch != "" ? ("&actualizar_switch=" + pActualizar_switch) : "");
        byte[] responseArray = web.UploadFile(url, "POST", pRutYNomArchivo);
        pProceso = System.Text.Encoding.ASCII.GetString(responseArray);

        return true;
    }


    /// <summary>
    /// Este método me permite realizar el consumo de un WEBSERVICES
    /// </summary>
    /// <param name="pUrl"></param>
    /// <param name="pMetodo"></param>
    /// <param name="requestXmlString"></param>
    /// <returns></returns>
    private string ConsumirWEBSERVICES(string pUrl, string pMetodo, string pRequestXmlString, int pTimeOut, ref string pError)
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
            ConfigurarAlgoritmo(algoritmo, Convert.ToInt32(padding), 128);
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
    public byte[] StringToByteArray(String hex)
    {
        int NumberChars = hex.Length;
        byte[] bytes = new byte[NumberChars / 2];
        for (int i = 0; i < NumberChars; i += 2)
            bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        return bytes;
    }

    /// <summary>
    /// Méotodo que determina los parámetros del algoritmo de encriptación. Se usa AES-CBC
    /// </summary>
    /// <param name="algoritmo"></param>
    /// <param name="padding"></param>
    private void ConfigurarAlgoritmo(Aes algoritmo, int padding, int longitudbloque)
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
    private void GenerarClave(Aes algoritmo)
    {
        algoritmo.Key = StringToByteArray(llave);
    }

    /// <summary>
    /// Determina el vector inicial
    /// </summary>
    /// <param name="algoritmo"></param>
    private void GenerarIV(Aes algoritmo)
    {
        algoritmo.IV = StringToByteArray(vector);
    }

    /// <summary>
    /// Método para encriptar los datos según configuración del algoritmo
    /// </summary>
    /// <param name="mensajeSinEncriptar"></param>
    /// <param name="algoritmo"></param>
    /// <returns></returns>
    public byte[] Encriptar(string mensajeSinEncriptar, Aes algoritmo)
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
    public byte[] Desencriptar(byte[] mensajeEncriptado, Aes algoritmo)
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

}


