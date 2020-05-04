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
    public class SolicitudCreditosRecogidosService
    {
        private SolicitudCreditosRecogidosBusiness BOSolicitudCreditosRecogidos;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para SolicitudCreditosRecogidos
        /// </summary>
        public SolicitudCreditosRecogidosService()
        {
            BOSolicitudCreditosRecogidos = new SolicitudCreditosRecogidosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100145"; } }
        public string CodigoProgramaModificacion { get { return "100150"; } }
        public string CodigoprogramaSoliciCredi { get { return "170118"; } }
        /// <summary>
        /// Servicio para crear SolicitudCreditosRecogidos
        /// </summary>
        /// <param name="pEntity">Entidad SolicitudCreditosRecogidos</param>
        /// <returns>Entidad SolicitudCreditosRecogidos creada</returns>
        public SolicitudCreditosRecogidos CrearSolicitudCreditosRecogidos(SolicitudCreditosRecogidos pSolicitudCreditosRecogidos, Usuario pUsuario)
        {
            try
            {
                return BOSolicitudCreditosRecogidos.CrearSolicitudCreditosRecogidos(pSolicitudCreditosRecogidos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCreditosRecogidosService", "CrearSolicitudCreditosRecogidos", ex);
                return null;
            }
        }




         public SolicitudRecogidoAvance CrearSolicitudCreditosRecogidosAvances(SolicitudRecogidoAvance pSolicitudCreditosRecogidos, Usuario pUsuario)
        {
            try
            {
                return BOSolicitudCreditosRecogidos.CrearSolicitudCreditosRecogidos(pSolicitudCreditosRecogidos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCreditosRecogidosService", "CrearSolicitudCreditosRecogidos", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para enviar Parametros al sp usp_xpinn_solicred_recoger
        /// </summary>
        /// <param name="pSolicitudCreditosRecogidos">Entidad SolicitudCreditosRecogidos</param>
        /// <returns>Entidad SolicitudCreditosRecogidos modificada</returns>
        public SolicitudCreditosRecogidos ParametrosSolicredRecoger(SolicitudCreditosRecogidos pSolicitudCreditosRecogidos, Usuario pUsuario)
        {
            try
            {
                return BOSolicitudCreditosRecogidos.ParametrosSolicredRecoger(pSolicitudCreditosRecogidos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCreditosRecogidosService", "ModificarSolicitudCreditosRecogidos", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para modificar SolicitudCreditosRecogidos
        /// </summary>
        /// <param name="pSolicitudCreditosRecogidos">Entidad SolicitudCreditosRecogidos</param>
        /// <returns>Entidad SolicitudCreditosRecogidos modificada</returns>
        public SolicitudCreditosRecogidos ModificarSolicitudCreditosRecogidos(SolicitudCreditosRecogidos pSolicitudCreditosRecogidos, Usuario pUsuario)
        {
            try
            {
                return BOSolicitudCreditosRecogidos.ModificarSolicitudCreditosRecogidos(pSolicitudCreditosRecogidos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCreditosRecogidosService", "ModificarSolicitudCreditosRecogidos", ex);
                return null;
            }
        }



        /// <summary>
        /// Servicio para Eliminar SolicitudCreditosRecogidos
        /// </summary>
        /// <param name="pId">identificador de SolicitudCreditosRecogidos</param>
        public void EliminarSolicitudCreditosRecogidos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOSolicitudCreditosRecogidos.EliminarSolicitudCreditosRecogidos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarSolicitudCreditosRecogidos", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener SolicitudCreditosRecogidos
        /// </summary>
        /// <param name="pId">identificador de SolicitudCreditosRecogidos</param>
        /// <returns>Entidad SolicitudCreditosRecogidos</returns>
        public SolicitudCreditosRecogidos ConsultarSolicitudCreditosRecogidos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOSolicitudCreditosRecogidos.ConsultarSolicitudCreditosRecogidos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCreditosRecogidosService", "ConsultarSolicitudCreditosRecogidos", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de SolicitudCreditosRecogidoss a partir de unos filtros
        /// </summary>
        /// <param name="pSolicitudCreditosRecogidos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de SolicitudCreditosRecogidos obtenidos</returns>
        public List<SolicitudCreditosRecogidos> ListarSolicitudCreditosRecogidos(SolicitudCreditosRecogidos pSolicitudCreditosRecogidos, Usuario pUsuario)
        {
            try
            {
                return BOSolicitudCreditosRecogidos.ListarSolicitudCreditosRecogidos(pSolicitudCreditosRecogidos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCreditosRecogidosService", "ListarSolicitudCreditosRecogidos", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de SolicitudCreditosRecogidoss a partir de unos filtros
        /// </summary>
        /// <param name="pSolicitudCreditosRecogidos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de SolicitudCreditosRecogidos obtenidos</returns>
        public List<SolicitudCreditosRecogidos> ListarSolicitudCreditosRecogidos(Usuario pUsuario)
        {
            try
            {
                return BOSolicitudCreditosRecogidos.ListarSolicitudCreditosRecogidos(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCreditosRecogidosService", "ListarSolicitudCreditosRecogidos", ex);
                return null;
            }
        }

        /// <summary>
        /// Mètodo para mostrar los crèditos recogidos
        /// </summary>
        /// <param name="pNumeroRadicacion"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<SolicitudCreditosRecogidos> ListarCreditosRecogidos(Int64 pNumeroRadicacion, Usuario pUsuario)
        {
            try
            {
                return BOSolicitudCreditosRecogidos.ListarCreditosRecogidos(pNumeroRadicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCreditosRecogidosBusiness", "ListarCreditosRecogidos", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para obtener lista de SolicitudCreditosRecogidoss a partir de unos filtros
        /// </summary>
        /// <param name="pSolicitudCreditosRecogidos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de SolicitudCreditosRecogidos obtenidos</returns>
        public List<SolicitudCreditosRecogidos> ListarTemp_recoger(SolicitudCreditosRecogidos pSolicitudCreditosRecogidos, Usuario pUsuario)
        {
            try
            {
                return BOSolicitudCreditosRecogidos.ListarTemp_recoger(pSolicitudCreditosRecogidos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCreditosRecogidosService", "ListarSolicitudCreditosRecogidos", ex);
                return null;
            }
        }


        //listado solicitudes de creditos
        public List<SolicitudCreditoAAC> ListarSolicitudCreditoAAC(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOSolicitudCreditosRecogidos.ListarSolicitudCreditoAAC(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCreditosRecogidosService", "ListarSolicitudCreditoAAC", ex);
                return null;
            }
        }


        public void EliminarSolicitudCreditoAAC(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOSolicitudCreditosRecogidos.EliminarSolicitudCreditoAAC(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCreditosRecogidosService", "ListarSolicitudCreditoAAC", ex);
            }
        }

        public Boolean ConfirmacionSolicitudCredito(List<SolicitudCreditoAAC> lstSolicitud, ref string pError, ref List<Credito> lstGenerados, Usuario pUsuario)
        {
            try
            {
                return BOSolicitudCreditosRecogidos.ConfirmacionSolicitudCredito(lstSolicitud, ref pError, ref lstGenerados, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCreditosRecogidosService", "ConfirmacionSolicitudCredito", ex);
                return false;
            }
        }

    }
}
