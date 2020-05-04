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
    /// Objeto de negocio para Auditoria
    /// </summary>
    public class AuditoriaBusiness : GlobalBusiness
    {
        private AuditoriaData DAAuditoria;

        /// <summary>
        /// Constructor del objeto de negocio para Auditoria
        /// </summary>
        public AuditoriaBusiness()
        {
            DAAuditoria = new AuditoriaData();
        }

        /// <summary>
        /// Obtiene un Auditoria
        /// </summary>
        /// <param name="pId">Identificador de Auditoria</param>
        /// <returns>Entidad Auditoria</returns>
        public Auditoria ConsultarAuditoria(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAAuditoria.ConsultarAuditoria(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuditoriaBusiness", "ConsultarAuditoria", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pAuditoria">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Auditoria obtenidos</returns>
        public List<Auditoria> ListarAuditoria(Auditoria pAuditoria, Usuario pUsuario)
        {
            try
            {
                return DAAuditoria.ListarAuditoria(pAuditoria, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuditoriaBusiness", "ListarAuditoria", ex);
                return null;
            }
        }

        public void CrearTablaAuditoria(string tablas, string operacion, Usuario usuario)
        {
            try
            {
                DAAuditoria.CrearTablaAuditoria(tablas, operacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuditoriaBusiness", "CrearTablaAuditoria", ex);
            }
        }
    }
}