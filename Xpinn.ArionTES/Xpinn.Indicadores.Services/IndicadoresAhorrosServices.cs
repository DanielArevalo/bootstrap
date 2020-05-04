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
    public class IndicadoresAhorrosService
    {


        private IndicadoresAhorrosBusiness BOIndicadoresAhorrosBusiness;
        private ExcepcionBusiness BOExcepcion;

        public string CodigoPrograma { get { return "140501"; } }

        /// <summary>
        /// Constructor del servicio para ComponenteAdicional
        /// </summary>
        public IndicadoresAhorrosService()
        {
            BOIndicadoresAhorrosBusiness = new IndicadoresAhorrosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<IndicadoresAhorros> consultarfecha(Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresAhorrosBusiness.consultarfecha(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraOficinasService", "consultarfecha", ex);
                return null;
            }
        }
        public List<IndicadoresAhorros> ListarTipoProducto(Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresAhorrosBusiness.ListarTipoProducto(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresAhorrosService", "ListarTipoProducto", ex);
                return null;
            }
        }

        public List<IndicadoresAhorros> ListarLineaAhorro(Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresAhorrosBusiness.ListarLineaAhorro(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresAhorrosService", "ListarLineaAhorro", ex);
                return null;
            }

        }

              public List<IndicadoresAhorros> ListarLineaProgramado(Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresAhorrosBusiness.ListarLineaProgramado(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresAhorrosService", "ListarLineaProgramado", ex);
                return null;
            }
        }


        public List<IndicadoresAhorros> ListarLineaCdat(Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresAhorrosBusiness.ListarLineaCdat(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresAhorrosService", "ListarLineaCdat", ex);
                return null;
            }
        }


        public List<IndicadoresAhorros> consultarAhorros(string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresAhorrosBusiness.consultarAhorros(fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresAhorrosService", "consultarAhorros", ex);
                return null;
            }
        }


        public List<IndicadoresAhorros> consultarProgramado(string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresAhorrosBusiness.consultarProgramado(fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresAhorrosService", "consultarProgramado", ex);
                return null;
            }
        }

        public List<IndicadoresAhorros> consultarCdat(string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresAhorrosBusiness.consultarCdat(fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresAhorrosService", "consultarCdat", ex);
                return null;
            }
        }
        public List<IndicadoresAhorros> consultarAhorrosVariacion(string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresAhorrosBusiness.consultarAhorrosVariacion(fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresAhorrosService", "consultarAhorrosVariacion", ex);
                return null;
            }
        }
    }
}




