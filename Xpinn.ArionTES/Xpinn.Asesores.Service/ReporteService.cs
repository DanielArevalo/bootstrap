using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;


namespace Xpinn.Asesores.Services
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

        public string CodigoPrograma { get { return "110115"; } }
        public string CodigoProgramaReportes { get { return "110301"; } }
        public string CodigoProgReportesCierre { get { return "60206"; } }
        public string CodigoProgCierreDiario { get { return "60308"; } }
        public string CodigoProgCuadreSaldos { get { return "60209"; } }

        /// <summary>
        /// Método que permite consultar los crèditos de un asesor
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public List<Reporte> ListarReporteMora(Usuario pUsuario, Int64 codigo)
        {
            try{
                return BOAReporte.ListarReporteMora(pUsuario, codigo);
            }
            catch (Exception ex){
                BOExcepcion.Throw("ReporteService", "ListarReporteMora", ex);
                return null;
            }
        }

        /// <summary>
        /// Método que permite consultar los crèditos  en mora
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public List<Reporte> ListarReporteMoraTodos(Usuario pUsuario)
        {
            try
            {
                return BOAReporte.ListarReporteMoraTodos(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarReporteMoraTodos", ex);
                return null;
            }
        }

        /// <summary>
        /// Método que permite consultar los créditos de todos los asesores
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public List<Reporte> ListarReportecobranza(Usuario pUsuario, Int64 codigo, string rango, string asesores, string oficina)
        {
            try
            {
                return BOAReporte.ListarReportecobranza(pUsuario, codigo, rango, asesores, oficina);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarReportecobranza", ex);
                return null;
            }
        }

        public List<Reporte> ListarReporteMoraofici(Usuario pUsuario,Int64 oficina)
        {
            try
            {
                return BOAReporte.ListarReporteMoraofici(pUsuario,oficina);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarReporteMoraofici", ex);
                return null;
            }
        }

        public List<Reporte> ListarReportepoliza(Usuario pUsuario, Int64 codigo, DateTime fechaini, DateTime fechafinal)
        {
            try
            {
                return BOAReporte.ListarReportepoliza(pUsuario, codigo, fechaini,fechafinal);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarReportepoliza", ex);
                return null;
            }
        }
        public List<Reporte> ListarReportecartejecutivo(Usuario pUsuario, Int64 codigo)
        {
            try
            {
                return BOAReporte.ListarReportecartejecutivo(pUsuario, codigo);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarReportecartejecutivo", ex);
                return null;
            }
        }
        public List<Reporte> ListarReporteactivo(Usuario pUsuario, Int64 codigo)
        {
            try
            {
                return BOAReporte.ListarReporteactivo(pUsuario, codigo);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarReporteactivo", ex);
                return null;
            }
        }
        public List<Reporte> ListarReporteactivoporasesor(Usuario pUsuario, Int64 codigo)
        {
            try
            {
                return BOAReporte.ListarReporteactivoporasesor(pUsuario, codigo);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarReporteactivoporasesor", ex);
                return null;
            }
        }

        public List<Reporte> ListarReportecierreoficina(Usuario pUsuario, Int64 codigo, DateTime fechaini)
        {
            try
            {
                return BOAReporte.ListarReportecierreoficina(pUsuario, codigo, fechaini);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarReportecierreoficina", ex);
                return null;
            }
        }

        public List<Reporte> ListarReportecierreoficinatodos(Usuario pUsuario,  DateTime fechaini)
        {
            try
            {
                return BOAReporte.ListarReportecierreoficinatodos(pUsuario, fechaini);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarReportecierreoficinatodos", ex);
                return null;
            }
        }
        public List<Reporte> Listarcolocacioneje(Usuario pUsuario, Int64 codigo, DateTime fechaini, DateTime fechafinal)
        {
            try
            {
                return BOAReporte.Listarcolocacioneje(pUsuario, codigo, fechaini, fechafinal);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "Listarcolocacioneje", ex);
                return null;
            }
        }


        /// <summary>
        /// Método que permite consultar la gestion de cobro de un asesor 
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public List<Reporte> ListarReporteGestionCobranzas(Usuario pUsuario, Int64 codigo,DateTime fechaini, DateTime fechafinal)
        {
            try
            {
                return BOAReporte.ListarReporteGestionCobranzas(pUsuario, codigo, fechaini, fechafinal);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarReporteGestionCobranzas", ex);
                return null;
            }
        }



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

        public List<Reporte> ListarAfiliacionPorAsesor(string cod_asesor, Usuario usuario)
        {
            try
            {
                return BOAReporte.ListarAfiliacionPorAsesor(cod_asesor, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarAfiliacionPorAsesor", ex);
                return null;
            }
        }

        public List<Reporte> ListarProductosPorAsesor(string cod_asesor, Usuario usuario)
        {
            try
            {
                return BOAReporte.ListarProductosPorAsesor(cod_asesor, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarProductosPorAsesor", ex);
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


    }


}

