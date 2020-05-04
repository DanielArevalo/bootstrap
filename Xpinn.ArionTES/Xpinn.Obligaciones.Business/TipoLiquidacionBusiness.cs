using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Obligaciones.Data;
using Xpinn.Obligaciones.Entities;
using System.Web.UI.WebControls;


namespace Xpinn.Obligaciones.Business
{
    public class TipoLiquidacionBusiness : GlobalBusiness
    {

        private TipoLiquidacionData DATipoLiquidacion;

        /// <summary>
        /// Constructor del objeto de negocio para TipoLiquidacion
        /// </summary>
        public TipoLiquidacionBusiness()
        {
            DATipoLiquidacion = new TipoLiquidacionData();
        }

         /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pSolicitud">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoLiquidacion obtenidos</returns>
        public List<TipoLiquidacion> ListarTipoLiquidacion(TipoLiquidacion pTipLiq, Usuario pUsuario)
        {
            try
            {
                return DATipoLiquidacion.ListarTipoLiquidacion(pTipLiq, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiquidacionBusiness", "ListarTipoLiquidacion", ex);
                return null;
            }
        }


        /// <summary>
        /// Elimina un Tipo de Liquidacion
        /// </summary>
        /// <param name="pId">identificador de la Linea Obligacion</param>
        public void EliminarTipoLiq(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    DATipoLiquidacion.EliminarTipoLiq(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiquidacionBusiness", "EliminarTipoLiq", ex);
            }
        }

        /// <summary>
        /// Crea un tipo de liquidacion
        /// </summary>
        /// <param name="pEntity">Entidad Tipo Liquidacion</param>
        /// <returns>Entidad creada</returns>
        public TipoLiquidacion CrearTipoLiq(TipoLiquidacion pTipoLiq, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoLiq = DATipoLiquidacion.CrearTipoLiq(pTipoLiq, pUsuario);

                    ts.Complete();
                }

                return pTipoLiq;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiquidacionBusiness", "CrearTipoLiq", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un tipo de liquidacion
        /// </summary>
        /// <param name="pEntity">Entidad Tipo de Liquidacion</param>
        /// <returns>Entidad modificada</returns>
        public TipoLiquidacion ModificarTipoLiq(TipoLiquidacion pTipoLiq, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoLiq = DATipoLiquidacion.ModificarTipoLiq(pTipoLiq, pUsuario);

                    ts.Complete();
                }

                return pTipoLiq;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiquidacionBusiness", "ModificarTipoLiq", ex);
                return null;
            }

        }


        /// <summary>
        /// Obtiene un tipo de liquidacion
        /// </summary>
        /// <param name="pId">identificador del Tipo de Liquidacion</param>
        /// <returns>LInea consultada</returns>
        public TipoLiquidacion ConsultarTipoLiq(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DATipoLiquidacion.ConsultarTipoLiq(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiquidacionBusiness", "ConsultarTipoLiq", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene el conteo de usuarios asociados a la oficina especifica 
        /// </summary>
        /// <param name="pId">identificador de la TipoLiquidacion</param>
        /// <returns>Caja consultada</returns>
        public TipoLiquidacion ConsultarTipoLiquidacionXLineaObligacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DATipoLiquidacion.ConsultarTipoLiquidacionXLineaObligacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiquidacionBusiness", "ConsultarTipoLiquidacionXLineaObligacion", ex);
                return null;
            }
        }

    }
}
