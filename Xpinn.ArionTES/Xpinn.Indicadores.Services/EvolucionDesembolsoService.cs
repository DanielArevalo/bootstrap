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
    public class EvolucionDesembolsoService
    {


        private EvolucionDesembolsoBusiness BODesembolsoBusiness;
        private ExcepcionBusiness BOExcepcion;

        public string CodigoPrograma { get { return "140104"; } }
        public string CodigoProgramaVencida { get { return "140407"; } }

        /// <summary>
        /// Constructor del servicio para ComponenteAdicional
        /// </summary>

        public EvolucionDesembolsoService()
        {
            BODesembolsoBusiness = new EvolucionDesembolsoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<EvolucionDesembolsos> consultarDesembolso(string fechaini, string fechafin, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BODesembolsoBusiness.consultarDesembolso(fechaini, fechafin,pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComponenteAdicionalService", "ListarComponenteAdicional", ex);
                return null;
            }
        }

    }
}




