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
    public class GestionDiariaService
    {


        private GestionDiariaBusiness BOGestionDiaria;
        private ExcepcionBusiness BOExcepcion;

        public string CodigoPrograma { get { return "140105"; } }

        /// <summary>
        /// Constructor del servicio para ComponenteAdicional
        /// </summary>
        public GestionDiariaService()
        {
            BOGestionDiaria = new GestionDiariaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<GestionDiaria> ReporteGestionDiaria(Usuario pUsuario)
        {
            try
            {
                return BOGestionDiaria.ReporteGestionDiaria(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComponenteAdicionalService", "ListarComponenteAdicional", ex);
                return null;
            }
        }
    }
}




