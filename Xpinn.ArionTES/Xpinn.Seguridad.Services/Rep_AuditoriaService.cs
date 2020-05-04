using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Seguridad.Business;
using Xpinn.Seguridad.Entities;

namespace Xpinn.Seguridad.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class Rep_AuditoriaService
    {
        private Rep_AuditoriaBusiness BOReporte;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Proceso
        /// </summary>
        public Rep_AuditoriaService()
        {
            BOReporte = new Rep_AuditoriaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "90107"; } }

       

        /// <summary>
        /// Servicio para obtener Proceso
        /// </summary>
        /// <param name="pId">identificador de Proceso</param>
        /// <returns>Entidad Proceso</returns>
        public ReporteAuditoria ConsultarReportPersonas(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOReporte.ConsultarReportPersonas(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoService", "ConsultarReportPersonas", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Procesos a partir de unos filtros
        /// </summary>
        /// <param name="pProceso">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Proceso obtenidos</returns>
        public List<ReporteAuditoria> ListarReporteAuditoria(ReporteAuditoria pProceso, Usuario pUsuario)
        {
            try
            {
                return BOReporte.ListarReporteAuditoria(pProceso, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoService", "ListarReporteAuditoria", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener lista de Procesos a partir de unos filtros
        /// </summary>
        /// <param name="pProceso">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Proceso obtenidos</returns>
        public List<ReporteAuditoria> ListarRep_aud_credito(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOReporte.ListarRep_aud_credito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoService", "ListarRep_aud_credito", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Procesos a partir de unos filtros
        /// </summary>
        /// <param name="pProceso">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Proceso obtenidos</returns>
        public List<ReporteAuditoria> ListarRep_aud_operacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOReporte.ListarRep_aud_operacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoService", "ListarRep_aud_operacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Procesos a partir de unos filtros
        /// </summary>
        /// <param name="pProceso">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Proceso obtenidos</returns>
        public List<ReporteAuditoria> ListarRep_aud_comprobante(Int64 pId, string pTipo, Usuario pUsuario)
        {
            try
            {
                return BOReporte.ListarRep_aud_comprobante(pId, pTipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoService", "ListarRep_aud_comprobante", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener lista de Procesos a partir de unos filtros
        /// </summary>
        /// <param name="pProceso">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Proceso obtenidos</returns>
        public List<ReporteAuditoria> ListarRep_aud_persona(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOReporte.ListarRep_aud_persona(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoService", "ListarRep_aud_persona", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Procesos a partir de unos filtros
        /// </summary>
        /// <param name="pProceso">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Proceso obtenidos</returns>
        public List<ReporteAuditoria> ConsultarReporte(ReporteAuditoria pReporteAuditoria, Usuario pUsuario)
        {
            try
            {
                return BOReporte.ConsultarReporte(pReporteAuditoria, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoService", "ListarReporteAuditoria", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener Reporte control perfiles de usuarios
        /// </summary>
        /// <param name="pProceso">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Proceso obtenidos</returns>
        public List<ReporteAuditoria> ConsultarReportePerfil(string pReporteAuditoria, Usuario pUsuario)
        {
            try
            {
                return BOReporte.ConsultarReportePerfil(pReporteAuditoria, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoService", "ConsultarReportePerfil", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Procesos a partir de unos filtros
        /// </summary>
        /// <param name="pProceso">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Proceso obtenidos</returns>
        public ReporteAuditoria ConsultarRep_aud_credito(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOReporte.ConsultarRep_aud_credito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoService", "ConsultarRep_aud_credito", ex);
                return null;
            }
        }

        public List<Auditoria> ListarAuditoriaDeTablaAuditoria(int tipoReporte, Usuario usuario)
        {
            try
            {
                return BOReporte.ListarAuditoriaDeTablaAuditoria(tipoReporte, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoService", "ListarAuditoriaDeTablaAuditoria", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para obtener lista de auditoria para llenar tabla dinamica
        /// </summary>
        /// <param name="aColumnas">Número de columnas</param>
        /// <returns>DataReader con la tabla de auditoria</returns>
        public DataTable GenerarReporte(string tablaAuditoria, ref string[] aColumnas, ref Type[] aTipos, ref int numerocolumnas, ref string sError, Usuario pUsuario)
        {
            try
            {
                return BOReporte.GenerarReporte(tablaAuditoria, ref aColumnas, ref aTipos, ref numerocolumnas, ref sError, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Rep_AuditoriaBusiness", "GenerarReporte", ex);
                return null;
            }
        }
    }
}