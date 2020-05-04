using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Caja.Business;
using Xpinn.Caja.Entities;
using Xpinn.Util;

namespace Xpinn.Caja.Services
{
    /// <summary>
    /// Servicio para TipoMoneda
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ConveniosService
    {
        private ConveniosBusiness BOConvenio;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para TipoMoneda
        /// </summary>
        public ConveniosService()
        {
            BOConvenio = new ConveniosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public ConveniosRecaudo ConsultarConvenioRecaudo(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOConvenio.ConsultarConvenioRecaudo(pId, vUsuario);                
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConveniosService", "ConsultarConvenioRecaudo", ex);
                return null;
            }
        }

    }
}
