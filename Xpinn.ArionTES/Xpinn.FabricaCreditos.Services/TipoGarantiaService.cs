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
    public class TipoGarantiaService
    {
        private TipoGarantiaBusiness BOTiposGarantia;
        private ExcepcionBusiness BOExcepcion;

        public string CodigoPrograma { get { return "100144"; } }
    
        public int cod_garantia;

        /// <summary>
        /// Constructor del servicio para PlanCuentas
        /// </summary>
        public TipoGarantiaService()
        {
            BOTiposGarantia = new TipoGarantiaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<TipoGarantias> ListarTiposGarantia(TipoGarantias pgarantia, Usuario pUsuario)
        {
            try
            {
                return BOTiposGarantia.ListarTipoGarantia(pgarantia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoGarantiaService", "ListarTipoGarantia", ex);
                return null;
            }
        }
    }
}
