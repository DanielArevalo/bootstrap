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
    /// Servicio para PlanCuentas
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class PlanCuentasService
    {
        private PlanCuentasBusiness BOPlanCuentas;
        private ExcepcionBusiness BOExcepcion;

        public string CodigoPrograma { get { return "100144"; } }
    
        public int cod_cuenta;

        /// <summary>
        /// Constructor del servicio para PlanCuentas
        /// </summary>
        public PlanCuentasService()
        {
            BOPlanCuentas = new PlanCuentasBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<PlanCuentas> ListarPlanCuentas(PlanCuentas plancuentas, Usuario pUsuario)
        {
            try
            {
                return BOPlanCuentas.ListarPlanCuentas(plancuentas, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasService", "ListarPlanCuentas", ex);
                return null;
            }
        }


    }
}
