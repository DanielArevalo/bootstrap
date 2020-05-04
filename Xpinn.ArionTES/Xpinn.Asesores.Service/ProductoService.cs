using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ProductoService
    {

        private ProductoBusiness BOProductos;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Productos
        /// </summary>
        public ProductoService()
        {
            BOProductos = new ProductoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110119"; } }


        public List<Producto> ListarProductosPorEstados(Producto pEntityProducto, Usuario pUsuario, String FiltroEstados)
        {
            try
            {
                return BOProductos.ListarProductosPorEstados(pEntityProducto, pUsuario, FiltroEstados);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductoService", "ListarProductosPorEstados", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para obtener lista de Productoss a partir de unos filtros
        /// </summary>
        /// <param name="pProductos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Productos obtenidos</returns>
        public List<Producto> ListarProductos(Producto pEntityProducto, Usuario pUsuario)
        {
            try
            {
                return BOProductos.ListarProductos(pEntityProducto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosService", "ListarProductos", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Productoss a partir de unos filtros
        /// </summary>
        /// <param name="pProductos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Productos obtenidos</returns>
        public List<Producto> ListarProductosTodos(Producto pEntityProducto, Usuario pUsuario)
        {
            try
            {
                return BOProductos.ListarProductosTodos(pEntityProducto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosService", "ListarProductosTodos", ex);
                return null;
            }
        }

        public List<ProductoResumen> ListarProductosResumen(string IdPersona, Usuario pUsuario)
        {
            try
            {
                return BOProductos.ListarProductosResumen(IdPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosService", "ListarProductosResumen", ex);
                return null;
            }
        }

        public List<ProductoResumen> ListarCreditosClubAhorrador(string IdPersona, Boolean pResult, Usuario pUsuario)
        {
            try
            {
                return BOProductos.ListarCreditosClubAhorrador(IdPersona, pResult, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosService", "ListarCreditosClubAhorrador", ex);
                return null;
            }
        }

        public List<ProductoResumen> ListarProductosAporte(string IdPersona, Usuario pUsuario)
        {
            try
            {
                return BOProductos.ListarProductosAporte(IdPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosService", "ListarProductosAporte", ex);
                return null;
            }
        }

        public List<Producto> ListarProductoscredito(Int64 credito, decimal saldo, Usuario pUsuario)
        {
            try
            {
                return BOProductos.ListarProductoscredito(credito, saldo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosService", "ListarProductos", ex);
                return null;
            }
        }       

        public List<ProductoResumen> ListarProductosCreditoResumen(int credito, Usuario pUsuario)
        {
            try
            {
                return BOProductos.ListarProductosCreditoResumen(credito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosService", "ListarProductosCreditoResumen", ex);
                return null;
            }
        }

        public List<MovimientoResumen> ListarMovimientoResumen(int credito, int numero, Usuario pUsuario)
        {
            try
            {
                return BOProductos.ListarMovimientoResumen(credito, numero, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosService", "ListarMovimientoResumen", ex);
                return null;
            }
        }

        public List<MovimientoResumen> ListarMovimientoPersonaResumen(string pIdentificacion, int pNumero, Usuario pUsuario)
        {
            try
            {
                return BOProductos.ListarMovimientoPersonaResumen(pIdentificacion, pNumero, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosService", "ListarMovimientoPersonaResumen", ex);
                return null;
            }
        }

        public Producto ConsultarCreditosTerminados(Int64 pIdEntityProducto, Usuario pUsuario)
        {
            try
            {
                return BOProductos.ConsultarCreditosTerminados(pIdEntityProducto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosService", "ConsultarCreditosTerminados", ex);
                return null;
            }
        }



        public List<ProductoPersonaAPP> ListarProductosXPersonaAPP(Int64 codPersona, Usuario pUsuario)
        {
            try
            {
                return BOProductos.ListarProductosXPersonaAPP(codPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosService", "ListarProductosXPersonaAPP", ex);
                return null;
            }
        }

    }
}