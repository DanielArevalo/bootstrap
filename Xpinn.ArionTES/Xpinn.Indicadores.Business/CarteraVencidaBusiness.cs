using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Indicadores.Data;
using Xpinn.Indicadores.Entities;

namespace Xpinn.Indicadores.Business
{
    public class CarteraVencidaBusiness : GlobalData
    {

        private CarteraVencidaData BOCarteraVencidaData;

        public CarteraVencidaBusiness()
        {
            BOCarteraVencidaData = new CarteraVencidaData();
        }


        public List<CarteraVencida> consultarCarteraVencida(string filtro, string fechaini, string fechafin, int dia, Usuario pUsuario)
        {
            try
            {
                return BOCarteraVencidaData.consultarCarteraVencida(filtro, fechaini, fechafin, dia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraVencidaBusiness", "consultarCarteraVencida", ex);
                return null;
            }
        }

        public List<IndicadorCartera> ConsultarCarteraVencidaFechas(string filtro, Usuario pUsuario)
        {
            try
            {
                return BOCarteraVencidaData.ConsultarCarteraVencidaFechas(filtro, pUsuario);
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
                return BOCarteraVencidaData.ConsultarCarteraVencidaXcategoria(filtro, fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraVencidaBusiness", "ConsultarCarteraVencidaXcategoria", ex);
                return null;
            }
        }

        public List<IndicadorCartera> consultarparticipacionpagadurias(string filtro, string Orden, string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOCarteraVencidaData.consultarparticipacionpagadurias(filtro, Orden, fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("consultarparticipacionpagadurias", "consultarparticipacionpagadurias", ex);
                return null;
            }
        }

        public List<IndicadorCartera> consultarCarteraVencida(string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOCarteraVencidaData.consultarCarteraVencida(fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraVencidaBusiness", "consultarCarteraVencida", ex);
                return null;
            }
        }

        public List<IndicadorCarteraXClasificacion> consultarCarteraVencidaxClasificacion(string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOCarteraVencidaData.consultarCarteraVencidaxClasificacion(fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraVencidaBusiness", "consultarCarteraVencidaxClasificacion", ex);
                return null;
            }
        }

        public List<IndicadorCarteraXClasificacion> consultarCarteraVencida30xClasificacion(string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOCarteraVencidaData.consultarCarteraVencida30xClasificacion(fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraVencidaBusiness", "consultarCarteraVencidaxClasificacion", ex);
                return null;
            }
        }

        public List<IndicadorCarteraOficinas> consultarCarteraVencidaOficinas(string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOCarteraVencidaData.consultarCarteraVencidaOficinas(fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraVencidaBusiness", "consultarCarteraVencidaOficinas", ex);
                return null;
            }
        }

        public List<IndicadorCarteraOficinas> consultarCarteraVencida30Oficinas(string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOCarteraVencidaData.consultarCarteraVencida30Oficinas(fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraVencidaBusiness", "consultarCarteraVencida30Oficinas", ex);
                return null;
            }
        }


        public List<IndicadorCartera> colocacionoficina(string ofi, string fechaini, string fechafin, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOCarteraVencidaData.colocacionoficina(ofi, fechaini, fechafin, pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EvolucionDesembolsoBusiness", "colocacionoficina", ex);
                return null;
            }
        }

        public List<IndicadorCartera> ListarCarteraCategoria(string pFiltro, string pOrden, DateTime pFechaCorte, Usuario pUsuario)
        {
            try
            {
                return BOCarteraVencidaData.ListarCarteraCategoria(pFiltro, pOrden, pFechaCorte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EvolucionDesembolsoBusiness", "ListarCarteraCategoria", ex);
                return null;
            }
        }

        public List<IndicadorCartera> ConsultarCarteraCategorias(DateTime pFecha, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOCarteraVencidaData.ConsultarCarteraCategorias(pFecha, pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraVencidaBusiness", "ConsultarCarteraCategorias", ex);
                return null;
            }
        }

        public List<IndicadorCartera> ColocacionCarteraXLinea(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOCarteraVencidaData.ColocacionCarteraXLinea(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraVencidaBusiness", "ColocacionCarteraXLinea", ex);
                return null;
            }
        }

        public List<IndicadorCartera> IngresosDatafono(DateTime pFechaIni, DateTime pFechaFin, Usuario pUsuario)
        {
            try
            {
                return BOCarteraVencidaData.IngresosDatafono(pFechaIni, pFechaFin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraVencidaBusiness", "IngresosDatafono", ex);
                return null;
            }
        }

        public List<IndicadorCartera> IngresosPagadurias(DateTime pFechaIni, DateTime pFechaFin, Usuario pUsuario)
        {
            try
            {
                return BOCarteraVencidaData.IngresosPagadurias(pFechaIni, pFechaFin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraVencidaBusiness", "IngresosPagadurias", ex);
                return null;
            }
        }

        public List<CarteraVencida> ConsultarCarteraVencidaLinea(string filtro, string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOCarteraVencidaData.ConsultarCarteraVencidaLinea(filtro, fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraVencidaBusiness", "consultarCarteraVencida", ex);
                return null;
            }
        }

        public List<CarteraVencida> ListarLineasCredito(Usuario pUsuario)
        {
            try
            {
                return BOCarteraVencidaData.ListarLineasCredito(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasCreditoBusiness", "ListarLineasCredito", ex);
                return null;
            }
        }

    }
}


