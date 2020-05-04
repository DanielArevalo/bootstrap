using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Cartera.Business;
using Xpinn.Cartera.Entities;


namespace Xpinn.Cartera.Services
{
    public class ReporteService
    {
        private ReporteBusiness BOAReporte;
        private ExcepcionBusiness BOExcepcion;

        public ReporteService()
        {
            BOAReporte = new ReporteBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoProgReportesCierre { get { return "60206"; } }
        public string CodigoProgCierreDiario { get { return "60308"; } }
        public string CodigoProgCuadreSaldos { get { return "60209"; } }
        public string CodigoProgramaReporteConsolidadoCierre { get { return "60307"; } }



        /// <summary>
        /// Método que permite un detalle de los tados al cierre 
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public List<Reporte> ListarRepCierreDetallado(DateTime fecha, string filtro, Usuario pUsuario)
        {
            try
            {
                return BOAReporte.ListarRepCierreDetallado(fecha, filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarRepCierreDetallado", ex);
                return null;
            }
        }

        public List<Reporte> ListarRepCausacionDetallado(DateTime fecha, string filtro, Usuario pUsuario)
        {
            try
            {
                return BOAReporte.ListarRepCausacionDetallado(fecha, filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarRepCausacionDetallado", ex);
                return null;
            }
        }

        public List<Reporte> ListarRepProvisionDetallado(DateTime fecha, string filtro, Usuario pUsuario)
        {
            try
            {
                return BOAReporte.ListarRepProvisionDetallado(fecha, filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarRepProvisionDetallado", ex);
                return null;
            }
        }

        public List<Reporte> ListarRepCierreDiarios(DateTime fecha, string filtro, Usuario pUsuario)
        {
            try
            {
                return BOAReporte.ListarRepCierreDiarios(fecha, filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarRepCierreDiarios", ex);
                return null;
            }
        }

        public List<Reporte> ListarFechaCorte(Usuario pUsuario)
        {
            try
            {
                return BOAReporte.ListarFechaCorte(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarFechaCorte", ex);
                return null;
            }
        }

        /// <summary>
        /// Método que permite un detalle de los tados al cierre por asesor
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public List<Reporte> ListarRepCierreDetAsesor(DateTime fecha, Usuario pUsuario,Int64 codigo)
        {
            try
            {
                return BOAReporte.ListarRepCierreDetAsesor(fecha, pUsuario, codigo);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarRepCierreDetAsesor", ex);
                return null;
            }
        }

        public List<Reporte> Consultarusuariopagoespecial(Int64 pId, Usuario pUsuario, string filtro)
        {
            try
            {
                return BOAReporte.Consultarusuariopagoespecial(pId, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "Consultarusuariopagoespecial", ex);
                return null;
            }
        }

        

        public List<Reporte> CuadreSaldos(DateTime pFecha, int pTipo, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOAReporte.CuadreSaldos(pFecha, pTipo, pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "CuadreSaldos", ex);
                return null;
            }
        }

        public bool GuardarCuadreSaldos(DateTime pFecha, int pTipo, Usuario pUsuario)
        {
            try
            {
                return BOAReporte.GuardarCuadreSaldos(pFecha, pTipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "GuardarCuadreSaldos", ex);
                return false;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<ReporteConsolidadoCierre> ConsultarReporteConsolidadoCierre(string fechaCorte, string filtro, Usuario usuario)
        {
            try
            {
                return BOAReporte.ConsultarReporteConsolidadoCierre(fechaCorte, filtro, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ConsultarReporteConsolidadoCierre", ex);
                return null;
            }
        }

        public List<ReporteConsolidadoCierre> ConsultarReporteConsolidadoCausacion(string fechaCorte, string filtro, Usuario usuario)
        {
            try
            {
                return BOAReporte.ConsultarReporteConsolidadoCausacion(fechaCorte, filtro, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ConsultarReporteConsolidadoCausacion", ex);
                return null;
            }
        }


        public List<ReporteConsolidadoCierre> ConsultarReporteConsolidadoProvision(string fechaCorte, string filtro, Usuario usuario)
        {
            try
            {
                return BOAReporte.ConsultarReporteConsolidadoProvision(fechaCorte, filtro, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ConsultarReporteConsolidadoProvision", ex);
                return null;
            }
        }


    }
}

