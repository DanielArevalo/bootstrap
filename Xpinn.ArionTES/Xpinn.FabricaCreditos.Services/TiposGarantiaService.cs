using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;
using System.Data;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicio para Garantias
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TiposGarantiaService
    {
        private TipoGarantiaBusiness BOTiposGarantia;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Caja
        /// </summary>
        public TiposGarantiaService()
        {
            BOTiposGarantia = new TipoGarantiaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<TipoGarantias> ListarTiposGarantia(TipoGarantias garantia, Usuario pUsuario)
        {
            try
            {
                return BOTiposGarantia.ListarTipoGarantia(garantia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposGarantiaService", "ListarTipoGarantia", ex);
                return null;
            }
        }
    }
}
