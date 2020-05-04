using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ProductosTerminadosService
    {
        private ProductosTerminadosBusiness BOProductosTerminados;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para ProductosTerminados
        /// </summary>
        public ProductosTerminadosService()
        {
            BOProductosTerminados = new ProductosTerminadosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100102"; } }  //100134

        /// <summary>
        /// Servicio para crear ProductosTerminados
        /// </summary>
        /// <param name="pEntity">Entidad ProductosTerminados</param>
        /// <returns>Entidad ProductosTerminados creada</returns>
        public ProductosTerminados CrearProductosTerminados(ProductosTerminados pProductosTerminados, Usuario pUsuario)
        {
            try
            {
                return BOProductosTerminados.CrearProductosTerminados(pProductosTerminados, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosTerminadosService", "CrearProductosTerminados", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar ProductosTerminados
        /// </summary>
        /// <param name="pProductosTerminados">Entidad ProductosTerminados</param>
        /// <returns>Entidad ProductosTerminados modificada</returns>
        public ProductosTerminados ModificarProductosTerminados(ProductosTerminados pProductosTerminados, Usuario pUsuario)
        {
            try
            {
                return BOProductosTerminados.ModificarProductosTerminados(pProductosTerminados, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosTerminadosService", "ModificarProductosTerminados", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar ProductosTerminados
        /// </summary>
        /// <param name="pId">identificador de ProductosTerminados</param>
        public void EliminarProductosTerminados(Int64 pId, Usuario pUsuario, Int64 Cod_persona, Int64 Cod_InfFin)
        {
            try
            {
                BOProductosTerminados.EliminarProductosTerminados(pId, pUsuario, Cod_persona, Cod_InfFin);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarProductosTerminados", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener ProductosTerminados
        /// </summary>
        /// <param name="pId">identificador de ProductosTerminados</param>
        /// <returns>Entidad ProductosTerminados</returns>
        public ProductosTerminados ConsultarProductosTerminados(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOProductosTerminados.ConsultarProductosTerminados(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosTerminadosService", "ConsultarProductosTerminados", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de ProductosTerminadoss a partir de unos filtros
        /// </summary>
        /// <param name="pProductosTerminados">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ProductosTerminados obtenidos</returns>
        public List<ProductosTerminados> ListarProductosTerminados(ProductosTerminados pProductosTerminados, Usuario pUsuario)
        {
            try
            {
                return BOProductosTerminados.ListarProductosTerminados(pProductosTerminados, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosTerminadosService", "ListarProductosTerminados", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener lista de ProductosTerminadoss a partir de unos filtros
        /// </summary>
        /// <param name="pProductosTerminados">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ProductosTerminados obtenidos</returns>
        public List<ProductosTerminados> ListarProductosTerminadosRepo(ProductosTerminados pProductosTerminados, Usuario pUsuario)
        {
            try
            {
                return BOProductosTerminados.ListarProductosTerminadosRepo(pProductosTerminados, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosTerminadosService", "ListarProductosTerminadosRepo", ex);
                return null;
            }
        }
    }
}