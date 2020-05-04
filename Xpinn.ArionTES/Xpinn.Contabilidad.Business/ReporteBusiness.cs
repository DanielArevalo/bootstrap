using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Contabilidad.Data;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Business
{
    public class ReporteBusiness : GlobalBusiness
    {
        private ReporteData reporteData;

        public ReporteBusiness()
        {
            reporteData = new ReporteData();
        }

        public List<Reporte> ListarReporteInteresPagados(Usuario pUsuario, DateTime fechaini, DateTime fechafin)
        {
            try
            {
                return reporteData.ListarReporteInteresPagados(pUsuario, fechaini, fechafin);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ListarReporteInteresPagados", ex);
                return null;
            }
        }
       

    }
}
