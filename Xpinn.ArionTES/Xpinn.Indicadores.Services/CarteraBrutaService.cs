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
    public class CarteraBrutaService
    {


        private CarteraBrutaBusiness BOCarteraBrutaBusiness;
        private ExcepcionBusiness BOExcepcion;

        public string CodigoPrograma { get { return "140101"; } }

        /// <summary>
        /// Constructor del servicio para ComponenteAdicional
        /// </summary>
        public CarteraBrutaService()
        {
            BOCarteraBrutaBusiness = new CarteraBrutaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<CarteraBruta> consultarCartera(string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOCarteraBrutaBusiness.consultarCartera( fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraBrutaService", "consultarCartera", ex);
                return null;
            }
        }
        public List<CarteraBruta> consultarCarteraVariacion(string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOCarteraBrutaBusiness.consultarCarteraVariacion(fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraBrutaService", "consultarCarteraVariacion", ex);
                return null;
            }
        }
    }
}




