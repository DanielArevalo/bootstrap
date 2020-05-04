using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;
using System.ServiceModel;
using System.Data;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicio para PlanesSeguros
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class PlanesSegurosAmparosService
    {
        private PlanesSegurosAmparosBusiness BOPlanesSegurosAmparos;
        private ExcepcionBusiness BOExcepcion;

        public string CodigoPrograma { get { return "100201"; } }


        public int tipo_plan;
        public int consecutivo;
        /// <summary>
        /// Constructor del servicio para PlanesSegurosAmparos
        /// </summary>
        public PlanesSegurosAmparosService()
        {
            BOPlanesSegurosAmparos = new PlanesSegurosAmparosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

     
        /// <summary>
        /// Obtiene la lista de PlanesSegurosAmparos dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PlanesSegurosAmparos obtenidos</returns>
        public List<PlanesSegurosAmparos> ListarPlanesSegurosAmparos(PlanesSegurosAmparos pPlanesSegurosAmparos, Usuario pUsuario)
        {
            try
            {
                return BOPlanesSegurosAmparos.ListarPlanesSegurosAmparos(pPlanesSegurosAmparos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesSegurosAmparosService", "ListarPlanesSegurosAmparos", ex);
                return null;
            }
        }


     
        /// <summary>
        /// Modifica un PlanesSegurosAmparos
        /// </summary>
        /// <param name="pEntity">Entidad PlanesSegurosAmparos</param>
        /// <returns>Entidad modificada</returns>
        public PlanesSegurosAmparos ModificarPlanesSegurosAmparos(PlanesSegurosAmparos pPlanesSegurosAmparos, Usuario pUsuario)
        {
            try
            {
                return BOPlanesSegurosAmparos.ModificarPlanesSegurosAmparos(pPlanesSegurosAmparos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesSegurosAmparosService", "ModificarPlanesSegurosAmparos", ex);
                return null;
            }

        }

        /// <summary>
        /// Crea un PlanesSegurosAmparos
        /// </summary>
        /// <param name="pEntity">Entidad PlanesSegurosAmparos</param>
        /// <returns>Entidad creada</returns>
        public PlanesSegurosAmparos InsertarPlanesSegurosAmparos(PlanesSegurosAmparos pPlanesSegurosAmparos, Usuario pUsuario)
        {
            try
            {
                return BOPlanesSegurosAmparos.InsertarPlanesSegurosAmparos(pPlanesSegurosAmparos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesSegurosAmparosService", "InsertarPlanesSegurosAmparos", ex);
                return null;
            }

        }

        /// <summary>
        /// Elimina un PlanesSegurosAmparos
        /// </summary>
        /// <param name="pId">identificador del PlanesSegurosAmparos</param>
        public void EliminarPlanesSegurosAmparos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOPlanesSegurosAmparos.EliminarPlanesSegurosAmparos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesSegurosAmparosService", "EliminarPlanesSegurosAmparos", ex);
            }
        }

        /// <summary>
        /// Obtiene un PlanesSegurosAmparos
        /// </summary>
        /// <param name="pId">identificador del PlanesSegurosAmparos</param>
        /// <returns>Caja PlanesSegurosAmparos</returns>
        public PlanesSegurosAmparos ConsultarPlanesSeguros(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOPlanesSegurosAmparos.ConsultarPlanesSegurosAmparos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesSegurosAmparosService", "ConsultarPlanesSegurosAmparos", ex);
                return null;
            }
        }
    }
}
