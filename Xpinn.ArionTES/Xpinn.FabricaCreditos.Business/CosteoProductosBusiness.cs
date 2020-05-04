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
    /// Objeto de negocio para CosteoProductos
    /// </summary>
    public class CosteoProductosBusiness : GlobalData
    {
        private CosteoProductosData DACosteoProductos;

        /// <summary>
        /// Constructor del objeto de negocio para CosteoProductos
        /// </summary>
        public CosteoProductosBusiness()
        {
            DACosteoProductos = new CosteoProductosData();
        }

        /// <summary>
        /// Crea un CosteoProductos
        /// </summary>
        /// <param name="pCosteoProductos">Entidad CosteoProductos</param>
        /// <returns>Entidad CosteoProductos creada</returns>
        public CosteoProductos CrearCosteoProductos(CosteoProductos pCosteoProductos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCosteoProductos = DACosteoProductos.CrearCosteoProductos(pCosteoProductos, pUsuario);

                    ts.Complete();
                }

                return pCosteoProductos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CosteoProductosBusiness", "CrearCosteoProductos", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un CosteoProductos
        /// </summary>
        /// <param name="pCosteoProductos">Entidad CosteoProductos</param>
        /// <returns>Entidad CosteoProductos modificada</returns>
        public CosteoProductos ModificarCosteoProductos(CosteoProductos pCosteoProductos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCosteoProductos = DACosteoProductos.ModificarCosteoProductos(pCosteoProductos, pUsuario);

                    ts.Complete();
                }

                return pCosteoProductos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CosteoProductosBusiness", "ModificarCosteoProductos", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un CosteoProductos
        /// </summary>
        /// <param name="pId">Identificador de CosteoProductos</param>
        public void EliminarCosteoProductos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DACosteoProductos.EliminarCosteoProductos(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CosteoProductosBusiness", "EliminarCosteoProductos", ex);
            }
        }

        /// <summary>
        /// Obtiene un CosteoProductos
        /// </summary>
        /// <param name="pId">Identificador de CosteoProductos</param>
        /// <returns>Entidad CosteoProductos</returns>
        public CosteoProductos ConsultarCosteoProductos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DACosteoProductos.ConsultarCosteoProductos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CosteoProductosBusiness", "ConsultarCosteoProductos", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCosteoProductos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de CosteoProductos obtenidos</returns>
        public List<CosteoProductos> ListarCosteoProductos(CosteoProductos pCosteoProductos, Usuario pUsuario)
        {
            try
            {
                return DACosteoProductos.ListarCosteoProductos(pCosteoProductos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CosteoProductosBusiness", "ListarCosteoProductos", ex);
                return null;
            }
        }

    }
}