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
    public class VehiculosService
    {
        private VehiculosBusiness BOVehiculos;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Vehiculos
        /// </summary>
        public VehiculosService()
        {
            BOVehiculos = new VehiculosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100102"; } } //100115

        /// <summary>
        /// Servicio para crear Vehiculos
        /// </summary>
        /// <param name="pEntity">Entidad Vehiculos</param>
        /// <returns>Entidad Vehiculos creada</returns>
        public Vehiculos CrearVehiculos(Vehiculos pVehiculos, Usuario pUsuario)
        {
            try
            {
                return BOVehiculos.CrearVehiculos(pVehiculos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("VehiculosService", "CrearVehiculos", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Vehiculos
        /// </summary>
        /// <param name="pVehiculos">Entidad Vehiculos</param>
        /// <returns>Entidad Vehiculos modificada</returns>
        public Vehiculos ModificarVehiculos(Vehiculos pVehiculos, Usuario pUsuario)
        {
            try
            {
                return BOVehiculos.ModificarVehiculos(pVehiculos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("VehiculosService", "ModificarVehiculos", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Vehiculos
        /// </summary>
        /// <param name="pId">identificador de Vehiculos</param>
        public void EliminarVehiculos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOVehiculos.EliminarVehiculos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarVehiculos", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Vehiculos
        /// </summary>
        /// <param name="pId">identificador de Vehiculos</param>
        /// <returns>Entidad Vehiculos</returns>
        public Vehiculos ConsultarVehiculos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOVehiculos.ConsultarVehiculos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("VehiculosService", "ConsultarVehiculos", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Vehiculoss a partir de unos filtros
        /// </summary>
        /// <param name="pVehiculos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Vehiculos obtenidos</returns>
        public List<Vehiculos> ListarVehiculos(Vehiculos pVehiculos, Usuario pUsuario)
        {
            try
            {
                return BOVehiculos.ListarVehiculos(pVehiculos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("VehiculosService", "ListarVehiculos", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Vehiculoss a partir de unos filtros
        /// </summary>
        /// <param name="pVehiculos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Vehiculos obtenidos</returns>
        public List<Vehiculos> ListarVehiculosRepo(Vehiculos pVehiculos, Usuario pUsuario)
        {
            try
            {
                return BOVehiculos.ListarVehiculosRepo(pVehiculos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("VehiculosService", "ListarVehiculosRepo", ex);
                return null;
            }
        }
    }
}