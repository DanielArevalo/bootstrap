using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Caja.Business;
using Xpinn.Caja.Entities;
using System.Web;
using System.Web.UI.WebControls;

namespace Xpinn.Caja.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>  
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ConsignacionService
    {
        private ConsignacionBusiness BOConsignacion;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Consignacion
        /// </summary>
        public ConsignacionService()
        {
            BOConsignacion = new ConsignacionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "120104"; } }

        public string CodigoProgramaTeso { get { return "40503"; } }

        /// <summary>
        /// Servicio para crear Consignacion
        /// </summary>
        /// <param name="pEntity">Entidad Consignacion</param>
        /// <returns>Entidad Consignacion creada</returns>
        public Consignacion CrearConsignacion(Consignacion pConsignacion, GridView gvConsignacion, Usuario pUsuario)
        {
            try
            {
                return BOConsignacion.CrearConsignacion(pConsignacion, gvConsignacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsignacionService", "CrearConsignacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para crear Consignacion
        /// </summary>
        /// <param name="pEntity">Entidad Consignacion</param>
        /// <returns>Entidad Consignacion creada</returns>
        public Consignacion CrearConsignacionCheque(Consignacion pConsignacion, GridView gvConsignacion, Usuario pUsuario)
        {
            try
            {
                return BOConsignacion.CrearConsignacionCheque(pConsignacion, gvConsignacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsignacionService", "CrearConsignacionCheque", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para crear Consignacion
        /// </summary>
        /// <param name="pEntity">Entidad Consignacion</param>
        /// <returns>Entidad Consignacion creada</returns>
        public Consignacion CrearConsignacionTesoreria(Consignacion pConsignacion, GridView gvConsignacion,ref Int64 pCOD_OPE, Usuario pUsuario)
        {
            try
            {
                return BOConsignacion.CrearConsignacionTesoreria(pConsignacion, gvConsignacion,ref pCOD_OPE, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsignacionService", "CrearConsignacionTesoreria", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para modificar Consignacion
        /// </summary>
        /// <param name="pConsignacion">Entidad Consignacion</param>
        /// <returns>Entidad Consignacion modificada</returns>
        public Consignacion ModificarConsignacion(Consignacion pConsignacion, Usuario pUsuario)
        {
            try
            {
                return BOConsignacion.ModificarConsignacion(pConsignacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsignacionService", "ModificarConsignacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Consignacion
        /// </summary>
        /// <param name="pId">identificador de Consignacion</param>
        public void EliminarConsignacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOConsignacion.EliminarConsignacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsignacionService", "EliminarConsignacion", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Consignacion
        /// </summary>
        /// <param name="pId">identificador de Consignacion</param>
        /// <returns>Entidad Consignacion</returns>
        public Consignacion ConsultarConsignacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOConsignacion.ConsultarConsignacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsignacionService", "ConsultarConsignacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Consignacions a partir de unos filtros
        /// </summary>
        /// <param name="pConsignacion">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Consignacion obtenidos</returns>
        public List<Consignacion> ListarConsignacion(Consignacion pConsignacion, Usuario pUsuario)
        {
            try
            {
                return BOConsignacion.ListarConsignacion(pConsignacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsignacionService", "ListarConsignacion", ex);
                return null;
            }
        }

        public List<Consignacion> ListarConsignacionCheque(Int64  pConsignacion, Usuario pUsuario)
        {
            try
            {
                return BOConsignacion.ListarConsignacionCheque(pConsignacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsignacionService", "ListarConsignacionCheque", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene un Traslado
        /// </summary>
        /// <param name="pUsuario">identificador del Usuario</param>
        /// <returns>Reitegro consultada</returns>
        public Consignacion ConsultarCajero(Usuario pUsuario)
        {
            try
            {
                return BOConsignacion.ConsultarCajero(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsignacionService", "ConsultarCajero", ex);
                return null;
            }
        }


        public Consignacion ConsultarUsuario(Usuario pUsuario)
        {
            try
            {
                return BOConsignacion.ConsultarUsuario(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsignacionService", "ConsultarUsuario", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar ConsignacionCheque
        /// </summary>
        /// <param name="pConsignacionCheque">Entidad Consignacion</param>
        /// <returns>Entidad Consignacion modificada</returns>
        public Consignacion GrabarCanje(Consignacion pConsignacionCheque,MotivoDevChe pMotivodevChe, Usuario pUsuario)
        {
            try
            {
                return BOConsignacion.GrabarCanje(pConsignacionCheque, pMotivodevChe, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConsignacionService", "ModificarConsignacionCheque", ex);
                return null;
            }
        }

    }
}