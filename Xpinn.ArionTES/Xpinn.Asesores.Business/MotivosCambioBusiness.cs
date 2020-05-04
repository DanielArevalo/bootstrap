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
    /// Objeto de negocio para MotivosCambio
    /// </summary>
    public class MotivosCambioBusiness : GlobalBusiness
    {
        private MotivosCambioData DAMotivosCambio;

        /// <summary>
        /// Constructor del objeto de negocio para MotivosCambio
        /// </summary>
        public MotivosCambioBusiness()
        {
            DAMotivosCambio = new MotivosCambioData();
        }

        /// <summary>
        /// Crea un MotivosCambio
        /// </summary>
        /// <param name="pMotivosCambio">Entidad MotivosCambio</param>
        /// <returns>Entidad MotivosCambio creada</returns>
        public MotivosCambio CrearMotivosCambio(MotivosCambio pMotivosCambio, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pMotivosCambio = DAMotivosCambio.CrearMotivosCambio(pMotivosCambio, pUsuario);

                    ts.Complete();
                }

                return pMotivosCambio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivosCambioBusiness", "CrearMotivosCambio", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un MotivosCambio
        /// </summary>
        /// <param name="pMotivosCambio">Entidad MotivosCambio</param>
        /// <returns>Entidad MotivosCambio modificada</returns>
        public MotivosCambio ModificarMotivosCambio(MotivosCambio pMotivosCambio, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pMotivosCambio = DAMotivosCambio.ModificarMotivosCambio(pMotivosCambio, pUsuario);

                    ts.Complete();
                }

                return pMotivosCambio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivosCambioBusiness", "ModificarMotivosCambio", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un MotivosCambio
        /// </summary>
        /// <param name="pId">Identificador de MotivosCambio</param>
        public void EliminarMotivosCambio(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAMotivosCambio.EliminarMotivosCambio(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivosCambioBusiness", "EliminarMotivosCambio", ex);
            }
        }

        /// <summary>
        /// Obtiene un MotivosCambio
        /// </summary>
        /// <param name="pId">Identificador de MotivosCambio</param>
        /// <returns>Entidad MotivosCambio</returns>
        public MotivosCambio ConsultarMotivosCambio(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAMotivosCambio.ConsultarMotivosCambio(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivosCambioBusiness", "ConsultarMotivosCambio", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pMotivosCambio">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MotivosCambio obtenidos</returns>
        public List<MotivosCambio> ListarMotivosCambio(MotivosCambio pMotivosCambio, Usuario pUsuario)
        {
            try
            {
                return DAMotivosCambio.ListarMotivosCambio(pMotivosCambio, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivosCambioBusiness", "ListarMotivosCambio", ex);
                return null;
            }
        }

    }
}