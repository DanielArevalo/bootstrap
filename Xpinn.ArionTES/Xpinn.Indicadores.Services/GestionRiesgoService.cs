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
    public class GestionRiesgoService
    {


        private GestionRiesgoBusiness BOGestionRiesgo;
        private ExcepcionBusiness BOExcepcion;

        public string CodigoPrograma { get { return "140901"; } }

        /// <summary>
        /// Constructor del servicio para ComponenteAdicional
        /// </summary>

        public GestionRiesgoService()
        {
            BOGestionRiesgo = new GestionRiesgoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<GestionRiesgo> ListadoSegmentoCredito(string fechaCorte, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOGestionRiesgo.ListadoSegmentoCredito(fechaCorte, pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComponenteAdicionalService", "ListadoSegmentoCredito", ex);
                return null;
            }
        }

    }
}




