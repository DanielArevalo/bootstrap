using System;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Data.Common;
using System.Web.Configuration;
using System.Globalization;

namespace Xpinn.Util
{
    public class Configuracion
    {
        /// <summary>
        /// Creador de objetos de la clase
        /// </summary>
        public Configuracion()
        {
            return;
         }

        /// <summary>
        /// Función que permite obtener el valor de un parámetro del WEB.CONFIG.
        /// </summary>
        /// <param name="llave"></param>
        /// <returns></returns>
        public string ObtenerValorConfig(string llave)
        {
            NameValueCollection appSettings = WebConfigurationManager.AppSettings as NameValueCollection;
            string valor = appSettings[llave];
            return valor;
        }

        public string ObtenerFormatoFecha()
        {
            return ObtenerValorConfig("FormatoFechaBase");
        }

        public string ObtenerSeparadorDecimalConfig()
        {            
            // char sseparador = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0];
            char sseparador = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator[0];
            if (char.Equals(sseparador, Convert.ToChar(".")))
                return ".";
            else
                return ",";
        }

        public string ObtenerSeparadorMilesConfig()
        {
            char sseparador = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator[0];
            if (char.Equals(sseparador, Convert.ToChar(".")))
                return ",";
            else
                return ".";
        }
    }
}