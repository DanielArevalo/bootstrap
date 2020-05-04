using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Indicadores.Business;
using Xpinn.Indicadores.Entities;

namespace Xpinn.Indicadores.Services
{
  [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class EvolucionDesembolsoOficinaService
    {


        private EvolucionDesembolsoBusiness BODesembolsoBusiness;
        private ExcepcionBusiness BOExcepcion;

        public string CodigoPrograma { get { return "140402"; } }

        /// <summary>
        /// Constructor del servicio para ComponenteAdicional
        /// </summary>

        public EvolucionDesembolsoOficinaService()
        {
            BODesembolsoBusiness = new EvolucionDesembolsoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<EvolucionDesembolsoOficinas> consultarDesembolsoOficina(string fechaini, string fechafin, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BODesembolsoBusiness.consultarDesembolsoOficina(fechaini, fechafin,pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EvolucionDesembolsoOficinaService", "consultarDesembolsoOficina", ex);
                return null;
            }
        }

        

    }
}




