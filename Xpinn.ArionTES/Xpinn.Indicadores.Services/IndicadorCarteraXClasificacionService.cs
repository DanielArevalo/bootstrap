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
    public class IndicadorCarteraXClasificacionService
    {


        private CarteraVencidaBusiness BOCarteraBrutaBusiness;
        private ExcepcionBusiness BOExcepcion;

        public string CodigoPrograma { get { return "140106"; } }

        /// <summary>
        /// Constructor del servicio para ComponenteAdicional
        /// </summary>
        public IndicadorCarteraXClasificacionService()
        {
            BOCarteraBrutaBusiness = new CarteraVencidaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<IndicadorCarteraXClasificacion> consultarCarteraVencidaxClasificacion(string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOCarteraBrutaBusiness.consultarCarteraVencidaxClasificacion(fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadorCarteraXClasificacionService", "consultarCarteraVencidaxClasificacion", ex);
                return null;
            }
        }

        public List<IndicadorCarteraXClasificacion> consultarCarteraVencida30xClasificacion(string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOCarteraBrutaBusiness.consultarCarteraVencida30xClasificacion(fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadorCarteraXClasificacionService", "consultarCarteraVencida30xClasificacion", ex);
                return null;
            }
        }

    }
}




