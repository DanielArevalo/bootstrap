using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.TarjetaDebito.Entities;
using Xpinn.TarjetaDebito.Services;
using Xpinn.Ahorros.Entities;
using Xpinn.Ahorros.Services;
using Xpinn.Util;


namespace WebServices
{
    /// <summary>
    /// Summary description for wsListarMovimientosEnpacto
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsListarMovimientosEnpacto : System.Web.Services.WebService
    {

        [WebMethod]
        public List<InterfazENPACTO.archivoSIC> ListadoMovimientos(string s_convenio, DateTime f_fecha, string s_IpApplianceConvenioTarjeta, string s_usuario_applicance, string s_clave_appliance, ref string s_respuesta, ref string s_error)
        {
            s_respuesta = "";
            s_error = "";

            List<InterfazENPACTO.archivoSIC> lstArchivo = new List<InterfazENPACTO.archivoSIC>();

            InterfazENPACTO interfazEnpacto;
            interfazEnpacto = new InterfazENPACTO("0123456789ABCDEFFEDCBA9876543210", "00000000000000000000000000000000");
            interfazEnpacto.ConfiguracionAppliance(s_IpApplianceConvenioTarjeta, s_usuario_applicance, s_clave_appliance);
            string error = "";
            try
            {
                string pRequestXmlString = "";
                if (!interfazEnpacto.ServicioSICENPACTO(f_fecha, s_convenio, ref pRequestXmlString, ref lstArchivo, ref error))
                {
                    s_error = "Se presento error." + error + " " + pRequestXmlString;
                    return null;
                }
                if (lstArchivo.Count <= 0)
                {
                    s_respuesta = pRequestXmlString;
                }
                s_error = error;
            }
            catch (Exception ex)
            {
                s_error = "No se pudo leer el archivo. Error:" + ex.Message + " " + error;
                return null;
            }

            return lstArchivo;
        }
        

    }
}