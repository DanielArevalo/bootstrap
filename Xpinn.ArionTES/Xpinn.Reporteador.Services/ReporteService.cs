using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Reporteador.Business;
using Xpinn.Reporteador.Entities;
using Xpinn.Caja.Business;
using Xpinn.Aportes.Business;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.Reporteador.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ReporteService
    {
        private ReporteBusiness BOReporte;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Presupuesto
        /// </summary>
        public ReporteService()
        {
            CodigoPrograma = "200101";
            BOReporte = new ReporteBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get; set; }
        public string CodigoProgramaRep { get { return "200102"; } }
        public string CodigoProgramaReportelista { get { return "200103"; } }
        public string CodigoProgramaReporteClientesGarantias { get { return "200109"; } }
        public string CodigoProgramaReporteClientesProductos { get { return "200110"; } }
        public string CodigoProgramaReporteCanalGarantias { get { return "200111"; } }
        public string CodigoProgramaReporteProductoGarantias { get { return "200113"; } }
        public string CodigoProgramaReporteTransaccionesGarantias { get { return "200114"; } }
        public string CodigoProgramaReporteCentroCostos { get { return "200113"; } }


        public void AsignarCodigoPrograma(string valor)
        {
            CodigoPrograma = valor;
        }   

        public DataTable GenerarReporte(int pCodReporte, DateTime pFecha, List<Parametros> lstParametros, ref string[] aColumnas, ref System.Type[] aTipos, ref int numerocolumnas, ref string pError, Usuario pUsuario)
        {
            try
            {
                return BOReporte.GenerarReporte(pCodReporte, pFecha, lstParametros, ref aColumnas, ref aTipos, ref numerocolumnas, ref pError, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "GenerarReporte", ex);
                return null;
            }
        }

        public DataTable GenerarLista(string pSentencia, ref string pError, Usuario pUsuario)
        {
            try
            {
                return BOReporte.GenerarLista(pSentencia, ref pError, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "GenerarReporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para crear Reporte
        /// </summary>
        /// <param name="pEntity">Entidad Reporte</param>
        /// <returns>Entidad Reporte creada</returns>
        public Reporte CrearReporte(Reporte vReporte, Usuario pUsuario)
        {
            try
            {
                return BOReporte.CrearReporte(vReporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "CrearReporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Reporte
        /// </summary>
        /// <param name="pUsuario">Entidad Reporte</param>
        /// <returns>Entidad Reporte modificada</returns>
        public Reporte ModificarReporte(Reporte vReporte, Usuario pUsuario)
        {
            try
            {
                return BOReporte.ModificarReporte(vReporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ModificarReporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Reporte
        /// </summary>
        /// <param name="pId">identificador de Reporte</param>
        public void EliminarReporte(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOReporte.EliminarReporte(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarReporte", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Reporte
        /// </summary>
        /// <param name="pId">identificador de Reporte</param>
        /// <returns>Entidad Reporte</returns>
        public Reporte ConsultarReporte(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOReporte.ConsultarReporte(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ConsultarReporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Reporte a partir de unos filtros
        /// </summary>
        /// <param name="pUsuario">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Reporte obtenidos</returns>
        public List<Reporte> ListarReporte(Reporte vReporte, Usuario pUsuario)
        {
            try
            {
                return BOReporte.ListarReporte(vReporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarReporte", ex);
                return null;
            }
        }

        public List<Reporte> ListarReporteUsuario(Usuario pUsuario)
        {
            try
            {
                return BOReporte.ListarReporteUsuario(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarReporteUsuario", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BOReporte.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ObtenerSiguienteCodigo", ex);
                return 0;
            }
        }

        public List<Formato> ListarFormato(Formato pFormato, Usuario vUsuario)
        {
            try
            {
                return BOReporte.ListarFormato(pFormato, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarFormato", ex);
                return null;
            }
        }

        
        public DataTable ConsultarPersonasProductos(DateTime pFechaCorte, Usuario usuario)
        {
            try
            {
                PersonaBusiness BOPersona = new PersonaBusiness();
                return BOPersona.ConsultarPersonasProductos(pFechaCorte, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ConsultarPersonasProductos", ex);
                return null;
            }
        }

        public DataTable ConsultarProductos_GarantiasComunitarias(DateTime pFechaCorte, Usuario usuario)
        {
            try
            {
                return BOReporte.ConsultarProductos_GarantiasComunitarias(pFechaCorte, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ConsultarPersonasProductos", ex);
                return null;
            }
        }

        public DataTable ConsultarTransacciones_GarantiasComunitarias(DateTime pFechaCorte, Usuario usuario)
        {
            try
            {
                return BOReporte.ConsultarTransacciones_GarantiasComunitarias(pFechaCorte, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ConsultarPersonasProductos", ex);
                return null;
            }
        }

        public DataTable ConsultarAfiliados_GarantiasComunitarias(DateTime pFechaCorte, Usuario vUsuario)
        {
            try
            {
                AfiliacionBusiness BOAfiliacion = new AfiliacionBusiness();
                return BOAfiliacion.ConsultarAfiliados_GarantiasComunitarias(pFechaCorte, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "ConsultarAfiliacion", ex);
                return null;
            }
        }

        public DataTable ConsultarCanal_GarantiasComunitarias(Usuario vUsuario)
        {
            try
            {
                return BOReporte.ConsultarCanal_GarantiasComunitarias(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "ConsultarAfiliacion", ex);
                return null;
            }
        }

        public Parametros CrearParametro(Parametros vParametros, Usuario pUsuario)
                {
                    try
                    {
                        return BOReporte.CrearParametro(vParametros, pUsuario);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteService", "CrearParametro", ex);
                        return null;
                    }
                }

        public void EliminarParametro(Int64 pIdReporte, Int64 pIdParametro, Usuario vUsuario)
        {
            try
            {
                BOReporte.EliminarParametro(pIdReporte, pIdParametro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "EliminarParametro", ex);
            }
        }

        public Parametros ModificarParametro(Parametros vParametros, Usuario pUsuario)
        {
            try
            {
                return BOReporte.ModificarParametro(vParametros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ModificarParametro", ex);
                return null;
            }
        }

        public List<Parametros> ListarParametro(Parametros pParametros, Usuario vUsuario)
        {
            try
            {
                return BOReporte.ListarParametro(pParametros, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarParametro", ex);
                return null;
            }
        }

        public List<UsuariosReporte> ListarUsuarios(UsuariosReporte pUsuario, Usuario vUsuario)
        {
            try
            {
                return BOReporte.ListarUsuarios(pUsuario, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarUsuarios", ex);
                return null;
            }
        }

        public List<PerfilReporte> ListarPerfil(PerfilReporte pPerfil, Usuario vUsuario)
        {
            try
            {
                return BOReporte.ListarPerfil(pPerfil, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarPerfil", ex);
                return null;
            }
        }

        public UsuariosReporte CrearUsuario(UsuariosReporte pUsuariosReporte, Usuario vUsuario)
        {
            try
            {
                return BOReporte.CrearUsuario(pUsuariosReporte, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "CrearUsuario", ex);
                return null;
            }
        }

        public void EliminarUsuario(Int64 pIdReporte, Int64 pIdUsuario, Usuario vUsuario)
        {
            try
            {
                BOReporte.EliminarUsuario(pIdReporte, pIdUsuario, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "EliminarUsuario", ex);
            }
        }

        public List<Tabla> ListarTablaBase(Tabla pTabla, Usuario vUsuario)
        {
            try
            {
                return BOReporte.ListarTablaBase(pTabla, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarTabla", ex);
                return null;
            }
        }

        public List<Columna> ListarColumnaBase(string pTabla, Columna pColumna, Usuario vUsuario)
        {
            try
            {
                return BOReporte.ListarColumnaBase(pTabla, pColumna, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarColumna", ex);
                return null;
            }
        }

        public Columna ConsultarColumna(string pTabla, string pColumna, Usuario vUsuario)
        {
            try
            {
                return BOReporte.ConsultarColumna(pTabla, pColumna, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ConsultarColumna", ex);
                return null;
            }
        }

        public int CodTipoDato(string tipo_dato)
        {
            int tipo = 1;
            if (tipo_dato.Contains("NUMBER") || tipo_dato.Contains("DEC") || tipo_dato.Contains("INT") || tipo_dato.Contains("FLOAT"))
                tipo = 3;
            else if (tipo_dato.Contains("DATE") || tipo_dato.Contains("TIME"))
                tipo = 2;
            else
                tipo = 1;
            return tipo;
        }

        public List<Xpinn.Reporteador.Entities.Lista> ListarLista(Xpinn.Reporteador.Entities.Lista pLista, Usuario vUsuario)
        {
            try
            {
                return BOReporte.ListarLista(pLista, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarLista", ex);
                return null;
            }
        }

        public List<Xpinn.Reporteador.Entities.Lista> ListarReporteLista(string filtro,Xpinn.Reporteador.Entities.Lista pLista, Usuario vUsuario)
        {
            try
            {
                return BOReporte.ListarReporteLista(filtro,pLista, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarLista", ex);
                return null;
            }
        }

        public Lista ConsultarLista(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOReporte.ConsultarLista(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ConsultarLista", ex);
                return null;
            }
        }

        public List<Plantilla> ListarPlantilla(Plantilla pPlantilla, Usuario vUsuario)
        {
            try
            {
                return BOReporte.ListarPlantilla(pPlantilla, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarPlantilla", ex);
                return null;
            }
        }


        public Lista CrearReporteLista(Lista pLista, Usuario pusuario)
        {
            try
            {
                pLista = BOReporte.CrearReporeLista(pLista, pusuario);
                return pLista;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ListaService", "CrearLista", ex);
                return null;
            }
        }


        public Lista ModificarReporteLista(Lista pLista, Usuario pusuario)
        {
            try
            {
                pLista = BOReporte.ModificarReporteLista(pLista, pusuario);
                return pLista;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ListaService", "ModificarLista", ex);
                return null;
            }
        }


        public void EliminarReporteLista(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOReporte.EliminarReporteLista(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ListaService", "EliminarLista", ex);
            }
        }


        public Lista ConsultarReporteLista(Int64 pId, Usuario pusuario)
        {
            try
            {
                Lista Lista = new Lista();
                Lista = BOReporte.ConsultarReporteLista(pId, pusuario);
                return Lista;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ListaService", "ConsultarLista", ex);
                return null;
            }
        }

        public List<TransaccionEfectivo> ListarfechaCierrHist(Usuario pUsuario)
        {
            try
            {
                List<TransaccionEfectivo> lstFecha = new List<TransaccionEfectivo>();
                lstFecha = BOReporte.ListarfechaCierrHist(pUsuario);
                return lstFecha;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ListaService", "ListarFechaCorte", ex);
                return null;
            }
        }

        public DataTable ConsultarAfiliados_GarantiasComunitarias(DateTime pFechaCorte, Usuario vUsuario, string TipInfAdicional)
        {
            try
            {
                AfiliacionBusiness BOAfiliacion = new AfiliacionBusiness();
                return BOAfiliacion.ConsultarAfiliados_GarantiasComunitarias(pFechaCorte, vUsuario, TipInfAdicional);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AfiliacionServices", "ConsultarAfiliacion", ex);
                return null;
            }
        }

        public List<Credito> ListarCreditosDesembolsados(Credito pCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOReporte.ListarCreditosDesembolsados(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ListarCredito", ex);
                return null;
            }
        }

    }
}