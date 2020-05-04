using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Business
{
    /// <summary>
    /// Objeto de negocio para AreasCaj
    /// </summary>
    public class ReporteMovimientosBusiness : GlobalBusiness
    {
        private ReporteMovimientoData DAReporte;

        /// <summary>
        /// Constructor del objeto de negocio para AreasCaj
        /// </summary>
        public ReporteMovimientosBusiness()
        {
            DAReporte = new ReporteMovimientoData();
        }

        public List<ReporteMovimientos> ListarReporteMovimientos(string filtro, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario)
        {
            try
            {
                return DAReporte.ListarReporteMovimientos(filtro, pFechaIni, pFechaFin, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientosBusiness", "ListarReporteMovimientos", ex);
                return null;
            }
        }

    }
}