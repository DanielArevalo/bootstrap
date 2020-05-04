using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ReporteMovimientosServices
    {
        private ReporteMovimientosBusiness BOAreasCaj;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para AreasCaj
        /// </summary>
        public ReporteMovimientosServices()
        {
            BOAreasCaj = new ReporteMovimientosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "40105"; } }

        public List<ReporteMovimientos> ListarReporteMovimientos(string filtro, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario)
        {
            try
            {
                return BOAreasCaj.ListarReporteMovimientos(filtro, pFechaIni, pFechaFin, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientosServices", "ListarReporteMovimientos", ex);
                return null;
            }
        }

    }
}