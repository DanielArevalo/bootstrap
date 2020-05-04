using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Contabilidad.Data;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Business
{
    /// <summary>
    /// Objeto de negocio para concepto
    /// </summary>
    public class BalanceGenCompBusiness : GlobalBusiness
    {
        private BalanceGenCompData DABalanceGenComp;

        /// <summary>
        /// Constructor del objeto de negocio para concepto
        /// </summary>
        public BalanceGenCompBusiness()
        {
            DABalanceGenComp = new BalanceGenCompData();
        }

        public List<BalanceGenComp> ListarBalanceComparativo(BalanceGenComp pEntidad, Usuario vUsuario, int pOpcion)
        {
            return DABalanceGenComp.ListarBalanceComparativo(pEntidad, vUsuario,pOpcion);
        }
        public List<BalanceGenComp> ListarFecha(Usuario pUsuario)
        {
            return DABalanceGenComp.ListarFecha(pUsuario);
        }
      
       
    }
}

