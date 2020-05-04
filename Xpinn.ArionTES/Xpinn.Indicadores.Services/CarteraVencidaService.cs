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
    public class CarteraVencidaService
    {
        private CarteraVencidaBusiness BOCarteraBrutaBusiness;
        private ExcepcionBusiness BOExcepcion;

        public string CodigoPrograma { get { return "140103"; } }

        /// <summary>
        /// Constructor del servicio para ComponenteAdicional
        /// </summary>
        public CarteraVencidaService()
        {
            BOCarteraBrutaBusiness = new CarteraVencidaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<CarteraVencida> consultarCarteraVencida(string filtro,string fechaini, string fechafin, int dia, Usuario pUsuario)
        {
            try
            {
                return BOCarteraBrutaBusiness.consultarCarteraVencida(filtro,fechaini, fechafin, dia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraVencidaService", "consultarCarteraVencida", ex);
                return null;
            }
        }

        public List<IndicadorCartera> ConsultarCarteraVencidaFechas(string filtro, Usuario pUsuario)
        {
            try
            {
                return BOCarteraBrutaBusiness.ConsultarCarteraVencidaFechas(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraVencidaBusiness", "ConsultarCarteraVencidaFechas", ex);
                return null;
            }
        }

        public List<IndicadorCartera> ConsultarCarteraVencidaXcategoria(string filtro, string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOCarteraBrutaBusiness.ConsultarCarteraVencidaXcategoria(filtro, fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraVencidaService", "ConsultarCarteraVencidaXcategoria", ex);
                return null;
            }
        }

        public List<CarteraVencida> ConsultarCarteraVencidaLinea(string filtro, string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOCarteraBrutaBusiness.ConsultarCarteraVencidaLinea(filtro,fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraVencidaService", "consultarCarteraVencida", ex);
                return null;
            }
        }

        public List<CarteraVencida> ListarLineasCredito(Usuario pUsuario)
        {
            try
            {
                return BOCarteraBrutaBusiness.ListarLineasCredito(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoService", "ListarLineasCredito", ex);
                return null;
            }
        }
    }
}




