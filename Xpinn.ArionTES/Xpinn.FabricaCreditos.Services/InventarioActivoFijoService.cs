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
    public class InventarioActivoFijoService
    {
        private InventarioActivoFijoBusiness BOInventarioActivoFijo;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para InventarioActivoFijo
        /// </summary>
        public InventarioActivoFijoService()
        {
            BOInventarioActivoFijo = new InventarioActivoFijoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100102"; } }   //100130

        /// <summary>
        /// Servicio para crear InventarioActivoFijo
        /// </summary>
        /// <param name="pEntity">Entidad InventarioActivoFijo</param>
        /// <returns>Entidad InventarioActivoFijo creada</returns>
        public InventarioActivoFijo CrearInventarioActivoFijo(InventarioActivoFijo pInventarioActivoFijo, Usuario pUsuario)
        {
            try
            {
                return BOInventarioActivoFijo.CrearInventarioActivoFijo(pInventarioActivoFijo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventarioActivoFijoService", "CrearInventarioActivoFijo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar InventarioActivoFijo
        /// </summary>
        /// <param name="pInventarioActivoFijo">Entidad InventarioActivoFijo</param>
        /// <returns>Entidad InventarioActivoFijo modificada</returns>
        public InventarioActivoFijo ModificarInventarioActivoFijo(InventarioActivoFijo pInventarioActivoFijo, Usuario pUsuario)
        {
            try
            {
                return BOInventarioActivoFijo.ModificarInventarioActivoFijo(pInventarioActivoFijo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventarioActivoFijoService", "ModificarInventarioActivoFijo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar InventarioActivoFijo
        /// </summary>
        /// <param name="pId">identificador de InventarioActivoFijo</param>
        public void EliminarInventarioActivoFijo(Int64 pId, Usuario pUsuario, Int64 Cod_persona, Int64 Cod_InfFin)
        {
            try
            {
                BOInventarioActivoFijo.EliminarInventarioActivoFijo(pId, pUsuario, Cod_persona, Cod_InfFin);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarInventarioActivoFijo", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener InventarioActivoFijo
        /// </summary>
        /// <param name="pId">identificador de InventarioActivoFijo</param>
        /// <returns>Entidad InventarioActivoFijo</returns>
        public InventarioActivoFijo ConsultarInventarioActivoFijo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOInventarioActivoFijo.ConsultarInventarioActivoFijo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventarioActivoFijoService", "ConsultarInventarioActivoFijo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de InventarioActivoFijos a partir de unos filtros
        /// </summary>
        /// <param name="pInventarioActivoFijo">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InventarioActivoFijo obtenidos</returns>
        public List<InventarioActivoFijo> ListarInventarioActivoFijo(InventarioActivoFijo pInventarioActivoFijo, Usuario pUsuario)
        {
            try
            {
                return BOInventarioActivoFijo.ListarInventarioActivoFijo(pInventarioActivoFijo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventarioActivoFijoService", "ListarInventarioActivoFijo", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para obtener lista de InventarioActivoFijos a partir de unos filtros
        /// </summary>
        /// <param name="pInventarioActivoFijo">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InventarioActivoFijo obtenidos</returns>
        public List<InventarioActivoFijo> ListarInventarioActivoFijoRepo(InventarioActivoFijo pInventarioActivoFijo, Usuario pUsuario)
        {
            try
            {
                return BOInventarioActivoFijo.ListarInventarioActivoFijoRepo(pInventarioActivoFijo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventarioActivoFijoService", "ListarInventarioActivoFijoRepo", ex);
                return null;
            }
        }
    }
}