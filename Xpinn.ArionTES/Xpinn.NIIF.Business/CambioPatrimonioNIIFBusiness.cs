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
    public class CambioPatrimonioNIIFBusiness : GlobalBusiness
    {
        private CambioPatrimonioNIIFData DACambioPatrimonioNIIF;

        /// <summary>
        /// Constructor del objeto de negocio para concepto
        /// </summary>
        public CambioPatrimonioNIIFBusiness()
        {
            DACambioPatrimonioNIIF = new CambioPatrimonioNIIFData();
        }

        public List<CambioPatrimonioNIIF> ListarEstadoCambioPatrimonio(CambioPatrimonioNIIF pEntidad, Usuario vUsuario, int pOpcion)
        {
            return DACambioPatrimonioNIIF.ListarEstadoCambioPatrimonio(pEntidad, vUsuario,pOpcion);
        }
        public List<CambioPatrimonioNIIF> ListarFecha(Usuario pUsuario)
        {
            return DACambioPatrimonioNIIF.ListarFecha(pUsuario);
        }
      
       
    }
}

