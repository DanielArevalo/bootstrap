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
    /// Objeto de negocio para ProductosTerminados
    /// </summary>
    public class ProductosTerminadosBusiness : GlobalData
    {
        private ProductosTerminadosData DAProductosTerminados;

        /// <summary>
        /// Constructor del objeto de negocio para ProductosTerminados
        /// </summary>
        public ProductosTerminadosBusiness()
        {
            DAProductosTerminados = new ProductosTerminadosData();
        }

        /// <summary>
        /// Crea un ProductosTerminados
        /// </summary>
        /// <param name="pProductosTerminados">Entidad ProductosTerminados</param>
        /// <returns>Entidad ProductosTerminados creada</returns>
        public ProductosTerminados CrearProductosTerminados(ProductosTerminados pProductosTerminados, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pProductosTerminados.vrtotal = pProductosTerminados.vrunitario * pProductosTerminados.cantidad;
                    pProductosTerminados = DAProductosTerminados.CrearProductosTerminados(pProductosTerminados, pUsuario);

                    ts.Complete();
                }

                return pProductosTerminados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosTerminadosBusiness", "CrearProductosTerminados", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un ProductosTerminados
        /// </summary>
        /// <param name="pProductosTerminados">Entidad ProductosTerminados</param>
        /// <returns>Entidad ProductosTerminados modificada</returns>
        public ProductosTerminados ModificarProductosTerminados(ProductosTerminados pProductosTerminados, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pProductosTerminados.vrtotal = pProductosTerminados.vrunitario * pProductosTerminados.cantidad;
                    pProductosTerminados = DAProductosTerminados.ModificarProductosTerminados(pProductosTerminados, pUsuario);

                    ts.Complete();
                }

                return pProductosTerminados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosTerminadosBusiness", "ModificarProductosTerminados", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un ProductosTerminados
        /// </summary>
        /// <param name="pId">Identificador de ProductosTerminados</param>
        public void EliminarProductosTerminados(Int64 pId, Usuario pUsuario, Int64 Cod_persona, Int64 Cod_InfFin)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAProductosTerminados.EliminarProductosTerminados(pId, pUsuario, Cod_persona, Cod_InfFin);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosTerminadosBusiness", "EliminarProductosTerminados", ex);
            }
        }

        /// <summary>
        /// Obtiene un ProductosTerminados
        /// </summary>
        /// <param name="pId">Identificador de ProductosTerminados</param>
        /// <returns>Entidad ProductosTerminados</returns>
        public ProductosTerminados ConsultarProductosTerminados(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAProductosTerminados.ConsultarProductosTerminados(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosTerminadosBusiness", "ConsultarProductosTerminados", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pProductosTerminados">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ProductosTerminados obtenidos</returns>
        public List<ProductosTerminados> ListarProductosTerminados(ProductosTerminados pProductosTerminados, Usuario pUsuario)
        {
            try
            {
                return DAProductosTerminados.ListarProductosTerminados(pProductosTerminados, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosTerminadosBusiness", "ListarProductosTerminados", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pProductosTerminados">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ProductosTerminados obtenidos</returns>
        public List<ProductosTerminados> ListarProductosTerminadosRepo(ProductosTerminados pProductosTerminados, Usuario pUsuario)
        {
            try
            {
                return DAProductosTerminados.ListarProductosTerminadosRepo(pProductosTerminados, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosTerminadosBusiness", "ListarProductosTerminadosRepo", ex);
                return null;
            }
        }

    }
}