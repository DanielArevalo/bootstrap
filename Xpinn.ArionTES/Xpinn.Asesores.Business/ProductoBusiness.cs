using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Asesores.Data;
using Xpinn.Util;
using System.IO;

namespace Xpinn.Asesores.Business
{
    /// <summary>
    /// Objeto de negocio para Productos
    /// </summary>
    public class ProductoBusiness : GlobalData
    {
        private ProductoData dataProducto;

        /// <summary>
        /// Constructor del objeto de negocio para Productos
        /// </summary>
        public ProductoBusiness()
        {
            dataProducto = new ProductoData();
        }


        public List<Producto> ListarProductosPorEstados(Producto pEntityProducto, Usuario pUsuario, String FiltroEstados)
        {
            try
            {
                return dataProducto.ListarProductosPorEstados(pEntityProducto, pUsuario, FiltroEstados);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosBusiness", "ListarProductosPorEstados", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pProductos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Productos obtenidos</returns>
        public List<Producto> ListarProductos(Producto pProductos, Usuario pUsuario)
        {
            try
            {
                return dataProducto.ListarProductos(pProductos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosBusiness", "ListarProductos", ex);
                return null;
            }
        }
        
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pProductos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Productos obtenidos</returns>
        public List<Producto> ListarProductosTodos(Producto pProductos, Usuario pUsuario)
        {
            try
            {
                return dataProducto.ListarProductosTodos(pProductos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosBusiness", "ListarProductosTodos", ex);
                return null;
            }
        }


        public List<ProductoResumen> ListarProductosResumen(string IdPersona, Usuario pUsuario)
        {
            try
            {
                return dataProducto.ListarProductosResumen(IdPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosBusiness", "ListarProductosResumen", ex);
                return null;
            }
        }

        public List<ProductoResumen> ListarCreditosClubAhorrador(string IdPersona, Boolean pResult, Usuario pUsuario)
        {
            try
            {
                return dataProducto.ListarCreditosClubAhorrador(IdPersona, pResult, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosBusiness", "ListarCreditosClubAhorrador", ex);
                return null;
            }
        }

        public List<ProductoResumen> ListarProductosAporte(string IdPersona, Usuario pUsuario)
        {
            try
            {
                return dataProducto.ListarProductosAporte(IdPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosBusiness", "ListarProductosAporte", ex);
                return null;
            }
        }

        public List<Producto> ListarProductoscredito(Int64 credito, decimal saldo, Usuario pUsuario)
        {
            try
            {
                return dataProducto.ListarProductoscredito(credito, saldo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosBusiness", "ListarProductos", ex);
                return null;
            }
        }

        public List<ProductoResumen> ListarProductosCreditoResumen(int credito, Usuario pUsuario)
        {
            try
            {
                return dataProducto.ListarProductosCreditoResumen(credito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosBusiness", "ListarProductosCreditoResumen", ex);
                return null;
            }
        }

        public List<Producto> ListarProductosMovGral(Producto pProductos, Usuario pUsuario)
        {
            try
            {
                return dataProducto.ListarProductos(pProductos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosBusiness", "ListarProductos", ex);
                return null;
            }
        }

        public List<MovimientoResumen> ListarMovimientoResumen(int credito, int numero, Usuario pUsuario)
        {
            try
            {
                return dataProducto.ListarMovimientoResumen(credito, numero, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosBusiness", "ListarMovimientoResumen", ex);
                return null;
            }
        }

        public List<MovimientoResumen> ListarMovimientoPersonaResumen(string pIdentificacion, int pNumero, Usuario pUsuario)
        {
            try
            {
                return dataProducto.ListarMovimientoPersonaResumen(pIdentificacion, pNumero, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosBusiness", "ListarMovimientoPersonaResumen", ex);
                return null;
            }
        }

        public Producto ConsultarCreditosTerminados(Int64 pIdEntityProducto, Usuario pUsuario)
        {
            try
            {
                return dataProducto.ConsultarCreditosTerminados(pIdEntityProducto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaData", "ConsultarCreditosTerminados", ex);
                return null;
            }

        }



        public List<ProductoPersonaAPP> ListarProductosXPersonaAPP(Int64 codPersona, Usuario pUsuario)
        {
            try
            {
                return dataProducto.ListarProductosXPersonaAPP(codPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductoBusiness", "ListarProductosXPersonaAPP", ex);
                return null;
            }
        }

    }
}