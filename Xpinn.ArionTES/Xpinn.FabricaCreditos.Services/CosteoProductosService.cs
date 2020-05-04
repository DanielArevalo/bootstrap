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
    public class CosteoProductosService
    {
        private CosteoProductosBusiness BOCosteoProductos;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para CosteoProductos
        /// </summary>
        public CosteoProductosService()
        {
            BOCosteoProductos = new CosteoProductosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100102"; } }  //100124

        /// <summary>
        /// Servicio para crear CosteoProductos
        /// </summary>
        /// <param name="pEntity">Entidad CosteoProductos</param>
        /// <returns>Entidad CosteoProductos creada</returns>
        public CosteoProductos CrearCosteoProductos(CosteoProductos pCosteoProductos, Usuario pUsuario)
        {
            try
            {
                return BOCosteoProductos.CrearCosteoProductos(pCosteoProductos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CosteoProductosService", "CrearCosteoProductos", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar CosteoProductos
        /// </summary>
        /// <param name="pCosteoProductos">Entidad CosteoProductos</param>
        /// <returns>Entidad CosteoProductos modificada</returns>
        public CosteoProductos ModificarCosteoProductos(CosteoProductos pCosteoProductos, Usuario pUsuario)
        {
            try
            {
                return BOCosteoProductos.ModificarCosteoProductos(pCosteoProductos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CosteoProductosService", "ModificarCosteoProductos", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar CosteoProductos
        /// </summary>
        /// <param name="pId">identificador de CosteoProductos</param>
        public void EliminarCosteoProductos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOCosteoProductos.EliminarCosteoProductos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarCosteoProductos", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener CosteoProductos
        /// </summary>
        /// <param name="pId">identificador de CosteoProductos</param>
        /// <returns>Entidad CosteoProductos</returns>
        public CosteoProductos ConsultarCosteoProductos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOCosteoProductos.ConsultarCosteoProductos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CosteoProductosService", "ConsultarCosteoProductos", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de CosteoProductoss a partir de unos filtros
        /// </summary>
        /// <param name="pCosteoProductos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de CosteoProductos obtenidos</returns>
        public List<CosteoProductos> ListarCosteoProductos(CosteoProductos pCosteoProductos, Usuario pUsuario)
        {
            try
            {
                return BOCosteoProductos.ListarCosteoProductos(pCosteoProductos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CosteoProductosService", "ListarCosteoProductos", ex);
                return null;
            }
        }
    }
}