using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xpinn.Util
{
    /// <summary>
    /// Objeto para definicion de caracteristicas globales para la capa de negocio
    /// </summary>
    public abstract class GlobalBusiness
    {
        protected ExcepcionBusiness BOExcepcion = new ExcepcionBusiness();

        /// <summary>
        /// Constructor del objeto global de capa de negocio
        /// </summary>
        public GlobalBusiness()
        {
            BOExcepcion = new ExcepcionBusiness();
        }
    }
}
