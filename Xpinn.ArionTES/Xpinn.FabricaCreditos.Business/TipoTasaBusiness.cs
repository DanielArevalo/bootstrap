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
    /// Objeto de negocio para codeudores
    /// </summary>
    public class TipoTasaBusiness : GlobalBusiness
    {
        private TipoTasaData DATipoTasa;

        /// <summary>
        /// Constructor del objeto de negocio para codeudores
        /// </summary>
        public TipoTasaBusiness()
        {
            DATipoTasa = new TipoTasaData();
        }

        /// <summary>
        /// Crea un tipo de tasa
        /// </summary>
        /// <param name="pUsuario">Entidad tipotasa</param>
        /// <returns>Entidad tipotasa creada</returns>
        public TipoTasa CrearTipoTasa(TipoTasa pTipoTasa, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoTasa = DATipoTasa.CrearTipoTasa(pTipoTasa, vUsuario);

                    ts.Complete();
                }

                return pTipoTasa;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoTasaBusiness", "CrearTipoTasa", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un tipo de tasa
        /// </summary>
        /// <param name="pUsuario">Entidad tipo de tasa</param>
        /// <returns>Entidad tipo tasa modificada</returns>
        public TipoTasa ModificarTipoTasa(TipoTasa pTipoTasa, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoTasa = DATipoTasa.ModificarTipoTasa(pTipoTasa, vUsuario);

                    ts.Complete();
                }

                return pTipoTasa;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoTasaBusiness", "ModificarTipoTasa", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un tipo de tasa
        /// </summary>
        /// <param name="pId">Identificador de tipo de tasa</param>
        public void EliminarTipoTasa(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DATipoTasa.EliminarTipoTasa(pId, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoTasaBusiness", "EliminarTipoTasa", ex);
            }
        }

        /// <summary>
        /// Obtiene un Usuario
        /// </summary>
        /// <param name="pId">Identificador de Usuario</param>
        /// <returns>Entidad Usuario</returns>
        public TipoTasa ConsultarTipoTasa(Int64 pId, Usuario vUsuario)
        {
            try
            {
                TipoTasa tipotasa = new TipoTasa();

                tipotasa = DATipoTasa.ConsultarTipoTasa(pId, vUsuario);

                return tipotasa;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoTasaBusiness", "ConsultarTipoTasa", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pUsuario">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de tipo de tasa obtenidos</returns>
        public List<TipoTasa> ListarTipoTasa(TipoTasa pTipoTasa, Usuario vUsuario)
        {
            try
            {
                return DATipoTasa.ListarTipoTasa(pTipoTasa, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoTasaBusiness", "ListarUsuario", ex);
                return null;
            }
        }

    }
}