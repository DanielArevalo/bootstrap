using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Contabilidad.Business;
using Xpinn.Contabilidad.Entities;
using Xpinn.NIIF.Entities;

namespace Xpinn.Contabilidad.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class PlanCuentasImpuestoService
    {
        private PlanCuentasImpuestoBusiness BOPlanCuentas;
        private ExcepcionBusiness BOExcepcion;

        
        public PlanCuentasImpuestoService()
        {
            BOPlanCuentas = new PlanCuentasImpuestoBusiness();
            BOExcepcion = new ExcepcionBusiness();            
        }

        public void EliminarPlanCuentaImpuesto(Int32 pId, Usuario vUsuario)
        {
            try
            {
                BOPlanCuentas.EliminarPlanCuentaImpuesto(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasServices", "EliminarPlanCuentaImpuesto", ex);
                return;
            }
        }


        public List<PlanCuentasImpuesto> ListarPlanCuentasImpuesto(PlanCuentasImpuesto pImpu, string filtro, Usuario vUsuario)
        {
            try
            {
                return BOPlanCuentas.ListarPlanCuentasImpuesto(pImpu,filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasBusiness", "ListarPlanCuentasImpuesto", ex);
                return null;
            }
        }

    }
}