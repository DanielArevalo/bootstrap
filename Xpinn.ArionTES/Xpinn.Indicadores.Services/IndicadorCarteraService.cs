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
    public class IndicadorCarteraService
    {

        private CarteraVencidaBusiness BOCarteraBrutaBusiness;
        private ExcepcionBusiness BOExcepcion;

        public string CodigoPrograma { get { return "140106"; } }
        public string CodigoProgramacolocacion { get { return "140406"; } }
        public string CodigoProgramaparticipacion { get { return "140108"; } }
        public string CodigoProgramaCartCatego { get { return "140109"; } }
        public string CodigoProgramaDatafono { get { return "140701"; } }
        public string CodigoProgramaPagadurias { get { return "140702"; } }

        /// <summary>
        /// Constructor del servicio para ComponenteAdicional
        /// </summary>
        public IndicadorCarteraService()
        {
            BOCarteraBrutaBusiness = new CarteraVencidaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<IndicadorCartera> consultarCarteraVencida(string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOCarteraBrutaBusiness.consultarCarteraVencida(fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadorCarteraService", "consultarCarteraVencida", ex);
                return null;
            }
        }


        public List<IndicadorCartera> colocacionoficina(string ofi, string fechaini, string fechafin, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOCarteraBrutaBusiness.colocacionoficina(ofi, fechaini, fechafin,pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadorCarteraService", "colocacionoficina", ex);
                return null;
            }
        }

        public List<IndicadorCartera> consultarparticipacionpagadurias(string filtro, string Orden, string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOCarteraBrutaBusiness.consultarparticipacionpagadurias(filtro, Orden, fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadorCarteraService", "consultarparticipacionpagadurias", ex);
                return null;
            }
        }

        public List<IndicadorCartera> ListarCarteraCategoria(string pFiltro, string pOrden, DateTime pFechaCorte, Usuario pUsuario)
        {
            try
            {
                return BOCarteraBrutaBusiness.ListarCarteraCategoria(pFiltro, pOrden, pFechaCorte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadorCarteraService", "ListarCarteraCategoria", ex);
                return null;
            }
        }

        public List<IndicadorCartera> ConsultarCarteraCategorias(DateTime pFecha, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOCarteraBrutaBusiness.ConsultarCarteraCategorias(pFecha, pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadorCarteraService", "ConsultarCarteraCategorias", ex);
                return null;
            }
        }

        public List<IndicadorCartera> ColocacionCarteraXLinea(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOCarteraBrutaBusiness.ColocacionCarteraXLinea(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadorCarteraService", "ColocacionCarteraXLinea", ex);
                return null;
            }
        }

        public List<IndicadorCartera> IngresosDatafono(DateTime pFechaIni, DateTime pFechaFin, Usuario pUsuario)
        {
            try
            {
                return BOCarteraBrutaBusiness.IngresosDatafono(pFechaIni, pFechaFin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadorCarteraService", "IngresosDatafono", ex);
                return null;
            }
        }


        public List<IndicadorCartera> IngresosPagadurias(DateTime pFechaIni, DateTime pFechaFin, Usuario pUsuario)
        {
            try
            {
                return BOCarteraBrutaBusiness.IngresosPagadurias(pFechaIni, pFechaFin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadorCarteraService", "IngresosDatafono", ex);
                return null;
            }
        }
    }
}




