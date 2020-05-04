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
    public class VentasSemanalesService
    {
        private VentasSemanalesBusiness BOVentasSemanales;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para VentasSemanales
        /// </summary>
        public VentasSemanalesService()
        {
            BOVentasSemanales = new VentasSemanalesBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100102"; } }  //100127

        /// <summary>
        /// Servicio para crear VentasSemanales
        /// </summary>
        /// <param name="pEntity">Entidad VentasSemanales</param>
        /// <returns>Entidad VentasSemanales creada</returns>
        public VentasSemanales CrearVentasSemanales(VentasSemanales pVentasSemanales, Usuario pUsuario)
        {
            try
            {
                return BOVentasSemanales.CrearVentasSemanales(pVentasSemanales, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("VentasSemanalesService", "CrearVentasSemanales", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar VentasSemanales
        /// </summary>
        /// <param name="pVentasSemanales">Entidad VentasSemanales</param>
        /// <returns>Entidad VentasSemanales modificada</returns>
        public VentasSemanales ModificarVentasSemanales(VentasSemanales pVentasSemanales, Usuario pUsuario)
        {
            try
            {
                return BOVentasSemanales.ModificarVentasSemanales(pVentasSemanales, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("VentasSemanalesService", "ModificarVentasSemanales", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar VentasSemanales
        /// </summary>
        /// <param name="pId">identificador de VentasSemanales</param>
        public void EliminarVentasSemanales(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOVentasSemanales.EliminarVentasSemanales(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarVentasSemanales", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener VentasSemanales
        /// </summary>
        /// <param name="pId">identificador de VentasSemanales</param>
        /// <returns>Entidad VentasSemanales</returns>
        public VentasSemanales ConsultarVentasSemanales(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOVentasSemanales.ConsultarVentasSemanales(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("VentasSemanalesService", "ConsultarVentasSemanales", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener calculos sobre las Ventas Semanales
        /// </summary>
        /// <param name="pVentasSemanales">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de VentasSemanales obtenidos</returns>
        public VentasSemanales CalculosTotalesSemanales(List<VentasSemanales> lstConsulta)
        {
            try
            {
                return BOVentasSemanales.CalculosTotalesSemanales(lstConsulta);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("VentasSemanalesService", "ListarVentasSemanales", ex);
                return null;
            }
        }




        /// <summary>
        /// Obtiene  listas desplegables
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de elementos obtenidos</returns>
        public List<VentasSemanales> ListasDesplegables(String ListaSolicitada, Usuario pUsuario)
        {
            try
            {
                return BOVentasSemanales.ListasDesplegables(ListaSolicitada, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("VentasSemanalesService", "ListarVentasSemanales", ex);
                return null;
            }
        }





        /// <summary>
        /// Servicio para obtener lista de VentasSemanaless a partir de unos filtros
        /// </summary>
        /// <param name="pVentasSemanales">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de VentasSemanales obtenidos</returns>
        public List<VentasSemanales> ListarVentasSemanales(VentasSemanales pVentasSemanales, Usuario pUsuario)
        {
            try
            {

                return BOVentasSemanales.ListarVentasSemanales(pVentasSemanales, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("VentasSemanalesService", "ListarVentasSemanales", ex);
                return null;
            }
        }




    }
}