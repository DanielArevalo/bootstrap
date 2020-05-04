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
    public class AuditoriaService
    {
        private AuditoriaBusiness BOAuditoria;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Auditoria
        /// </summary>
        public AuditoriaService()
        {
            BOAuditoria = new AuditoriaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "90201"; } }
        public string CodigoProgramaAuditoriaTriggers { get { return "90114"; } }

        /// <summary>
        /// Servicio para obtener Auditoria
        /// </summary>
        /// <param name="pId">identificador de Auditoria</param>
        /// <returns>Entidad Auditoria</returns>
        public Auditoria ConsultarAuditoria(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOAuditoria.ConsultarAuditoria(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuditoriaService", "ConsultarAuditoria", ex);
                return null;
            }
        }

        public void CrearTablaAuditoria(string tablas, string operacion, Usuario usuario)
        {
            try
            {
                BOAuditoria.CrearTablaAuditoria(tablas, operacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuditoriaService", "CrearTablaAuditoria", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Auditorias a partir de unos filtros
        /// </summary>
        /// <param name="pAuditoria">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Auditoria obtenidos</returns>
        public List<Auditoria> ListarAuditoria(Auditoria pAuditoria, Usuario pUsuario)
        {
            try
            {
                return BOAuditoria.ListarAuditoria(pAuditoria, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuditoriaService", "ListarAuditoria", ex);
                return null;
            }
        }
    }
}