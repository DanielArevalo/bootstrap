using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Web.Services2.Security;
using Microsoft.Web.Services2.Security.Tokens;
using Microsoft.Web.Services2;
using Microsoft.Web.Services2.Security.X509;
using System.Xml;
using Xpinn.Util;
using System.Configuration;

#region Clases Mapeo Cifin

public class ConsultaCifin
{
    public Cifin Cifin { get; set; }
}

public class Cifin
{
    public string archivo { get; set; }
    public Tercero Tercero { get; set; }
}

public class Tercero
{
    public string IdentificadorLinea { get; set; }
    public string TipoIdentificacion { get; set; }
    public string CodigoTipoIndentificacion { get; set; }
    public string NumeroIdentificacion { get; set; }
    public string NombreTitular { get; set; }
    public string NombreCiiu { get; set; }
    public string LugarExpedicion { get; set; }
    public string FechaExpedicion { get; set; }
    public string Estado { get; set; }
    public string NumeroInforme { get; set; }
    public string EstadoTitular { get; set; }
    public string RangoEdad { get; set; }
    public string CodigoCiiu { get; set; }
    public string CodigoDepartamento { get; set; }
    public string CodigoMunicipio { get; set; }
    public string Fecha { get; set; }
    public string Hora { get; set; }
    public string Entidad { get; set; }
    public string RespuestaConsulta { get; set; }
    public Score Score { get; set; }
    public Consolidado Consolidado { get; set; }
    public CuentasVigentes CuentasVigentes { get; set; }
    public CuentasExtinguidas CuentasExtinguidas { get; set; }
    public SectorFinancieroAlDia SectorFinancieroAlDia { get; set; }
    public SectorFinancieroEnMora SectorFinancieroEnMora { get; set; }
    public SectorFinancieroExtinguidas SectorFinancieroExtinguidas { get; set; }
    public SectorRealAlDia SectorRealAlDia { get; set; }
    public SectorRealEnMora SectorRealEnMora { get; set; }
    public SectorRealExtinguidas SectorRealExtinguidas { get; set; }
}

public class Score
{
    public string IndicadorScore { get; set; }
    public string TipoScore { get; set; }
    public string CodigoScore { get; set; }
    public string Puntaje { get; set; }
    public string TasaMorosidad { get; set; }
    public string IndicadorDefault { get; set; }
    public string SubPoblacion { get; set; }
    public string Politica { get; set; }
    public string Observacion { get; set; }
}

public class Consolidado
{
    public Resumen ResumenPrincipal { get; set; }
    public Resumen ResumenDiferentePrincipal { get; set; }
    [JsonProperty("Registro")]
    public Registro RegistroTotal { get; set; }
}

[JsonConverter(typeof(RegistroConverter))]
public class Resumen : IRegistreable
{
    public List<Registro> Registro { get; set; }
}

public class Registro
{
    public string PaqueteInformacion { get; set; }
    public string NumeroObligaciones { get; set; }
    public string TotalSaldo { get; set; }
    public string ParticipacionDeuda { get; set; }
    public string NumeroObligacionesDia { get; set; }
    public string SaldoObligacionesDia { get; set; }
    public string CuotaObligacionesDia { get; set; }
    public string CantidadObligacionesMora { get; set; }
    public string SaldoObligacionesMora { get; set; }
    public string CuotaObligacionesMora { get; set; }
    public string ValorMora { get; set; }
    public string CuotaTotalObligaciones { get; set; }
}

[JsonConverter(typeof(ObligacionConverter))]
public class CuentasVigentes : IObligacionable
{
    public List<Obligacion> Obligacion { get; set; }
}

[JsonConverter(typeof(ObligacionConverter))]
public class CuentasExtinguidas : IObligacionable
{
    public List<Obligacion> Obligacion { get; set; }
}

[JsonConverter(typeof(ObligacionConverter))]
public class SectorFinancieroAlDia : IObligacionable
{
    public List<Obligacion> Obligacion { get; set; }
}

[JsonConverter(typeof(ObligacionConverter))]
public class SectorFinancieroEnMora : IObligacionable
{
    public List<Obligacion> Obligacion { get; set; }
}

[JsonConverter(typeof(ObligacionConverter))]
public class SectorFinancieroExtinguidas : IObligacionable
{
    public List<Obligacion> Obligacion { get; set; }
}

[JsonConverter(typeof(ObligacionConverter))]
public class SectorRealAlDia : IObligacionable
{
    public List<Obligacion> Obligacion { get; set; }
}

[JsonConverter(typeof(ObligacionConverter))]
public class SectorRealEnMora : IObligacionable
{
    public List<Obligacion> Obligacion { get; set; }
}

[JsonConverter(typeof(ObligacionConverter))]
public class SectorRealExtinguidas : IObligacionable
{
    public List<Obligacion> Obligacion { get; set; }
}

public class Obligacion
{
    [JsonConverter(typeof(PaqueteInformacionConverter))]
    public string PaqueteInformacion { get; set; }
    public string IdentificadorLinea { get; set; }
    public string TipoContrato { get; set; }
    public string EstadoContrato { get; set; }
    public string TipoEntidad { get; set; }
    public string NombreEntidad { get; set; }
    public string Ciudad { get; set; }
    public string Sucursal { get; set; }
    public string NumeroObligacion { get; set; }
    public string Calidad { get; set; }
    public string EstadoObligacion { get; set; }
    public string ModalidadCredito { get; set; }
    public string LineaCredito { get; set; }
    public string Periodicidad { get; set; }
    public string FechaApertura { get; set; }
    public string FechaTerminacion { get; set; }
    public string Calificacion { get; set; }
    public string ValorInicial { get; set; }
    public string SaldoObligacion { get; set; }
    public string ValorMora { get; set; }
    public string ValorCuota { get; set; }
    public string TipoMoneda { get; set; }
    public string CuotasCanceladas { get; set; }
    public string TipoGarantia { get; set; }
    public string CubrimientoGarantia { get; set; }
    public string MoraMaxima { get; set; }
    public string Comportamientos { get; set; }
    public string ParticipacionDeuda { get; set; }
    public string ProbabilidadNoPago { get; set; }
    public string FechaCorte { get; set; }
    public string ModoExtincion { get; set; }
    public string FechaPago { get; set; }
    public string FechaPermanencia { get; set; }
    public string NumeroReestructuraciones { get; set; }
    public string NaturalezaReestructuracion { get; set; }
    public string MarcaTarjeta { get; set; }
    public string ClaseTarjeta { get; set; }
    public string TipoFideicomiso { get; set; }
    public string TipoEntidadOriginadoraCartera { get; set; }
    public string EntidadOriginadoraCartera { get; set; }
    public string NumeroFideicomiso { get; set; }
    public string ChequesDevueltos { get; set; }
    public string Plazo { get; set; }
    public string DiasCartera { get; set; }
    public string TipoPago { get; set; }
    public string EstadoTitular { get; set; }
    public string NumeroCuotasPactadas { get; set; }
    public string NumeroCuotasMora { get; set; }
    public string ValorCargoFijo { get; set; }
    public string ClausulaPermanencia { get; set; }
    public string Reestructurado { get; set; }
    public string Vigencia { get; set; }
    public string NumeroMesesContrato { get; set; }
    public string NumeroMesesClausula { get; set; }
}

public interface IObligacionable
{
    List<Obligacion> Obligacion { get; set; }
}

public interface IRegistreable
{
    List<Registro> Registro { get; set; }
}

public class ObligacionConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        IObligacionable instancia = (IObligacionable)Activator.CreateInstance(objectType);

        JToken jo = JObject.Load(reader)["Obligacion"];

        if (jo is JObject)
        {
            JObject objetoSolo = jo as JObject;
            instancia.Obligacion = new List<Obligacion> { jo.ToObject<Obligacion>() };
        }
        else if (jo is JArray)
        {
            JArray objetoSolo = jo as JArray;
            instancia.Obligacion = objetoSolo.Children().Select(x => x.ToObject<Obligacion>()).ToList();
        }

        return instancia;
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType.GetInterfaces().Contains(typeof(IObligacionable));
    }
}

public class RegistroConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        IRegistreable instancia = (IRegistreable)Activator.CreateInstance(objectType);

        JToken jo = JObject.Load(reader)["Registro"];

        if (jo is JObject)
        {
            JObject objetoSolo = jo as JObject;
            instancia.Registro = new List<Registro> { jo.ToObject<Registro>() };
        }
        else if (jo is JArray)
        {
            JArray objetoSolo = jo as JArray;
            instancia.Registro = objetoSolo.Children().Select(x => x.ToObject<Registro>()).ToList();
        }

        return instancia;
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType.GetInterfaces().Contains(typeof(IRegistreable));
    }
}

public class PaqueteInformacionConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        string value = reader.Value.ToString();
        return InterfazCIFIN.HomologarCodigoPaqueteInformacionADescripcion(value);
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(string);
    }
}

#endregion

/// <summary>
/// Summary description for InterfazCIFIN
/// </summary>
public class InterfazCIFIN
{

    #region Clase Servicio Cifin

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "InformacionComercialSoapBinding", Namespace = "http://infocomercial.cifin.asobancaria.com")]
    public partial class InformacionComercialWSService : Microsoft.Web.Services2.WebServicesClientProtocol
    {
        private System.Threading.SendOrPostCallback consultaXmlOperationCompleted;

        private System.Threading.SendOrPostCallback consultaPlanoOperationCompleted;

        private System.Threading.SendOrPostCallback cambioPasswordOperationCompleted;

        private bool useDefaultCredentialsSetExplicitly;

        /// <remarks/>
        public InformacionComercialWSService()
        {
            this.Url = "https://cifinpruebas.asobancaria.com/InformacionComercialWS/services/InformacionComercial";
            if ((this.IsLocalFileSystemWebService(this.Url) == true))
            {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else
            {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }

        public new string Url
        {
            get
            {
                return base.Url;
            }
            set
            {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true)
                            && (this.useDefaultCredentialsSetExplicitly == false))
                            && (this.IsLocalFileSystemWebService(value) == false)))
                {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }

        public new bool UseDefaultCredentials
        {
            get
            {
                return base.UseDefaultCredentials;
            }
            set
            {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }

        /// <remarks/>
        public event consultaXmlCompletedEventHandler consultaXmlCompleted;

        /// <remarks/>
        public event consultaPlanoCompletedEventHandler consultaPlanoCompleted;

        /// <remarks/>
        public event cambioPasswordCompletedEventHandler cambioPasswordCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://infocomercial.cifin.asobancaria.com", ResponseNamespace = "http://infocomercial.cifin.asobancaria.com")]
        [return: System.Xml.Serialization.SoapElementAttribute("consultaXmlReturn")]
        public string consultaXml(ParametrosConsultaDTO parametrosConsulta)
        {
            object[] results = this.Invoke("consultaXml", new object[] {
                        parametrosConsulta});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void consultaXmlAsync(ParametrosConsultaDTO parametrosConsulta)
        {
            this.consultaXmlAsync(parametrosConsulta, null);
        }

        /// <remarks/>
        public void consultaXmlAsync(ParametrosConsultaDTO parametrosConsulta, object userState)
        {
            if ((this.consultaXmlOperationCompleted == null))
            {
                this.consultaXmlOperationCompleted = new System.Threading.SendOrPostCallback(this.OnconsultaXmlOperationCompleted);
            }
            this.InvokeAsync("consultaXml", new object[] {
                        parametrosConsulta}, this.consultaXmlOperationCompleted, userState);
        }

        private void OnconsultaXmlOperationCompleted(object arg)
        {
            if ((this.consultaXmlCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.consultaXmlCompleted(this, new consultaXmlCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://infocomercial.cifin.asobancaria.com", ResponseNamespace = "http://infocomercial.cifin.asobancaria.com")]
        [return: System.Xml.Serialization.SoapElementAttribute("consultaPlanoReturn")]
        public string consultaPlano(ParametrosConsultaDTO parametrosConsulta)
        {
            object[] results = this.Invoke("consultaPlano", new object[] {
                        parametrosConsulta});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void consultaPlanoAsync(ParametrosConsultaDTO parametrosConsulta)
        {
            this.consultaPlanoAsync(parametrosConsulta, null);
        }

        /// <remarks/>
        public void consultaPlanoAsync(ParametrosConsultaDTO parametrosConsulta, object userState)
        {
            if ((this.consultaPlanoOperationCompleted == null))
            {
                this.consultaPlanoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnconsultaPlanoOperationCompleted);
            }
            this.InvokeAsync("consultaPlano", new object[] {
                        parametrosConsulta}, this.consultaPlanoOperationCompleted, userState);
        }

        private void OnconsultaPlanoOperationCompleted(object arg)
        {
            if ((this.consultaPlanoCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.consultaPlanoCompleted(this, new consultaPlanoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapRpcMethodAttribute("", RequestNamespace = "http://infocomercial.cifin.asobancaria.com", ResponseNamespace = "http://infocomercial.cifin.asobancaria.com")]
        [return: System.Xml.Serialization.SoapElementAttribute("cambioPasswordReturn")]
        public string cambioPassword(string nuevoPassword)
        {
            object[] results = this.Invoke("cambioPassword", new object[] {
                        nuevoPassword});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void cambioPasswordAsync(string nuevoPassword)
        {
            this.cambioPasswordAsync(nuevoPassword, null);
        }

        /// <remarks/>
        public void cambioPasswordAsync(string nuevoPassword, object userState)
        {
            if ((this.cambioPasswordOperationCompleted == null))
            {
                this.cambioPasswordOperationCompleted = new System.Threading.SendOrPostCallback(this.OncambioPasswordOperationCompleted);
            }
            this.InvokeAsync("cambioPassword", new object[] {
                        nuevoPassword}, this.cambioPasswordOperationCompleted, userState);
        }

        private void OncambioPasswordOperationCompleted(object arg)
        {
            if ((this.cambioPasswordCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.cambioPasswordCompleted(this, new cambioPasswordCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }

        private bool IsLocalFileSystemWebService(string url)
        {
            if (((url == null)
                        || (url == string.Empty)))
            {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024)
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0)))
            {
                return true;
            }
            return false;
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1590.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace = "http://dto.infocomercial.cifin.asobancaria.com")]
    public partial class ParametrosConsultaDTO
    {

        private string codigoInformacionField;

        private string motivoConsultaField;

        private string numeroIdentificacionField;

        private string tipoIdentificacionField;

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string codigoInformacion
        {
            get
            {
                return this.codigoInformacionField;
            }
            set
            {
                this.codigoInformacionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string motivoConsulta
        {
            get
            {
                return this.motivoConsultaField;
            }
            set
            {
                this.motivoConsultaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string numeroIdentificacion
        {
            get
            {
                return this.numeroIdentificacionField;
            }
            set
            {
                this.numeroIdentificacionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string tipoIdentificacion
        {
            get
            {
                return this.tipoIdentificacionField;
            }
            set
            {
                this.tipoIdentificacionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    public delegate void consultaXmlCompletedEventHandler(object sender, consultaXmlCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class consultaXmlCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal consultaXmlCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    public delegate void consultaPlanoCompletedEventHandler(object sender, consultaPlanoCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class consultaPlanoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal consultaPlanoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    public delegate void cambioPasswordCompletedEventHandler(object sender, cambioPasswordCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1590.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class cambioPasswordCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal cambioPasswordCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    #endregion

    // NO QUITARME MIS VERIFICACIONES, PIDE TU VAINA BIEN SI QUIERES USAR CIFIN
    // SI NO USA TUS TRY CATCH O VALIDA ANTES DE PEDIRME ALGO
    // :DDDDDDD
    public ConsultaCifin ConsultarCifin(string numeroIdentificacion, string tipoIdentificacion)
    {
        if (string.IsNullOrWhiteSpace(numeroIdentificacion)) throw new ArgumentException("No puedes consultar una identificación vacia en Cifin!.");
        if (string.IsNullOrWhiteSpace(tipoIdentificacion)) throw new ArgumentException("No puedes consultar una identificación sin tipo de identificacion en Cifin!.");

        ParametrosConsultaDTO entidad = new ParametrosConsultaDTO();
        InformacionComercialWSService servicio = new InformacionComercialWSService();

        X509CertificateStore store = X509CertificateStore.LocalMachineStore(X509CertificateStore.MyStore);

        try
        {
            entidad.codigoInformacion = "2042";
            entidad.motivoConsulta = "9";
            entidad.numeroIdentificacion = numeroIdentificacion;
            entidad.tipoIdentificacion = tipoIdentificacion;

            bool open = store.OpenRead();

            if (!open) throw new Exception("No se pudo abrir el certification store!.");

            if (store.Certificates.Count == 0) throw new Exception("No se encontró el certificado!.");

            X509Certificate cert = store.Certificates[0];

            if (!cert.SupportsDigitalSignature) throw new Exception("El certificado no soporta digital signature o no tiene key!.");

            SoapContext requestContext = servicio.RequestSoapContext;
            X509SecurityToken signatureToken = new X509SecurityToken(cert);
            requestContext.Security.Tokens.Add(signatureToken);

            MessageSignature sig = new MessageSignature(signatureToken);
            requestContext.Security.Elements.Add(sig);
            requestContext.Security.Timestamp.TtlInSeconds = 60;

            CifradoBusiness cifradoBuss = new CifradoBusiness();

            string usuarioEncrypted = ConfigurationManager.AppSettings["usuarioCifin"];
            if (string.IsNullOrWhiteSpace(usuarioEncrypted)) throw new Exception("No se encontro el usuario correspondiente de Cifin");
            string usuario = cifradoBuss.Desencriptar(usuarioEncrypted);

            string claveEncrypted = ConfigurationManager.AppSettings["claveCifin"];
            if (string.IsNullOrWhiteSpace(claveEncrypted)) throw new Exception("No se encontro la contraseña correspondiente de Cifin");
            string clave = cifradoBuss.Desencriptar(claveEncrypted);

            servicio.Credentials = new System.Net.NetworkCredential(usuario, clave);
            servicio.ClientCertificates.Add(cert);

            string resultado = servicio.consultaXml(entidad);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(resultado);

            string json = JsonConvert.SerializeXmlNode(doc);

            ConsultaCifin cifin = JsonConvert.DeserializeObject<ConsultaCifin>(json);

            return cifin;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            if (store != null) store.Close();
        }
    }

    public static string HomologarCodigoPaqueteInformacionADescripcion(string codigoPaqueteInformacion)
    {
        string descripcion = string.Empty;

        if (codigoPaqueteInformacion == "0001")
        {
            descripcion = "Cuenta corriente";
        }
        else if (codigoPaqueteInformacion == "0023")
        {
            descripcion = "Cuenta de ahorro";
        }
        else if (codigoPaqueteInformacion == "0002")
        {
            descripcion = "Tarjeta de crédito";
        }
        else if (codigoPaqueteInformacion == "0006")
        {
            descripcion = "Cartera total";
        }
        else if (codigoPaqueteInformacion == "0011")
        {
            descripcion = "Sector real - cartera servicios";
        }
        else if (codigoPaqueteInformacion == "0012")
        {
            descripcion = "Sector real - cartera comercio o prestamos";
        }
        else if (codigoPaqueteInformacion == "0013")
        {
            descripcion = "Cartera total - operaciones leasing";
        }
        else if (codigoPaqueteInformacion == "0014")
        {
            descripcion = "Cartera total - sector fiduciario";
        }
        else if (codigoPaqueteInformacion == "0019")
        {
            descripcion = "Sector asegurador cartera";
        }
        else if (codigoPaqueteInformacion == "0021")
        {
            descripcion = "Sector solidario – cooperativo";
        }
        else if (codigoPaqueteInformacion == "0005")
        {
            descripcion = "Endeudamiento Global Cortes y conteos";
        }
        else
        {
            descripcion = codigoPaqueteInformacion;
        }

        return descripcion;
    }
}