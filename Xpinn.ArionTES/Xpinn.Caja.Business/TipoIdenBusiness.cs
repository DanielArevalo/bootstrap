using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Caja.Data;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Business
{
    /// <summary>
    /// Objeto de negocio para TipoIden
    /// </summary>
    public class TipoIdenBusiness : GlobalBusiness
    {
        private TipoIdenData DATipoIden;

        /// <summary>
        /// Constructor del objeto de negocio para TipoIden
        /// </summary>
        public TipoIdenBusiness()
        {
            DATipoIden = new TipoIdenData();
        }

        /// <summary>
        /// Crea un TipoIden
        /// </summary>
        /// <param name="pTipoIden">Entidad TipoIden</param>
        /// <returns>Entidad TipoIden creada</returns>
        public TipoIden CrearTipoIden(TipoIden pTipoIden, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoIden = DATipoIden.CrearTipoIden(pTipoIden, pUsuario);

                    ts.Complete();
                }

                return pTipoIden;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoIdenBusiness", "CrearTipoIden", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un TipoIden
        /// </summary>
        /// <param name="pTipoIden">Entidad TipoIden</param>
        /// <returns>Entidad TipoIden modificada</returns>
        public TipoIden ModificarTipoIden(TipoIden pTipoIden, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoIden = DATipoIden.ModificarTipoIden(pTipoIden, pUsuario);

                    ts.Complete();
                }

                return pTipoIden;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoIdenBusiness", "ModificarTipoIden", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un TipoIden
        /// </summary>
        /// <param name="pId">Identificador de TipoIden</param>
        public void EliminarTipoIden(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DATipoIden.EliminarTipoIden(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoIdenBusiness", "EliminarTipoIden", ex);
            }
        }

        /// <summary>
        /// Obtiene un TipoIden
        /// </summary>
        /// <param name="pId">Identificador de TipoIden</param>
        /// <returns>Entidad TipoIden</returns>
        public TipoIden ConsultarTipoIden(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DATipoIden.ConsultarTipoIden(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoIdenBusiness", "ConsultarTipoIden", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pTipoIden">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoIden obtenidos</returns>
        public List<TipoIden> ListarTipoIden(TipoIden pTipoIden, Usuario pUsuario)
        {
            try
            {
                return DATipoIden.ListarTipoIden(pTipoIden, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoIdenBusiness", "ListarTipoIden", ex);
                return null;
            }
        }

    }
}