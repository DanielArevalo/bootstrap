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
    public class ArqueoPagosServices
    {
        private ArqueoPagosBusiness BOArqueo;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para AreasCaj
        /// </summary>
        public ArqueoPagosServices()
        {
            BOArqueo = new ArqueoPagosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "40106"; } }

        public List<ReporteMovimientos> ListarReporteMovimientos(string filtro, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario)
        {
            try
            {
                return BOArqueo.ListarReporteMovimientos(filtro, pFechaIni, pFechaFin, vUsuario);
        }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientosServices", "ListarReporteMovimientos", ex);
                return null;
            }
        }

    }
}