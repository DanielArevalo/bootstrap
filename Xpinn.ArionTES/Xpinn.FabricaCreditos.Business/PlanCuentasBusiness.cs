using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Business
{
    public class PlanCuentasBusiness : GlobalData
    {
        private PlanCuentasData DAPlanCuentas;

        /// <summary>
        /// Constructor del objeto de negocio para Caja
        /// </summary>
        public PlanCuentasBusiness()
        {
            DAPlanCuentas = new PlanCuentasData();
        }

        /// <summary>
        /// Obtiene la lista de PlanCuentas
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de Tipos de PlanCuentas obtenidas</returns>        
        public List<PlanCuentas> ListarPlanCuentas(PlanCuentas pPlanCuentas, Usuario pUsuario)
        {
            try
            {
                return DAPlanCuentas.ListarPlanCuentas(pPlanCuentas, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasBusiness", "ListarPlanCuentas", ex);
                return null;
            }
        }
    }
}
