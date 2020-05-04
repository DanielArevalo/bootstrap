using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Business
{
    /// <summary>
    /// Objeto de negocio para CalificacionReferencias
    /// </summary>
    public class CalificacionReferenciasBusiness : GlobalBusiness
    {
        private CalificacionReferenciasData DACalificacionReferencias;

        /// <summary>
        /// Constructor del objeto de negocio para CalificacionReferencias
        /// </summary>
        public CalificacionReferenciasBusiness()
        {
            DACalificacionReferencias = new CalificacionReferenciasData();
        }

        /// <summary>
        /// Crea un CalificacionReferencias
        /// </summary>
        /// <param name="pCalificacionReferencias">Entidad CalificacionReferencias</param>
        /// <returns>Entidad CalificacionReferencias creada</returns>
        public CalificacionReferencias CrearCalificacionReferencias(CalificacionReferencias pCalificacionReferencias, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCalificacionReferencias = DACalificacionReferencias.CrearCalificacionReferencias(pCalificacionReferencias, pUsuario);

                    ts.Complete();
                }

                return pCalificacionReferencias;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CalificacionReferenciasBusiness", "CrearCalificacionReferencias", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un CalificacionReferencias
        /// </summary>
        /// <param name="pCalificacionReferencias">Entidad CalificacionReferencias</param>
        /// <returns>Entidad CalificacionReferencias modificada</returns>
        public CalificacionReferencias ModificarCalificacionReferencias(CalificacionReferencias pCalificacionReferencias, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCalificacionReferencias = DACalificacionReferencias.ModificarCalificacionReferencias(pCalificacionReferencias, pUsuario);

                    ts.Complete();
                }

                return pCalificacionReferencias;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CalificacionReferenciasBusiness", "ModificarCalificacionReferencias", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un CalificacionReferencias
        /// </summary>
        /// <param name="pId">Identificador de CalificacionReferencias</param>
        public void EliminarCalificacionReferencias(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DACalificacionReferencias.EliminarCalificacionReferencias(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CalificacionReferenciasBusiness", "EliminarCalificacionReferencias", ex);
            }
        }

        /// <summary>
        /// Obtiene un CalificacionReferencias
        /// </summary>
        /// <param name="pId">Identificador de CalificacionReferencias</param>
        /// <returns>Entidad CalificacionReferencias</returns>
        public CalificacionReferencias ConsultarCalificacionReferencias(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DACalificacionReferencias.ConsultarCalificacionReferencias(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CalificacionReferenciasBusiness", "ConsultarCalificacionReferencias", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCalificacionReferencias">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de CalificacionReferencias obtenidos</returns>
        public List<CalificacionReferencias> ListarCalificacionReferencias(CalificacionReferencias pCalificacionReferencias, Usuario pUsuario)
        {
            try
            {
                return DACalificacionReferencias.ListarCalificacionReferencias(pCalificacionReferencias, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CalificacionReferenciasBusiness", "ListarCalificacionReferencias", ex);
                return null;
            }
        }

    }
}