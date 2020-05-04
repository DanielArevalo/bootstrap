using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Caja.Data;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Business
{
    public class ReporteCajaBusiness : GlobalBusiness
    {
        private ReportesCajaData reporteData;

        public ReporteCajaBusiness()
        {
            reporteData = new ReportesCajaData();
        }

        public List<ReportesCaja> ListarReportemovdiario(Usuario pUsuario, Int64 codigo, DateTime fechaini, DateTime fechafinal)
        {
            try
            {
                return reporteData.ListarReportemovdiario(pUsuario, codigo, fechaini, fechafinal);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReportesCajaBusiness", "ListarReportemovdiario", ex);
                return null;
            }
        }
     
    }
}
