using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Caja.Data;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Business
{
    /// <summary>
    /// Objeto de negocio para Traslado
    /// </summary>
    public class TrasladoBusiness:GlobalData
    {
        private TrasladoData DATraslado;

        /// <summary>
        /// Constructor del objeto de negocio para Traslado
        /// </summary>
        public TrasladoBusiness()
        {
            DATraslado = new TrasladoData();
        }

        /// <summary>
        /// Crea un Traslado
        /// </summary>
        /// <param name="pEntity">Entidad Traslado</param>
        /// <returns>Entidad creada</returns>
        public Traslado CrearTraslado(Traslado pTraslado, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTraslado = DATraslado.InsertarTraslado(pTraslado,pUsuario);

                    ts.Complete();
                }

                return pTraslado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TrasladoBusiness", "CrearTraslado", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene una Traslado
        /// </summary>
        /// <param name="pId">identificador del Traslado</param>
        /// <returns>Traslado consultada</returns>
        public Traslado ConsultarCajero(Usuario pUsuario)
        {
            try
            {
                return DATraslado.ConsultarCajero(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TrasladoBusiness", "ConsultarCajero", ex);
                return null;
            }
        }


        public void EliminarTraslado(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    DATraslado.EliminarTraslado(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobadorBusiness", "EliminarOficina", ex);
            }
        }

        /// <summary>
        /// Obtiene una Traslado
        /// </summary>
        /// <param name="pId">identificador del Traslado</param>
        /// <returns>Traslado consultada</returns>
        public Cajero ConsultarCajaXCajero(Cajero pEntidad,Usuario pUsuario)
        {
            try
            {
                return DATraslado.ConsultarCajaXCajero(pEntidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TrasladoBusiness", "ConsultarCajero", ex);
                return null;
            }
        }
    }
}
