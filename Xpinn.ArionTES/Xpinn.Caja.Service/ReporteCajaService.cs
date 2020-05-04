using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Caja.Business;
using Xpinn.Caja.Entities;


namespace Xpinn.Caja.Services
{
    public class ReporteCajaService
    {
        private ReporteCajaBusiness BOAReporte;
        private ExcepcionBusiness BOExcepcion;

        public ReporteCajaService()
        {
            BOAReporte = new ReporteCajaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "120301"; } }


        public List<ReportesCaja> ListarReportemovdiario(Usuario pUsuario, Int64 codigo, DateTime fechaini, DateTime fechafinal)
        {
            try
            {
                return BOAReporte.ListarReportemovdiario(pUsuario, codigo, fechaini, fechafinal);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteCajaService", "ListarReportemovdiario", ex);
                return null;
            }
        }
      
    }
}

