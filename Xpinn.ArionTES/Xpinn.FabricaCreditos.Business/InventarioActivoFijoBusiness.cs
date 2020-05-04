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
    /// Objeto de negocio para InventarioActivoFijo
    /// </summary>
    public class InventarioActivoFijoBusiness : GlobalData
    {
        private InventarioActivoFijoData DAInventarioActivoFijo;

        /// <summary>
        /// Constructor del objeto de negocio para InventarioActivoFijo
        /// </summary>
        public InventarioActivoFijoBusiness()
        {
            DAInventarioActivoFijo = new InventarioActivoFijoData();
        }

        /// <summary>
        /// Crea un InventarioActivoFijo
        /// </summary>
        /// <param name="pInventarioActivoFijo">Entidad InventarioActivoFijo</param>
        /// <returns>Entidad InventarioActivoFijo creada</returns>
        public InventarioActivoFijo CrearInventarioActivoFijo(InventarioActivoFijo pInventarioActivoFijo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pInventarioActivoFijo = DAInventarioActivoFijo.CrearInventarioActivoFijo(pInventarioActivoFijo, pUsuario);

                    ts.Complete();
                }

                return pInventarioActivoFijo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventarioActivoFijoBusiness", "CrearInventarioActivoFijo", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un InventarioActivoFijo
        /// </summary>
        /// <param name="pInventarioActivoFijo">Entidad InventarioActivoFijo</param>
        /// <returns>Entidad InventarioActivoFijo modificada</returns>
        public InventarioActivoFijo ModificarInventarioActivoFijo(InventarioActivoFijo pInventarioActivoFijo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pInventarioActivoFijo = DAInventarioActivoFijo.ModificarInventarioActivoFijo(pInventarioActivoFijo, pUsuario);

                    ts.Complete();
                }

                return pInventarioActivoFijo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventarioActivoFijoBusiness", "ModificarInventarioActivoFijo", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un InventarioActivoFijo
        /// </summary>
        /// <param name="pId">Identificador de InventarioActivoFijo</param>
        public void EliminarInventarioActivoFijo(Int64 pId, Usuario pUsuario, Int64 Cod_persona, Int64 Cod_InfFin)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAInventarioActivoFijo.EliminarInventarioActivoFijo(pId, pUsuario, Cod_persona, Cod_InfFin);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventarioActivoFijoBusiness", "EliminarInventarioActivoFijo", ex);
            }
        }

        /// <summary>
        /// Obtiene un InventarioActivoFijo
        /// </summary>
        /// <param name="pId">Identificador de InventarioActivoFijo</param>
        /// <returns>Entidad InventarioActivoFijo</returns>
        public InventarioActivoFijo ConsultarInventarioActivoFijo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAInventarioActivoFijo.ConsultarInventarioActivoFijo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventarioActivoFijoBusiness", "ConsultarInventarioActivoFijo", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pInventarioActivoFijo">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InventarioActivoFijo obtenidos</returns>
        public List<InventarioActivoFijo> ListarInventarioActivoFijo(InventarioActivoFijo pInventarioActivoFijo, Usuario pUsuario)
        {
            try
            {
                return DAInventarioActivoFijo.ListarInventarioActivoFijo(pInventarioActivoFijo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventarioActivoFijoBusiness", "ListarInventarioActivoFijo", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pInventarioActivoFijo">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InventarioActivoFijo obtenidos</returns>
        public List<InventarioActivoFijo> ListarInventarioActivoFijoRepo(InventarioActivoFijo pInventarioActivoFijo, Usuario pUsuario)
        {
            try
            {
                return DAInventarioActivoFijo.ListarInventarioActivoFijoRepo(pInventarioActivoFijo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventarioActivoFijoBusiness", "ListarInventarioActivoFijoRepo", ex);
                return null;
            }
        }

    }
}