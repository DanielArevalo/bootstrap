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
    public class EstadoResultadoNIIFBusiness : GlobalBusiness
    {
        private EstadoResultadoNIIFData DAEstadoResultadoNIIF;

        /// <summary>
        /// Constructor del objeto de negocio para concepto
        /// </summary>
        public EstadoResultadoNIIFBusiness()
        {
            DAEstadoResultadoNIIF = new EstadoResultadoNIIFData();
        }

        public List<EstadoResultadoNIIF> ListarEstadoResultado(EstadoResultadoNIIF pEntidad, Usuario vUsuario, int pOpcion)
        {
            return DAEstadoResultadoNIIF.ListarEstadoResultado(pEntidad, vUsuario,pOpcion);
        }
        public List<EstadoResultadoNIIF> ListarFecha(Usuario pUsuario)
        {
            return DAEstadoResultadoNIIF.ListarFecha(pUsuario);
        }
      
       
    }
}

