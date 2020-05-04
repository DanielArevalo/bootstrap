using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Obligaciones.Business;
using Xpinn.Obligaciones.Entities;

namespace Xpinn.Obligaciones.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class SolicitudService
    {
        private SolicitudBusiness BOSolicitud;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Solicitud
        /// </summary>
        public SolicitudService()
        {
            BOSolicitud = new SolicitudBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "130101"; } }

        /// <summary>
        /// Servicio para crear Solicitud
        /// </summary>
        /// <param name="pEntity">Entidad Solicitud</param>
        /// <returns>Entidad Solicitud creada</returns>
        public Solicitud CrearSolicitud(Solicitud pSolicitud,DataTable dtComp, DataTable dtPagos, Usuario pUsuario)
        {
            try
            {
                return BOSolicitud.CrearSolicitud(pSolicitud, dtComp, dtPagos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudService", "CrearSolicitud", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Solicitud
        /// </summary>
        /// <param name="pSolicitud">Entidad Solicitud</param>
        /// <returns>Entidad Solicitud modificada</returns>
        public Solicitud ModificarSolicitud(Xpinn.Obligaciones.Entities.ObligacionCredito pOperacion, Solicitud pSolicitud, DataTable dtComp, DataTable dtPagos, Usuario pUsuario)
        {
            try
            {
                
                return BOSolicitud.ModificarSolicitud(pOperacion, pSolicitud, dtComp,dtPagos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudService", "ModificarSolicitud", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Solicitud
        /// </summary>
        /// <param name="pId">identificador de Solicitud</param>
        public void EliminarSolicitud(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOSolicitud.EliminarSolicitud(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarSolicitud", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Solicitud
        /// </summary>
        /// <param name="pId">identificador de Solicitud</param>
        /// <returns>Entidad Solicitud</returns>
        public Solicitud ConsultarSolicitud(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOSolicitud.ConsultarSolicitud(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudService", "ConsultarSolicitud", ex);
                return null;
            }
        }



        /// <summary>
        /// Servicio para obtener Estado de Cuenta
        /// </summary>
        /// <param name="pId">identificador de Estado de Cuenta</param>
        /// <returns>Id Solicitud</returns>
        public Solicitud ConsultarEstadoCuenta(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOSolicitud.ConsultarEstadoCuenta(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudService", "ConsultarEstadoCuenta", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Solicituds a partir de unos filtros
        /// </summary>
        /// <param name="pSolicitud">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Solicitud obtenidos</returns>
        public List<Solicitud> ListarSolicitud(Solicitud pSolicitud, Usuario pUsuario)
        {
            try
            {
                return BOSolicitud.ListarSolicitud(pSolicitud, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudService", "ListarSolicitud", ex);
                return null;
            }
        }





        /// <summary>
        /// Servicio para obtener Estado de Cuenta
        /// </summary>
        /// <param name="pId">identificador del tercero</param>
        /// <returns>Id Solicitud</returns>
        public string Consultar_Tercero(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOSolicitud.Consultar_Tercero(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudService", "Consultar_Tercero", ex);
                return null;
            }
        }

    }
}