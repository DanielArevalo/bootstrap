using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Asesores.Data;
using Xpinn.Util;
using System.IO;

namespace Xpinn.Asesores.Business
{
   public  class InformacionFinancieraBusiness
    {

        private InformacionFinancieraData DADatosInformacionFinanciera;
        

        /// <summary>
        /// Constructor del objeto de negocio para Caja
        /// </summary>
        public InformacionFinancieraBusiness()
        {
            DADatosInformacionFinanciera = new InformacionFinancieraData();
        }

        public InformacionFinanciera ListarInformacionFinanciera(int codigo, Usuario pUsuario)
        {

            var list = DADatosInformacionFinanciera.ListarInformacionFinanciera(codigo, pUsuario);
            return list;
        }
      

       
    }
}
