using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.ActivosFijos.Data;
using Xpinn.ActivosFijos.Entities;

namespace Xpinn.ActivosFijos.Business
{
    /// <summary>
    /// Objeto de negocio para EstadoActivo
    /// </summary>
    public class EstadoActivoBusiness : GlobalBusiness
    {
        private EstadoActivoData DAEstadoActivo;

        /// <summary>
        /// Constructor del objeto de negocio para EstadoActivo
        /// </summary>
        public EstadoActivoBusiness()
        {
            DAEstadoActivo = new EstadoActivoData();
        }

        /// <summary>
        /// Crea un EstadoActivo
        /// </summary>
        /// <param name="pActivosFijos">Entidad EstadoActivo</param>
        /// <returns>Entidad EstadoActivo creada</returns>
        public EstadoActivo CrearEstadoActivo(EstadoActivo pEstadoActivo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEstadoActivo = DAEstadoActivo.CrearEstadoActivo(pEstadoActivo, pUsuario);

                    ts.Complete();
                }

                return pEstadoActivo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadoActivoBusiness", "CrearEstadoActivo", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un EstadoActivo
        /// </summary>
        /// <param name="pActivosFijos">Entidad EstadoActivo</param>
        /// <returns>Entidad EstadoActivo modificada</returns>
        public EstadoActivo ModificarEstadoActivo(EstadoActivo pEstadoActivo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEstadoActivo = DAEstadoActivo.ModificarEstadoActivo(pEstadoActivo, pUsuario);

                    ts.Complete();
                }

                return pEstadoActivo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadoActivoBusiness", "ModificarEstadoActivo", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un EstadoActivo
        /// </summary>
        /// <param name="pId">Identificador de EstadoActivo</param>
        public void EliminarEstadoActivo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAEstadoActivo.EliminarEstadoActivo(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadoActivoBusiness", "EliminarEstadoActivo", ex);
            }
        }

        /// <summary>
        /// Obtiene un EstadoActivo
        /// </summary>
        /// <param name="pId">Identificador de EstadoActivo</param>
        /// <returns>Entidad EstadoActivo</returns>
        public EstadoActivo ConsultarEstadoActivo(Int64 pId, Usuario vUsuario)
        {
            try
            {
                EstadoActivo EstadoActivo = new EstadoActivo();

                EstadoActivo = DAEstadoActivo.ConsultarEstadoActivo(pId, vUsuario);

                return EstadoActivo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadoActivoBusiness", "ConsultarEstadoActivo", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pActivosFijos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de EstadoActivo obtenidos</returns>
        public List<EstadoActivo> ListarEstadoActivo(EstadoActivo pEstadoActivo, Usuario pUsuario)
        {
            try
            {
                return DAEstadoActivo.ListarEstadoActivo(pEstadoActivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadoActivoBusiness", "ListarEstadoActivo", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return DAEstadoActivo.ObtenerSiguienteCodigo(pUsuario);
            }
            catch
            {
                return 0;
            }
        }

    }
}