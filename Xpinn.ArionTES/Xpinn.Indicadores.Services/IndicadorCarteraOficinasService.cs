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
    public class IndicadorCarteraOficinasService
    {


        private CarteraVencidaBusiness BOCarteraBrutaBusiness;
        private ExcepcionBusiness BOExcepcion;

        public string CodigoPrograma { get { return "140403"; } }

        /// <summary>
        /// Constructor del servicio para ComponenteAdicional
        /// </summary>
        public IndicadorCarteraOficinasService()
        {
            BOCarteraBrutaBusiness = new CarteraVencidaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<IndicadorCarteraOficinas> consultarCarteraVencidaOficinas(string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOCarteraBrutaBusiness.consultarCarteraVencidaOficinas(fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadorCarteraOficinasService", "consultarCarteraVencida", ex);
                return null;
            }
        }

        public List<IndicadorCarteraOficinas> consultarCarteraVencida30Oficinas(string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOCarteraBrutaBusiness.consultarCarteraVencida30Oficinas(fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadorCarteraOficinasService", "consultarCarteraVencida30Oficinas", ex);
                return null;
            }
        }

    }
}




