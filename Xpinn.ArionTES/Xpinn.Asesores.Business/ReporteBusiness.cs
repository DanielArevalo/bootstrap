using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Asesores.Data;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Business
{
    public class ReporteBusiness : GlobalBusiness
    {
        private ReporteData reporteData;

        public ReporteBusiness()
        {
            reporteData = new ReporteData();
        }

        public List<Reporte> ListarReporteMora(Usuario pUsuario, Int64 codigo)
        {
            try
            {
                return reporteData.ListarReporteMora(pUsuario, codigo);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ListarReporteMora", ex);
                return null;
            }
        }


        public List<Reporte> ListarReporteMoraTodos(Usuario pUsuario)
        {
            try
            {
                return reporteData.ListarReporteMoraTodos(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ListarReporteMoraTodos", ex);
                return null;
            }
        }


        public List<Reporte> ListarReporteGestionCobranzas(Usuario pUsuario, Int64 codigo,DateTime fechaini, DateTime fechafinal)
        {
            try
            {
                return reporteData.ListarReporteGestionCobranzas(pUsuario, codigo, fechaini, fechafinal);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ListarReporteGestionCobranzas", ex);
                return null;
            }
        }


        

        public List<Reporte> ListarReportecobranza(Usuario pUsuario, Int64 codigo, string rango, string asesores, string oficina)
        {
            try
            {
                return reporteData.ListarReportecobranza(pUsuario, codigo, rango, asesores, oficina);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AcodeudadosBusiness", "ListarAcodeudados", ex);
                return null;
            }
        }

        public List<Reporte> ListarReporteMoraofici(Usuario pUsuario,Int64 pOficina)
        {
            try
            {
                return reporteData.ListarReporteMoraofici(pUsuario, pOficina);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AcodeudadosBusiness", "ListarAcodeudados", ex);
                return null;
            }
        }

        public List<Reporte> ListarReportecartejecutivo(Usuario pUsuario, Int64 codigo)
        {
            try
            {
                return reporteData.ListarReportecartejecutivo(pUsuario, codigo);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AcodeudadosBusiness", "ListarAcodeudados", ex);
                return null;
            }
        }

        public List<Reporte> ListarReporteactivo(Usuario pUsuario, Int64 codigo)
        {
            try
            {
                return reporteData.ListarReporteactivo(pUsuario, codigo);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AcodeudadosBusiness", "ListarAcodeudados", ex);
                return null;
            }
        }
        public List<Reporte> ListarReporteactivoporasesor(Usuario pUsuario, Int64 codigo)
        {
            try
            {
                return reporteData.ListarReporteactivoporasesor(pUsuario, codigo);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AcodeudadosBusiness", "ListarReporteactivoporasesor", ex);
                return null;
            }
        }
        
        public List<Reporte> ListarReportecierreoficina(Usuario pUsuario, Int64 codigo, DateTime fechaini)
        {
            try
            {
                return reporteData.ListarReportecierreoficina(pUsuario, codigo, fechaini );
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AcodeudadosBusiness", "ListarAcodeudados", ex);
                return null;
            }
        }

        public List<Reporte> ListarReportecierreoficinatodos(Usuario pUsuario,  DateTime fechaini)
        {
            try
            {
                return reporteData.ListarReportecierreoficinatodos(pUsuario, fechaini);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AcodeudadosBusiness", "ListarReportecierreoficinatodos", ex);
                return null;
            }
        }

        public List<Reporte> ListarReportepoliza(Usuario pUsuario, Int64 codigo, DateTime fechaini, DateTime fechafinal)
        {
            try
            {
                return reporteData.ListarReportepoliza(pUsuario, codigo, fechaini, fechafinal);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AcodeudadosBusiness", "ListarAcodeudados", ex);
                return null;
            }
        }
        public List<Reporte> Listarcolocacioneje(Usuario pUsuario, Int64 codigo, DateTime fechaini, DateTime fechafinal)
        {
            try
            {
                return reporteData.Listarcolocacioneje(pUsuario, codigo, fechaini, fechafinal);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AcodeudadosBusiness", "ListarAcodeudados", ex);
                return null;
            }
        }



        public List<Reporte> ListarRepCierreDetallado(DateTime fecha, string filtro, Usuario pUsuario)
        {
            try
            {
                return reporteData.ListarRepCierreDetallado(fecha, filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ListarRepCierreDetallado", ex);
                return null;
            }
        }

        public List<Reporte> ListarRepCausacionDetallado(DateTime fecha, string filtro, Usuario pUsuario)
        {
            try
            {
                return reporteData.ListarRepCausacionDetallado(fecha, filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ListarRepCausacionDetallado", ex);
                return null;
            }
        }

        public List<Reporte> ListarRepProvisionDetallado(DateTime fecha, string filtro, Usuario pUsuario)
        {
            try
            {
                return reporteData.ListarRepProvisionDetallado(fecha, filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ListarRepProvisionDetallado", ex);
                return null;
            }
        }



        public List<Reporte> ListarRepCierreDiarios(DateTime fecha, string filtro, Usuario pUsuario)
        {
            try
            {
                return reporteData.ListarRepCierreDiarios(fecha, filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ListarRepCierreDiarios", ex);
                return null;
            }
        }


        public List<Reporte> ListarFechaCorte(Usuario pUsuario)
        {
            try
            {
                return reporteData.ListarFechaCorte( pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ListarFechaCorte", ex);
                return null;
            }
        }


        public List<Reporte> ListarRepCierreDetAsesor(DateTime fecha, Usuario pUsuario,Int64 codigo)
        {
            try
            {
                return reporteData.ListarRepCierreDetAsesor(fecha, pUsuario, codigo);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ListarRepCierreDetAsesor", ex);
                return null;
            }
        }

        public List<Reporte> Consultarusuariopagoespecial(Int64 pId, Usuario pUsuario, string filtro)
        {
            try
            {
                return reporteData.Consultarusuariopagoespecial(pId, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "Consultarusuariopagoespecial", ex);
                return null;
            }
        }

        public List<Reporte> ListarAfiliacionPorAsesor(string cod_asesor, Usuario usuario)
        {
            try
            {
                return reporteData.ListarAfiliacionPorAsesor(cod_asesor, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ListarAfiliacionPorAsesor", ex);
                return null;
            }
        }

        public List<Reporte> ListarProductosPorAsesor(string cod_asesor, Usuario usuario)
        {
            try
            {
                return reporteData.ListarProductosPorAsesor(cod_asesor, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ListarProductosPorAsesor", ex);
                return null;
            }
        }

        public List<Reporte> CuadreSaldos(DateTime pFecha, int pTipo, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return reporteData.CuadreSaldos(pFecha, pTipo, pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "CuadreSaldos", ex);
                return null;
            }
        }

        public bool GuardarCuadreSaldos(DateTime pFecha, int pTipo, Usuario pUsuario)
        {
            try
            {
                return reporteData.GuardarCuadreSaldos(pFecha, pTipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "GuardarCuadreSaldos", ex);
                return false;
            }
        }

    }
}
