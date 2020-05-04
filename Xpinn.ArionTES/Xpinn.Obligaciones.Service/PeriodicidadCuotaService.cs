using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Obligaciones.Business;
using Xpinn.Obligaciones.Entities;

namespace Xpinn.Obligaciones.Services
{
    /// <summary>
    /// Servicios para Tipo Liquidacion
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class PeriodicidadCuotaService
    {
        private PeriodicidadCuotaBusiness BOPeriodicidadCuota;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para PeriodicidadCuota
        /// </summary>
        public PeriodicidadCuotaService()
        {
            BOPeriodicidadCuota = new PeriodicidadCuotaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        
        /// <summary>
        /// Servicio para obtener lista de Tipo de Liquidacion a partir de unos filtros
        /// </summary>
        /// <param name="pSolicitud">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PeriodicidadCuota obtenidos</returns>
        public List<PeriodicidadCuota> ListarPeriodicidadCuota(PeriodicidadCuota pTipLiq, Usuario pUsuario)
        {
            try
            {
                return BOPeriodicidadCuota.ListarPeriodicidadCuota(pTipLiq, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PeriodicidadCuotaService", "ListarPeriodicidadCuota", ex);
                return null;
            }
        }
    }
}
