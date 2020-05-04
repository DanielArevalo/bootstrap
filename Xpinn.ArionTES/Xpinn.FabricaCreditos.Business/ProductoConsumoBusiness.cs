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
    /// Objeto de negocio para la factura
    /// </summary>
    public class ProductoConsumoBusiness : GlobalData
    {
        private ProductoConsumoData DAProducto;

        /// <summary>
        /// Constructor del objeto de negocio para Programa
        /// </summary>
        public ProductoConsumoBusiness()
        {
            DAProducto = new ProductoConsumoData();
        }

        /// <summary>
        /// Obtiene una Central
        /// </summary>
        /// <returns>Programa consultado</returns>
        public List<ProductoConsumo> ListarProductoConsumo(string filtro, Usuario pUsuario)
        {
            try
            {
                return DAProducto.ListarProductoConsumo(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductoConsumoBusiness", "ListarProductoConsumo", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene una Central
        /// </summary>
        /// <returns>Programa consultado</returns>
        public List<CategoriaProducto> ListarCatProductoConsumo(string filtro, Usuario pUsuario)
        {
            try
            {
                return DAProducto.ListarCategoriaProductoConsumo(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductoConsumoBusiness", "ListarCatProductoConsumo", ex);
                return null;
            }
        }


        public AutorizaConsulta consultarAutorizaPendiente(Int32 cod_persona, Usuario pUsuario)
        {
            try
            {
                return DAProducto.consultarAutorizaPendiente(cod_persona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductoConsumoBusiness", "consultarAutorizaPendiente", ex);
                return null;
            }
        }

        public AutorizaConsulta verificarAutorizacion(Int32 cod_persona, Usuario pUsuario)
        {
            try
            {
                return DAProducto.verificarAutorizacion(cod_persona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductoConsumoBusiness", "consultarAutorizaPendiente", ex);
                return null;
            }
        }

        public bool CrearAutorizacion(AutorizaConsulta pentidad, Usuario pUsuario)
        {
            try
            {
                return DAProducto.CrearAutorizacion(pentidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductoConsumoBusiness", "CrearAutorizacion", ex);
                return false;
            }
        }

        public bool ModificarAutorizacion(AutorizaConsulta pentidad, Usuario pUsuario)
        {
            try
            {
                return DAProducto.ModificarAutorizacion(pentidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductoConsumoBusiness", "ModificarAutorizacion", ex);
                return false;
            }
        }

    }
}
