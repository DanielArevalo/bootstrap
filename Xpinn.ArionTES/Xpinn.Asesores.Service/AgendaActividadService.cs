using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class AgendaActividadService
    {
        private AgendaActividadBusiness BOAgendaActividad;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para AgendaActividad
        /// </summary>
        public AgendaActividadService()
        {
            BOAgendaActividad = new AgendaActividadBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110110"; } }

        /// <summary>
        /// Servicio para crear AgendaActividad
        /// </summary>
        /// <param name="pEntity">Entidad AgendaActividad</param>
        /// <returns>Entidad AgendaActividad creada</returns>
        public AgendaActividad CrearAgendaActividad(AgendaActividad pAgendaActividad, Usuario pUsuario)
        {
            try
            {
                return BOAgendaActividad.CrearAgendaActividad(pAgendaActividad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaActividadService", "CrearAgendaActividad", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar AgendaActividad
        /// </summary>
        /// <param name="pAgendaActividad">Entidad AgendaActividad</param>
        /// <returns>Entidad AgendaActividad modificada</returns>
        public AgendaActividad ModificarAgendaActividad(AgendaActividad pAgendaActividad, Usuario pUsuario)
        {
            //try
            //{
                return BOAgendaActividad.ModificarAgendaActividad(pAgendaActividad, pUsuario);
            //}
            //catch (Exception ex)
            //{
            //    BOExcepcion.Throw("AgendaActividadService", "ModificarAgendaActividad", ex);
            //    return null;
            //}
        }

        /// <summary>
        /// Servicio para Eliminar AgendaActividad
        /// </summary>
        /// <param name="pId">identificador de AgendaActividad</param>
        public void EliminarAgendaActividad(Int64 pId, Usuario pUsuario)
        {
            //try
            //{
                BOAgendaActividad.EliminarAgendaActividad(pId, pUsuario);
            //}
            //catch (Exception ex)
            //{
            //    BOExcepcion.Throw("$Programa$Service", "EliminarAgendaActividad", ex);
            //}
        }

        /// <summary>
        /// Servicio para obtener AgendaActividad
        /// </summary>
        /// <param name="pId">identificador de AgendaActividad</param>
        /// <returns>Entidad AgendaActividad</returns>
        public AgendaActividad ConsultarAgendaActividad(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOAgendaActividad.ConsultarAgendaActividad(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaActividadService", "ConsultarAgendaActividad", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de AgendaActividads a partir de unos filtros
        /// </summary>
        /// <param name="pAgendaActividad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de AgendaActividad obtenidos</returns>
        public List<AgendaActividad> ListarAgendaActividad(AgendaActividad pAgendaActividad, Usuario pUsuario)
        {
            try
            {
                return BOAgendaActividad.ListarAgendaActividad(pAgendaActividad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaActividadService", "ListarAgendaActividad", ex);
                return null;
            }
        }
    }
}