using System;
using System.Collections.Generic;
using System.Transactions;
using System.Linq;
using Xpinn.Util;
using Xpinn.Cartera.Data;
using Xpinn.Cartera.Entities;

namespace Xpinn.Cartera.Business
{
    public class ReporteBusiness : GlobalBusiness
    {
        private ReporteData reporteData;

        public ReporteBusiness()
        {
            reporteData = new ReporteData();
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

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<ReporteConsolidadoCierre> ConsultarReporteConsolidadoCierre(string fechaCorte, string filtro, Usuario usuario)
        {
            try
            {
                List<ReporteConsolidadoCierre> lstReporte = reporteData.ConsultarReporteConsolidadoCierre(fechaCorte, filtro, usuario);

                decimal saldoCapitalTotal = lstReporte.Sum(h => h.SaldoCapital);

                lstReporte.ForEach(x =>
                {
                    x.Categoria = x.CodigoCategoria + "-" + x.Categoria;
                    x.Porcentaje = x.SaldoCapital / saldoCapitalTotal;
                    x.CodigoLinea = x.CodigoLinea + "-" + x.LineaCredito;
                });

                return lstReporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ConsultarReporteConsolidadoCierre", ex);
                return null;
            }
        }


        public List<ReporteConsolidadoCierre> ConsultarReporteConsolidadoCausacion(string fechaCorte, string filtro, Usuario usuario)
        {
            try
            {
                List<ReporteConsolidadoCierre> lstReporte = reporteData.ConsultarReporteConsolidadoCausacion(fechaCorte, filtro, usuario);

                lstReporte.ForEach(x =>
                {
                    x.Categoria = x.CodigoCategoria + "-" + x.Categoria;
                    x.Atributo = x.CodigoAtributo + "-" + x.Atributo;
                    x.CodigoLinea = x.CodigoLinea + "-" + x.LineaCredito;
                });

                return lstReporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ConsultarReporteConsolidadoCausacion", ex);
                return null;
            }
        }


        public List<ReporteConsolidadoCierre> ConsultarReporteConsolidadoProvision(string fechaCorte, string filtro, Usuario usuario)
        {
            try
            {
                List<ReporteConsolidadoCierre> lstReporte = reporteData.ConsultarReporteConsolidadoProvision(fechaCorte, filtro, usuario);

                lstReporte.ForEach(x =>
                {
                    x.Categoria = x.CodigoCategoria + "-" + x.Categoria;
                    x.Atributo = x.CodigoAtributo + "-" + x.Atributo;
                    x.CodigoLinea = x.CodigoLinea + "-" + x.LineaCredito;
                });

                return lstReporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "ConsultarReporteConsolidadoProvision", ex);
                return null;
            }
        }


    }
}
