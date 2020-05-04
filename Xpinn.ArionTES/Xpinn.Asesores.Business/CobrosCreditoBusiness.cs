using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Asesores.Data;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Business
{
    /// <summary>
    /// Objeto de negocio para CobrosCredito
    /// </summary>
    public class CobrosCreditoBusiness : GlobalBusiness
    {
        private CobrosCreditoData DACobrosCredito;

        /// <summary>
        /// Constructor del objeto de negocio para CobrosCredito
        /// </summary>
        public CobrosCreditoBusiness()
        {
            DACobrosCredito = new CobrosCreditoData();
        }

        /// <summary>
        /// Crea un CobrosCredito
        /// </summary>
        /// <param name="pCobrosCredito">Entidad CobrosCredito</param>
        /// <returns>Entidad CobrosCredito creada</returns>
        public CobrosCredito CrearCobrosCredito(CobrosCredito pCobrosCredito, Usuario pUsuario)
        {
            try
            {
              //  using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCobrosCredito = DACobrosCredito.CrearCobrosCredito(pCobrosCredito, pUsuario);

                   // ts.Complete();
                }

                return pCobrosCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CobrosCreditoBusiness", "CrearCobrosCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un CobrosCredito
        /// </summary>
        /// <param name="pCobrosCredito">Entidad CobrosCredito</param>
        /// <returns>Entidad CobrosCredito modificada</returns>
        public CobrosCredito ModificarCobrosCredito(CobrosCredito pCobrosCredito, Usuario pUsuario)
        {
            try
            {
              //  using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCobrosCredito = DACobrosCredito.ModificarCobrosCredito(pCobrosCredito, pUsuario);

                 //   ts.Complete();
                }

                return pCobrosCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CobrosCreditoBusiness", "ModificarCobrosCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un CobrosCredito
        /// </summary>
        /// <param name="pId">Identificador de CobrosCredito</param>
        public void EliminarCobrosCredito(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DACobrosCredito.EliminarCobrosCredito(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CobrosCreditoBusiness", "EliminarCobrosCredito", ex);
            }
        }

        /// <summary>
        /// Obtiene un CobrosCredito
        /// </summary>
        /// <param name="pId">Identificador de CobrosCredito</param>
        /// <returns>Entidad CobrosCredito</returns>
        public CobrosCredito ConsultarCobrosCredito(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DACobrosCredito.ConsultarCobrosCredito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CobrosCreditoBusiness", "ConsultarCobrosCredito", ex);
                return null;
            }
        }

        /// <summary>  
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCobrosCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de CobrosCredito obtenidos</returns>
        public List<CobrosCredito> ListarCobrosCredito(CobrosCredito pCobrosCredito, Usuario pUsuario)
        {
            try
            {
                return DACobrosCredito.ListarCobrosCredito(pCobrosCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CobrosCreditoBusiness", "ListarCobrosCredito", ex);
                return null;
            }
        }

    }
}