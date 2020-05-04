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
    public class PlanesSegurosService
    {
        private PlanesSegurosBusiness BOPlanesSeguros;
        private ExcepcionBusiness BOExcepcion;

        public int CodigoPlan;

        public string CodigoPrograma { get { return "100201"; } }


        /// <summary>
        /// Constructor del servicio para PlanesSeguros
        /// </summary>
        public PlanesSegurosService()
        {
            BOPlanesSeguros = new PlanesSegurosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

     


        /// <summary>
        /// Obtiene la lista de PlanesSeguros dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PlanesSeguros obtenidos</returns>
        public List<PlanesSeguros> ListarPlanesSeguros(PlanesSeguros pPlanesSeguros, Usuario pUsuario)
        {
            try
            {
                return BOPlanesSeguros.ListarPlanesSeguros(pPlanesSeguros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesSegurosService", "ListarPlanesSeguros", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un PlanesSeguros
        /// </summary>
        /// <param name="pEntity">Entidad PlanesSeguros</param>
        /// <returns>Entidad modificada</returns>
        public PlanesSeguros ModificarPlanesSeguros(PlanesSeguros pPlanesSeguros, Usuario pUsuario)
        {
            try
            {
                return BOPlanesSeguros.ModificarPlanesSeguros(pPlanesSeguros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesSegurosService", "ModificarPlanesSeguros", ex);
                return null;
            }

        }
        /// <summary>
        /// Crea un PlanesSeguros
        /// </summary>
        /// <param name="pEntity">Entidad PlanesSeguros</param>
        /// <returns>Entidad Creada</returns>
        public PlanesSeguros CrearPlanesSeguros(PlanesSeguros pPlanesSeguros, Usuario pUsuario)
        {
            try
            {
                return BOPlanesSeguros.InsertarPlanesSeguros(pPlanesSeguros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesSegurosService", "CrearPlanesSeguros", ex);
                return null;
            }

        }

        /// <summary>
        /// Elimina un PlanesSeguros
        /// </summary>
        /// <param name="pId">identificador del PlanesSeguros</param>
        public void EliminarPlanesSeguros(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOPlanesSeguros.EliminarPlanesSeguros(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesSegurosService", "EliminarPlanesSeguros", ex);
            }
        }

        /// <summary>
        /// Obtiene un PlanesSeguros
        /// </summary>
        /// <param name="pId">identificador del PlanesSeguros</param>
        /// <returns>Caja PlanesSeguros</returns>
        public PlanesSeguros ConsultarPlanesSeguros(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOPlanesSeguros.ConsultarPlanesSeguros(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesSegurosService", "ConsultarPlanesSeguros", ex);
                return null;
            }
        }
    }
}
