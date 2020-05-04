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
    public class EstacionalidadMensualService
    {
        private EstacionalidadMensualBusiness BOEstacionalidadMensual;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para EstacionalidadMensual
        /// </summary>
        public EstacionalidadMensualService()
        {
            BOEstacionalidadMensual = new EstacionalidadMensualBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100102"; } }  //100126

        /// <summary>
        /// Servicio para crear EstacionalidadMensual
        /// </summary>
        /// <param name="pEntity">Entidad EstacionalidadMensual</param>
        /// <returns>Entidad EstacionalidadMensual creada</returns>
        public EstacionalidadMensual CrearEstacionalidadMensual(EstacionalidadMensual pEstacionalidadMensual, Usuario pUsuario)
        {
            try
            {
                return BOEstacionalidadMensual.CrearEstacionalidadMensual(pEstacionalidadMensual, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstacionalidadMensualService", "CrearEstacionalidadMensual", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar EstacionalidadMensual
        /// </summary>
        /// <param name="pEstacionalidadMensual">Entidad EstacionalidadMensual</param>
        /// <returns>Entidad EstacionalidadMensual modificada</returns>
        public EstacionalidadMensual ModificarEstacionalidadMensual(EstacionalidadMensual pEstacionalidadMensual, Usuario pUsuario)
        {
            try
            {
                return BOEstacionalidadMensual.ModificarEstacionalidadMensual(pEstacionalidadMensual, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstacionalidadMensualService", "ModificarEstacionalidadMensual", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar EstacionalidadMensual
        /// </summary>
        /// <param name="pId">identificador de EstacionalidadMensual</param>
        public void EliminarEstacionalidadMensual(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOEstacionalidadMensual.EliminarEstacionalidadMensual(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarEstacionalidadMensual", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener EstacionalidadMensual
        /// </summary>
        /// <param name="pId">identificador de EstacionalidadMensual</param>
        /// <returns>Entidad EstacionalidadMensual</returns>
        public EstacionalidadMensual ConsultarEstacionalidadMensual(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOEstacionalidadMensual.ConsultarEstacionalidadMensual(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstacionalidadMensualService", "ConsultarEstacionalidadMensual", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de EstacionalidadMensuals a partir de unos filtros
        /// </summary>
        /// <param name="pEstacionalidadMensual">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de EstacionalidadMensual obtenidos</returns>
        public List<EstacionalidadMensual> ListarEstacionalidadMensual(EstacionalidadMensual pEstacionalidadMensual, Usuario pUsuario)
        {
            try
            {
                return BOEstacionalidadMensual.ListarEstacionalidadMensual(pEstacionalidadMensual, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstacionalidadMensualService", "ListarEstacionalidadMensual", ex);
                return null;
            }
        }



        /// <summary>
        /// Obtiene  listas desplegables
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de elementos obtenidos</returns>
        public List<EstacionalidadMensual> ListasDesplegables(String ListaSolicitada, Usuario pUsuario)
        {
            try
            {
                return BOEstacionalidadMensual.ListasDesplegables(ListaSolicitada, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstacionalidadMensualService", "ListarEstacionalidadMensual", ex);
                return null;
            }
        }




        /// <summary>
        /// Servicio para obtener calculos sobre las Ventas Semanales
        /// </summary>
        /// <param name="pVentasSemanales">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de VentasSemanales obtenidos</returns>
        public EstacionalidadMensual CalculosEstacionalidadMensual(List<EstacionalidadMensual> lstConsulta)
        {
            try
            {
                return BOEstacionalidadMensual.CalculosEstacionalidadMensual(lstConsulta);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("VentasSemanalesService", "ListarVentasSemanales", ex);
                return null;
            }
        }




    }
}