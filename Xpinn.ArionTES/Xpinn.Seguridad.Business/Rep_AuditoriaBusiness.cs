using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Seguridad.Data;
using Xpinn.Seguridad.Entities;

namespace Xpinn.Seguridad.Business
{
    /// <summary>
    /// Objeto de negocio para Proceso
    /// </summary>
    public class Rep_AuditoriaBusiness : GlobalBusiness
    {
        private Rep_AuditoriaData DAReporteAuditoria;

        /// <summary>
        /// Constructor del objeto de negocio para Auditoria
        /// </summary>
        public Rep_AuditoriaBusiness()
        {
            DAReporteAuditoria = new Rep_AuditoriaData();
        }



        /// <summary>
        /// Obtiene un Proceso
        /// </summary>
        /// <param name="pId">Identificador de Proceso</param>
        /// <returns>Entidad Proceso</returns>
        public ReporteAuditoria ConsultarReportPersonas(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAReporteAuditoria.ConsultarReportPersonas(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Rep_AuditoriaBusiness", "ConsultarReportPersonas", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un Proceso
        /// </summary>
        /// <param name="pId">Identificador de Proceso</param>
        /// <returns>Entidad Proceso</returns>
        public ReporteAuditoria ConsultarRep_aud_credito(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAReporteAuditoria.ConsultarRep_aud_credito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Rep_AuditoriaBusiness", "ConsultarRep_aud_credito", ex);
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
                return DAReporteAuditoria.ConsultarReporte(pProceso, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoService", "ListarReporteAuditoria", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pProceso">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Proceso obtenidos</returns>
        public List<ReporteAuditoria> ListarRep_aud_persona(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAReporteAuditoria.ListarRep_aud_persona(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Rep_AuditoriaBusiness", "ListarRep_aud_persona", ex);
                return null;
            }
        }



        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pProceso">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Proceso obtenidos</returns>
        public List<ReporteAuditoria> ListarRep_aud_credito(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAReporteAuditoria.ListarRep_aud_credito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Rep_AuditoriaBusiness", "ListarRep_aud_credito", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pProceso">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Proceso obtenidos</returns>
        public List<ReporteAuditoria> ListarRep_aud_operacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAReporteAuditoria.ListarRep_aud_operacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Rep_AuditoriaBusiness", "ListarRep_aud_operacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pProceso">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Proceso obtenidos</returns>
        public List<ReporteAuditoria> ListarRep_aud_comprobante(Int64 pId, string pTipo, Usuario pUsuario)
        {
            try
            {
                return DAReporteAuditoria.ListarRep_aud_comprobante(pId, pTipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Rep_AuditoriaBusiness", "ListarRep_aud_comprobante", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un Proceso
        /// </summary>
        /// <param name="pId">Identificador de Proceso</param>
        /// <returns>Entidad Proceso</returns>
        public List<ReporteAuditoria> ConsultarReporte(ReporteAuditoria pReporteAuditoria, Usuario pUsuario)
        {
            try
            {
                return DAReporteAuditoria.ConsultarReporte(pReporteAuditoria, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Rep_AuditoriaBusiness", "ConsultarReporte", ex);
                return null;
            }
        }
        public List<ReporteAuditoria> ConsultarReportePerfil(string pReporteAuditoria, Usuario pUsuario)
        {
            try
            {
                return DAReporteAuditoria.ConsultarReportePerfil(pReporteAuditoria, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Rep_AuditoriaBusiness", "ConsultarReportePerfil", ex);
                return null;
            }
        }

        public List<Auditoria> ListarAuditoriaDeTablaAuditoria(int tipoReporte, Usuario usuario)
        {
            try
            {
                return DAReporteAuditoria.ListarAuditoriaDeTablaAuditoria(tipoReporte, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Rep_AuditoriaBusiness", "ListarAuditoriaDeTablaAuditoria", ex);
                return null;
            }
        }

        public DataTable GenerarReporte(string tablaAuditoria, ref string[] aColumnas, ref Type[] aTipos, ref int numerocolumnas, ref string sError, Usuario pUsuario)
        {
            try
            {
                return DAReporteAuditoria.GenerarReporte(tablaAuditoria, ref aColumnas, ref aTipos, ref numerocolumnas, ref sError, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Rep_AuditoriaBusiness", "GenerarReporte", ex);
                return null;
            }
        }
    }
}