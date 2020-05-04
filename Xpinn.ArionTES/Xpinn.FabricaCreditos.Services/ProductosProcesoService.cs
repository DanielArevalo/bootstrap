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
    public class ProductosProcesoService
    {
        private ProductosProcesoBusiness BOProductosProceso;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para ProductosProceso
        /// </summary>
        public ProductosProcesoService()
        {
            BOProductosProceso = new ProductosProcesoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100102"; } }   //100133

        /// <summary>
        /// Servicio para crear ProductosProceso
        /// </summary>
        /// <param name="pEntity">Entidad ProductosProceso</param>
        /// <returns>Entidad ProductosProceso creada</returns>
        public ProductosProceso CrearProductosProceso(ProductosProceso pProductosProceso, Usuario pUsuario)
        {
            try
            {
                return BOProductosProceso.CrearProductosProceso(pProductosProceso, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosProcesoService", "CrearProductosProceso", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar ProductosProceso
        /// </summary>
        /// <param name="pProductosProceso">Entidad ProductosProceso</param>
        /// <returns>Entidad ProductosProceso modificada</returns>
        public ProductosProceso ModificarProductosProceso(ProductosProceso pProductosProceso, Usuario pUsuario)
        {
            try
            {
                return BOProductosProceso.ModificarProductosProceso(pProductosProceso, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosProcesoService", "ModificarProductosProceso", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar ProductosProceso
        /// </summary>
        /// <param name="pId">identificador de ProductosProceso</param>
        public void EliminarProductosProceso(Int64 pId, Usuario pUsuario, Int64 Cod_persona, Int64 Cod_InfFin)
        {
            try
            {
                BOProductosProceso.EliminarProductosProceso(pId, pUsuario, Cod_persona, Cod_InfFin);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarProductosProceso", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener ProductosProceso
        /// </summary>
        /// <param name="pId">identificador de ProductosProceso</param>
        /// <returns>Entidad ProductosProceso</returns>
        public ProductosProceso ConsultarProductosProceso(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOProductosProceso.ConsultarProductosProceso(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosProcesoService", "ConsultarProductosProceso", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de ProductosProcesos a partir de unos filtros
        /// </summary>
        /// <param name="pProductosProceso">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ProductosProceso obtenidos</returns>
        public List<ProductosProceso> ListarProductosProceso(ProductosProceso pProductosProceso, Usuario pUsuario)
        {
            try
            {
                return BOProductosProceso.ListarProductosProceso(pProductosProceso, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosProcesoService", "ListarProductosProceso", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de ProductosProcesos a partir de unos filtros
        /// </summary>
        /// <param name="pProductosProceso">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ProductosProceso obtenidos</returns>
        public List<ProductosProceso> ListarProductosProcesoRepo(ProductosProceso pProductosProceso, Usuario pUsuario)
        {
            try
            {
                return BOProductosProceso.ListarProductosProcesoRepo(pProductosProceso, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProductosProcesoService", "ListarProductosProcesoRepo", ex);
                return null;
            }
        }
    }
}