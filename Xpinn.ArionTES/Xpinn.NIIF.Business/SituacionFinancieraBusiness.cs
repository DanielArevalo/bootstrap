using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.NIIF.Data;
using Xpinn.NIIF.Entities;

namespace Xpinn.NIIF.Business
{
    /// <summary>
    /// Objeto de negocio para concepto
    /// </summary>
    public class SituacionFinancieraBusiness : GlobalBusiness
    {
        private SituacionFinancieraData DASituacionFinanciera;

        /// <summary>
        /// Constructor del objeto de negocio para concepto
        /// </summary>
        public SituacionFinancieraBusiness()
        {
            DASituacionFinanciera = new SituacionFinancieraData();
        }

        public List<SituacionFinanciera> ListarSituacionFinanciera(SituacionFinanciera pEntidad, Usuario vUsuario, int pOpcion)
        {
            return DASituacionFinanciera.ListarSituacionFinanciera(pEntidad, vUsuario,pOpcion);
        }
        public List<SituacionFinanciera> ListarFecha(Usuario pUsuario)
        {
            return DASituacionFinanciera.ListarFecha(pUsuario);
        }
      
       
    }
}

