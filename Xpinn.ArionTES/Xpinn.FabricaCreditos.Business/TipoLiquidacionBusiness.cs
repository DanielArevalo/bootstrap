using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Business
{
    public class TipoLiquidacionBusiness : GlobalData
    {
        private TipoLiquidacionData DATipoLiquidacion;

        /// <summary>
        /// Constructor del objeto de negocio para Caja
        /// </summary>
        public TipoLiquidacionBusiness()
        {
            DATipoLiquidacion = new TipoLiquidacionData();
        }

        /// <summary>
        /// Crea un TipoLiquidacion
        /// </summary>
        /// <param name="pEntity">Entidad TipoLiquidacion</param>
        /// <returns>Entidad creada</returns>
        public TipoLiquidacion CrearTipoLiquidacion(TipoLiquidacion pTipoLiquidacion, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoLiquidacion = DATipoLiquidacion.CrearTipoLiquidacion(pTipoLiquidacion, pUsuario);
                    ts.Complete();
                }
                return pTipoLiquidacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiquidacionBusiness", "CrearTipoLiquidacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de TipoLiquidaciones
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de TipoLiquidaciones obtenidos</returns>
        public List<TipoLiquidacion> ListarTipoLiquidacion(TipoLiquidacion pTipoLiquidacion, Usuario pUsuario)
        {
            try
            {
                return DATipoLiquidacion.ListarTipoLiquidacion(pTipoLiquidacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiquidacionBusiness", "ListarTipoLiquidacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un TipoLiquidacion
        /// </summary>
        /// <param name="pId">identificador del TipoLiquidacion</param>
        public void EliminarTipoLiquidacion(Int32 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    DATipoLiquidacion.EliminarTipoLiquidacion(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiquidacionBusiness", "EliminarOficina", ex);
            }
        }

        /// <summary>
        /// Modifica un TipoLiquidacion
        /// </summary>
        /// <param name="pEntity">Entidad TipoLiquidacion</param>
        /// <returns>Entidad modificada</returns>
        public TipoLiquidacion ModificarTipoLiquidacion(TipoLiquidacion pTipoLiquidacion, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoLiquidacion = DATipoLiquidacion.ModificarTipoLiquidacion(pTipoLiquidacion, pUsuario);

                    ts.Complete();
                }

                return pTipoLiquidacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiquidacionBusiness", "ModificarTipoLiquidacion", ex);
                return null;
            }

        }

        /// <summary>
        /// Obtiene los datos de un TipoLiquidacion
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de TipoLiquidaciones obtenidos</returns>
        public TipoLiquidacion ConsultarTipoLiquidacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DATipoLiquidacion.ConsultarTipoLiquidacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiquidacionBusiness", "ConsultarTipoLiquidacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene el ultimo Id de un TipoLiquidacion
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>IdTipoLiquidacion obtenido</returns>
        public object UltimoIdTipoLiquidacion(Usuario pUsuario)
        {
            try
            {
                return DATipoLiquidacion.UltimoIdTipoLiquidacion(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiquidacionBusiness", "UltimoIdTipoLiquidacion", ex);
                return null;
            }
        }

    }
}
