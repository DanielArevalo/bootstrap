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
    public class ArqueoPagosBusiness : GlobalBusiness
    {
        private ArqueoPagosData DAArqueo;

        /// <summary>
        /// Constructor del objeto de negocio para AreasCaj
        /// </summary>
        public ArqueoPagosBusiness()
        {
            DAArqueo = new ArqueoPagosData();
        }

        public List<ReporteMovimientos> ListarReporteMovimientos(string filtro, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario)
        {
            try
            {
                return DAArqueo.ListarReporteMovimientos(filtro, pFechaIni, pFechaFin, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientosBusiness", "ListarReporteMovimientos", ex);
                return null;
            }
        }

    }
}