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
    /// Objeto de negocio para Vehiculos
    /// </summary>
    public class VehiculosBusiness : GlobalData
    {
        private VehiculosData DAVehiculos;

        /// <summary>
        /// Constructor del objeto de negocio para Vehiculos
        /// </summary>
        public VehiculosBusiness()
        {
            DAVehiculos = new VehiculosData();
        }

        /// <summary>
        /// Crea un Vehiculos
        /// </summary>
        /// <param name="pVehiculos">Entidad Vehiculos</param>
        /// <returns>Entidad Vehiculos creada</returns>
        public Vehiculos CrearVehiculos(Vehiculos pVehiculos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pVehiculos = DAVehiculos.CrearVehiculos(pVehiculos, pUsuario);

                    ts.Complete();
                }

                return pVehiculos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("VehiculosBusiness", "CrearVehiculos", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Vehiculos
        /// </summary>
        /// <param name="pVehiculos">Entidad Vehiculos</param>
        /// <returns>Entidad Vehiculos modificada</returns>
        public Vehiculos ModificarVehiculos(Vehiculos pVehiculos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pVehiculos = DAVehiculos.ModificarVehiculos(pVehiculos, pUsuario);

                    ts.Complete();
                }

                return pVehiculos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("VehiculosBusiness", "ModificarVehiculos", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Vehiculos
        /// </summary>
        /// <param name="pId">Identificador de Vehiculos</param>
        public void EliminarVehiculos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAVehiculos.EliminarVehiculos(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("VehiculosBusiness", "EliminarVehiculos", ex);
            }
        }

        /// <summary>
        /// Obtiene un Vehiculos
        /// </summary>
        /// <param name="pId">Identificador de Vehiculos</param>
        /// <returns>Entidad Vehiculos</returns>
        public Vehiculos ConsultarVehiculos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAVehiculos.ConsultarVehiculos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("VehiculosBusiness", "ConsultarVehiculos", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pVehiculos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Vehiculos obtenidos</returns>
        public List<Vehiculos> ListarVehiculos(Vehiculos pVehiculos, Usuario pUsuario)
        {
            try
            {
                return DAVehiculos.ListarVehiculos(pVehiculos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("VehiculosBusiness", "ListarVehiculos", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pVehiculos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Vehiculos obtenidos</returns>
        public List<Vehiculos> ListarVehiculosRepo(Vehiculos pVehiculos, Usuario pUsuario)
        {
            try
            {
                return DAVehiculos.ListarVehiculosRepo(pVehiculos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("VehiculosBusiness", "ListarVehiculosRepo", ex);
                return null;
            }
        }


    }
}