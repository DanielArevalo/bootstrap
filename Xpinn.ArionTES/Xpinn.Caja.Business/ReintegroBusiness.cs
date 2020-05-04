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
    /// Objeto de negocio para Reintegro
    /// </summary>
    public class ReintegroBusiness:GlobalData
    {
        private ReintegroData DAReintegro;

        /// <summary>
        /// Constructor del objeto de negocio para Reintegro
        /// </summary>
        public ReintegroBusiness()
        {
            DAReintegro = new ReintegroData();
        }

        /// <summary>
        /// Crea un Reintegro
        /// </summary>
        /// <param name="pEntity">Entidad Reintegro</param>
        /// <returns>Entidad creada</returns>
        public Reintegro CrearReintegro(Reintegro pReintegro, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pReintegro = DAReintegro.InsertarReintegro(pReintegro,pUsuario);

                    ts.Complete();
                }

                return pReintegro;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReintegroBusiness", "CrearReintegro", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene una Reintegro
        /// </summary>
        /// <param name="pId">identificador del Reintegro</param>
        /// <returns>Reintegro consultada</returns>
        public Reintegro ConsultarCajero(Usuario pUsuario)
        {
            try
            {
                return DAReintegro.ConsultarCajero(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReintegroBusiness", "ConsultarCajero", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene una Reintegro
        /// </summary>
        /// <param name="pId">identificador del Reintegro</param>
        /// <returns>Reintegro consultada</returns>
        public Reintegro ConsultarFecUltCierre(Usuario pUsuario)
        {
            try
            {
                return DAReintegro.ConsultarFecUltCierre(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReintegroBusiness", "ConsultarFecUltCierre", ex);
                return null;
            }
        }
    }
}
