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
    /// Objeto de negocio para ProductosProceso
    /// </summary>
    public class ProductosProcesoBusiness : GlobalData
    {
        private ProductosProcesoData DAProductosProceso;

        /// <summary>
        /// Constructor del objeto de negocio para ProductosProceso
        /// </summary>
        public ProductosProcesoBusiness()
        {
            DAProductosProceso = new ProductosProcesoData();
        }

        /// <summary>
        /// Crea un ProductosProceso
        /// </summary>
        /// <param name="pProductosProceso">Entidad ProductosProceso</param>
        /// <returns>Entidad ProductosProceso creada</returns>
        public ProductosProceso CrearProductosProceso(ProductosProceso pProductosProceso, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pProductosProceso.valortotal = pProductosProceso.valunitario * pProductosProceso.cantidad;
                    pProductosProceso = DAProductosProceso.CrearProductosProceso(pProductosProceso, pUsuario);

                    ts.Complete();
                }

                return pProductosProceso;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosProcesoBusiness", "CrearProductosProceso", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un ProductosProceso
        /// </summary>
        /// <param name="pProductosProceso">Entidad ProductosProceso</param>
        /// <returns>Entidad ProductosProceso modificada</returns>
        public ProductosProceso ModificarProductosProceso(ProductosProceso pProductosProceso, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pProductosProceso.valortotal = pProductosProceso.valunitario * pProductosProceso.cantidad;
                    pProductosProceso = DAProductosProceso.ModificarProductosProceso(pProductosProceso, pUsuario);

                    ts.Complete();
                }

                return pProductosProceso;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosProcesoBusiness", "ModificarProductosProceso", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un ProductosProceso
        /// </summary>
        /// <param name="pId">Identificador de ProductosProceso</param>
        public void EliminarProductosProceso(Int64 pId, Usuario pUsuario, Int64 Cod_persona, Int64 Cod_InfFin)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAProductosProceso.EliminarProductosProceso(pId, pUsuario, Cod_persona, Cod_InfFin);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosProcesoBusiness", "EliminarProductosProceso", ex);
            }
        }

        /// <summary>
        /// Obtiene un ProductosProceso
        /// </summary>
        /// <param name="pId">Identificador de ProductosProceso</param>
        /// <returns>Entidad ProductosProceso</returns>
        public ProductosProceso ConsultarProductosProceso(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAProductosProceso.ConsultarProductosProceso(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosProcesoBusiness", "ConsultarProductosProceso", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pProductosProceso">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ProductosProceso obtenidos</returns>
        public List<ProductosProceso> ListarProductosProceso(ProductosProceso pProductosProceso, Usuario pUsuario)
        {
            try
            {
                return DAProductosProceso.ListarProductosProceso(pProductosProceso, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosProcesoBusiness", "ListarProductosProceso", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pProductosProceso">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ProductosProceso obtenidos</returns>
        public List<ProductosProceso> ListarProductosProcesoRepo(ProductosProceso pProductosProceso, Usuario pUsuario)
        {
            try
            {
                return DAProductosProceso.ListarProductosProcesoRepo(pProductosProceso, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosProcesoBusiness", "ListarProductosProcesoRepo", ex);
                return null;
            }
        }

    }
}