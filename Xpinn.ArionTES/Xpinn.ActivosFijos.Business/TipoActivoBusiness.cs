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
    /// Objeto de negocio para TipoActivo
    /// </summary>
    public class TipoActivoBusiness : GlobalBusiness
    {
        private TipoActivoData DATipoActivo;

        /// <summary>
        /// Constructor del objeto de negocio para TipoActivo
        /// </summary>
        public TipoActivoBusiness()
        {
            DATipoActivo = new TipoActivoData();
        }

        /// <summary>
        /// Crea un TipoActivo
        /// </summary>
        /// <param name="pTipoActivo">Entidad TipoActivo</param>
        /// <returns>Entidad TipoActivo creada</returns>
        public TipoActivo CrearTipoActivo(TipoActivo pTipoActivo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoActivo = DATipoActivo.CrearTipoActivo(pTipoActivo, pUsuario);

                    ts.Complete();
                }

                return pTipoActivo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoActivoBusiness", "CrearTipoActivo", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un TipoActivo
        /// </summary>
        /// <param name="pTipoActivo">Entidad TipoActivo</param>
        /// <returns>Entidad TipoActivo modificada</returns>
        public TipoActivo ModificarTipoActivo(TipoActivo pTipoActivo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoActivo = DATipoActivo.ModificarTipoActivo(pTipoActivo, pUsuario);

                    ts.Complete();
                }

                return pTipoActivo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoActivoBusiness", "ModificarTipoActivo", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un TipoActivo
        /// </summary>
        /// <param name="pId">Identificador de TipoActivo</param>
        public void EliminarTipoActivo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DATipoActivo.EliminarTipoActivo(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoActivoBusiness", "EliminarTipoActivo", ex);
            }
        }

        /// <summary>
        /// Obtiene un TipoActivo
        /// </summary>
        /// <param name="pId">Identificador de TipoActivo</param>
        /// <returns>Entidad TipoActivo</returns>
        public TipoActivo ConsultarTipoActivo(Int64 pId, Usuario vUsuario)
        {
            try
            {
                TipoActivo TipoActivo = new TipoActivo();

                TipoActivo = DATipoActivo.ConsultarTipoActivo(pId, vUsuario);

                return TipoActivo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoActivoBusiness", "ConsultarTipoActivo", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pTipoActivo">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoActivo obtenidos</returns>
        public List<TipoActivo> ListarTipoActivo(TipoActivo pTipoActivo, Usuario pUsuario)
        {
            try
            {
                return DATipoActivo.ListarTipoActivo(pTipoActivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoActivoBusiness", "ListarTipoActivo", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return DATipoActivo.ObtenerSiguienteCodigo(pUsuario);
            }
            catch
            {
                return 0;
            }
        }


        //AGREGADO

        public List<TipoActivo> ListarTipoActivo_NIIF(TipoActivo pTipoActivo, Usuario pUsuario)
        {
            try
            {
                return DATipoActivo.ListarTipoActivo_NIIF(pTipoActivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoActivoBusiness", "ListarTipoActivo_NIIF", ex);
                return null;
            }
        }


        public List<TipoActivo> ListarUniGeneradora_NIIF(TipoActivo pTipoActivo, Usuario pUsuario)
        {
            try
            {
                return DATipoActivo.ListarUniGeneradora_NIIF(pTipoActivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoActivoBusiness", "ListarUniGeneradora_NIIF", ex);
                return null;
            }
        }

    }
}