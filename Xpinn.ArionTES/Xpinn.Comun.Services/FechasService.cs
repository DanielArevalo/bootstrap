using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Comun.Entities;
using Xpinn.Comun.Business;
using System.Web;
namespace Xpinn.Comun.Services
{
    public class FechasService
    {
        private FechasBusiness BOFechas;
        private FechasBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para CierreHistorio
        /// </summary>
        public FechasService()
        {
            BOFechas = new FechasBusiness();
            BOExcepcion = new FechasBusiness();
        }

        public DateTime SumarMeses(DateTime fecha, int meses)
        {
            return BOFechas.SumarMeses(fecha, meses); 
        }

        public DateTime FecUltDia(DateTime f_fec_des)
        {
            return BOFechas.FecUltDia(f_fec_des);
        }

        public DateTime FecSumDia(DateTime fecha, int dias, int tipo_cal)
        {
            return BOFechas.FecSumDia(fecha,dias,tipo_cal);
        }

        public Int32? FecDifDia(DateTime fecha_ini, DateTime fecha_fin, int tipo_cal)
        {
            try
            {
             return BOFechas.FecDifDia(fecha_ini,fecha_fin,tipo_cal);
            }
           catch (Exception ex)
            {
               return null;
            }
        
        }

    }
}
