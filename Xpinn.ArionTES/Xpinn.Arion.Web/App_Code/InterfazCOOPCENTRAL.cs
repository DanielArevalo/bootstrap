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
using System.Xml.Serialization;
using System.Xml.Linq;
using Xpinn.TarjetaDebito.Entities;

/// <summary>
/// Esta clase se usa para procesar las transacciones con el switch autorizador de COOPCENTRAL
/// </summary>
public class InterfazCOOPCENTRAL
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

    public InterfazCOOPCENTRAL(string pLlave, string pVector)
    {
        GlobalWeb global = new GlobalWeb();
        HostAppliance = global.IpApplianceConvenioTarjeta();
        strHost = global.IpSwitchConvenioTarjeta();
        llave = pLlave;
        vector = pVector;
        padding = "4";
    }

    public InterfazCOOPCENTRAL(string pLlave, string pVector, int pTimeOut)
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
            if (!(pDatos.tipo_cuenta == "10") && !(pDatos.tipo_cuenta == "50"))
            {
                pError += "El tipo de cuenta debe ser 10, 50";
                bCorrecto = false;
            }
        }

        // Si no cumple las validaciones
        if (!bCorrecto)
            return false;

        // Determinando estructura XML segùn el tipo de transacciòn
        string datos = /*"<?xml version=" + strQuote + "1.0" + strQuote + "?>" +*/ "<root>";       
        if (pDatos.tipo == "0")                 // Registrar datos del cliente
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
        else if (pDatos.tipo == "1")        // Bloquear tarjeta 
        {
            datos += "<tarjeta>" + pDatos.tarjeta + "</tarjeta>" +
                     "<documento>" + pDatos.identificacion + "</documento>" +
                     "<tipo_documento>" + pDatos.tipo_identificacion + "</tipo_documento>" +
                     "<bloqueo>" + pDatos.bloqueo + "</bloqueo>";
        }
        else if (pDatos.tipo == "3")        // Retiro
        {
            datos += "<documento>" + pDatos.identificacion + "</documento>" +
                     "<cuenta>" + pDatos.cuenta + "</cuenta>" +
                     "<tipo_cuenta>" + pDatos.tipo_cuenta + "</tipo_cuenta>" +
                     "<monto>" + pDatos.monto + "</monto>";
        }
        else if (pDatos.tipo == "4")        // Consignación
        {
            datos += "<documento>" + pDatos.identificacion + "</documento>" +
                     "<cuenta>" + pDatos.cuenta + "</cuenta>" +
                     "<tipo_cuenta>" + pDatos.tipo_cuenta + "</tipo_cuenta>" +
                     "<monto>" + pDatos.monto + "</monto>";
        }
        else if (pDatos.tipo == "5")        // Consulta de saldo
        {
            datos += "<documento>" + pDatos.identificacion + "</documento>" +
                     "<cuenta>" + pDatos.cuenta + "</cuenta>";
        }
        else if (pDatos.tipo == "6")        // Borrar relación tarjeta cuenta (Cambio tarjeta)
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
        string responseFromServer = ConsumirWEBSERVICES("Http://" + strHost + "/webservice", "POST", requestXmlString, timeout, ref sAux);
        if (sAux != "") pError += sAux;

        try
        {            
            pRespuesta = DeserializeTran(responseFromServer);
            if (pRespuesta != null)
            {
                pRespuesta.datos = datos;
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
    /// Método para convertir el response del webservices que esta en XML en una entidad
    /// </summary>
    /// <param name="pRespuesta"></param>
    /// <returns></returns>
    public RespuestaCoopcentral DeserializeTran(string pRespuesta)
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

    public bool Consulta(string pConvenio, string pFecha, string pHora, string pSecuencia, string pCuenta, string pTarjeta, ref RespuestaCoopcentral pRespuesta, ref string pError)
    {
        TransaccionCoopcentral transaccion = new TransaccionCoopcentral();
        transaccion.tipo = "0";
        transaccion.fecha = pFecha;
        transaccion.hora = pHora;
        transaccion.secuencia = pSecuencia;
        transaccion.cuenta = pCuenta;
        transaccion.tarjeta = pTarjeta;
        transaccion.monto = "0";
        return GenerarTransaccionCOOPCENTRAL(pConvenio, transaccion, false, ref pRespuesta, ref pError);
    }

    public bool Retiro(string pConvenio, string pFecha, string pHora, string pSecuencia, string pCuenta, string pTarjeta, decimal pMonto, bool pReverso, ref RespuestaCoopcentral pRespuesta, ref string pError)
    {
        TransaccionCoopcentral transaccion = new TransaccionCoopcentral();
        transaccion.tipo = "1";
        transaccion.fecha = pFecha;
        transaccion.hora = pHora;
        transaccion.secuencia = pSecuencia;
        transaccion.cuenta = pCuenta;
        transaccion.tarjeta = pTarjeta;
        transaccion.monto = Convert.ToString(Math.Truncate(pMonto * 100));
        return GenerarTransaccionCOOPCENTRAL(pConvenio, transaccion, pReverso, ref pRespuesta, ref pError);
    }

    public bool Consignacion(string pConvenio, string pFecha, string pHora, string pSecuencia, string pCuenta, string pTarjeta, decimal pMonto, bool pReverso, ref RespuestaCoopcentral pRespuesta, ref string pError)
    {
        TransaccionCoopcentral transaccion = new TransaccionCoopcentral();
        transaccion.tipo = "3";
        transaccion.fecha = pFecha;
        transaccion.hora = pHora;
        transaccion.secuencia = pSecuencia;
        transaccion.cuenta = pCuenta;
        transaccion.tarjeta = pTarjeta;
        transaccion.monto = Convert.ToString(Math.Truncate(pMonto * 100));
        return GenerarTransaccionCOOPCENTRAL(pConvenio, transaccion, pReverso, ref pRespuesta, ref pError);
    }

    public bool Bloqueo(string pConvenio, string pFecha, string pHora, string pSecuencia, string pCuenta, string pTarjeta, ref RespuestaCoopcentral pRespuesta, ref string pError)
    {
        TransaccionCoopcentral transaccion = new TransaccionCoopcentral();
        transaccion.tipo = "B";
        transaccion.fecha = pFecha;
        transaccion.hora = pHora;
        transaccion.secuencia = pSecuencia;
        transaccion.cuenta = pCuenta;
        transaccion.tarjeta = pTarjeta;
        transaccion.monto = "0";
        return GenerarTransaccionCOOPCENTRAL(pConvenio, transaccion, false, ref pRespuesta, ref pError);
    }

    public bool DesBloqueo(string pConvenio, string pFecha, string pHora, string pSecuencia, string pCuenta, string pTarjeta, ref RespuestaCoopcentral pRespuesta, ref string pError)
    {
        TransaccionCoopcentral transaccion = new TransaccionCoopcentral();
        transaccion.tipo = "D";
        transaccion.fecha = pFecha;
        transaccion.hora = pHora;
        transaccion.secuencia = pSecuencia;
        transaccion.cuenta = pCuenta;
        transaccion.tarjeta = pTarjeta;
        transaccion.monto = "0";
        return GenerarTransaccionCOOPCENTRAL(pConvenio, transaccion, false, ref pRespuesta, ref pError);
    }

    public class archivoSIC : MovimientoCoopcentral { };

    #endregion Webservice “transacción”


    #region WebServices para actualizaciòn de clientes

    public bool ServicioCLIENTESCOOPCENTRAL(string pConvenio, string pNombreArchivo, StreamReader pArchivo, ref RespuestaCoopcentral pRespuesta, ref string pError)
    {
        string sAux = "";
        pError = "";
        string requestXmlString = "";
        // Leer el archivo
        string data = "";
        string line = "";
        int counter = 0;
        while ((line = pArchivo.ReadLine()) != null)
        {
            data += line;
            counter++;
        }
        // Determinar paràmetros
        requestXmlString = "conv=" + pConvenio + "&filename=" + pNombreArchivo + "&data=" + EncriptarDatos(data, ref sAux);
        if (sAux != "") pError += sAux;
        // Consumir el webservices 
        string responseFromServer = ConsumirWEBSERVICES("Http://" + strHost + "/webservice", "POST", requestXmlString, timeout, ref sAux);
        if (sAux != "") pError += sAux;

        // Si la respuesta del servicio es valida y no hay errores, materializamos la entidad de respuesta para obtener las relaciones
        if (!string.IsNullOrWhiteSpace(requestXmlString))
        {
            // Cargar respuesta del proceso
            try
            {
                pRespuesta = DeserializeTran(responseFromServer);
                if (pRespuesta != null)
                {
                    pRespuesta.datos = data;
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
                pError = ex.Message;
                return false;
            }
        }

        return true;
    }


    #endregion


    #region consumo del webservice COOPCENTRAL
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

    #endregion


    
}


