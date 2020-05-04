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
    /// Objeto de negocio para TipoPago
    /// </summary>
    public class TipoPagoBusiness : GlobalBusiness
    {
        private TipoPagoData DATipoPago;

        /// <summary>
        /// Constructor del objeto de negocio para TipoPago
        /// </summary>
        public TipoPagoBusiness()
        {
            DATipoPago = new TipoPagoData();
        }

        /// <summary>
        /// Crea un TipoPago
        /// </summary>
        /// <param name="pTipoPago">Entidad TipoPago</param>
        /// <returns>Entidad TipoPago creada</returns>
        public TipoPago CrearTipoPago(TipoPago pTipoPago, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoPago = DATipoPago.CrearTipoPago(pTipoPago, pUsuario);

                    ts.Complete();
                }

                return pTipoPago;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoPagoBusiness", "CrearTipoPago", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un TipoPago
        /// </summary>
        /// <param name="pTipoPago">Entidad TipoPago</param>
        /// <returns>Entidad TipoPago modificada</returns>
        public TipoPago ModificarTipoPago(TipoPago pTipoPago, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoPago = DATipoPago.ModificarTipoPago(pTipoPago, pUsuario);

                    ts.Complete();
                }

                return pTipoPago;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoPagoBusiness", "ModificarTipoPago", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un TipoPago
        /// </summary>
        /// <param name="pId">Identificador de TipoPago</param>
        public void EliminarTipoPago(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DATipoPago.EliminarTipoPago(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoPagoBusiness", "EliminarTipoPago", ex);
            }
        }

        /// <summary>
        /// Obtiene un TipoPago
        /// </summary>
        /// <param name="pId">Identificador de TipoPago</param>
        /// <returns>Entidad TipoPago</returns>
        public TipoPago ConsultarTipoPago(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DATipoPago.ConsultarTipoPago(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoPagoBusiness", "ConsultarTipoPago", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pTipoPago">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoPago obtenidos</returns>
        public List<TipoPago> ListarTipoPago(TipoPago pTipoPago, Usuario pUsuario)
        {
            try
            {
                return DATipoPago.ListarTipoPago(pTipoPago, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoPagoBusiness", "ListarTipoPago", ex);
                return null;
            }
        }


        public List<TipoPago> ListarTipoPagoCon(TipoPago pTipoPago, Usuario pUsuario)
        {
            try
            {
                return DATipoPago.ListarTipoPagoCon(pTipoPago, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoPagoBusiness", "ListarTipoPagoCon", ex);
                return null;
            }
        }

    }
}