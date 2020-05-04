using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Contabilidad.Data;
using Xpinn.Contabilidad.Entities;
using Xpinn.NIIF.Entities;

namespace Xpinn.Contabilidad.Business
{
    /// <summary>
    /// Objeto de negocio para PlanCuentas
    /// </summary>
    public class PlanCuentasImpuestoBusiness : GlobalData
    {
        private PlanCuentasImpuestoData DAPlanCuentas;

        
        public PlanCuentasImpuestoBusiness()
        {
            DAPlanCuentas = new PlanCuentasImpuestoData();
        }


        public void EliminarPlanCuentaImpuesto(Int32 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAPlanCuentas.EliminarPlanCuentaImpuesto(pId, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasBusiness", "EliminarPlanCuentaImpuesto", ex);
                return;
            }
        }


        public List<PlanCuentasImpuesto> ListarPlanCuentasImpuesto(PlanCuentasImpuesto pImpu, string filtro, Usuario vUsuario)
        {
            try
            {
                return DAPlanCuentas.ListarPlanCuentasImpuesto(pImpu,filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasBusiness", "ListarPlanCuentasImpuesto", ex);
                return null;
            }
        }


    }
}