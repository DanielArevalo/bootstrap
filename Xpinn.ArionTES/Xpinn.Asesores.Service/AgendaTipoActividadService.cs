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
    public class AgendaTipoActividadService
    {
        private AgendaTipoActividadBusiness BOAgendaTipoActividad;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para AgendaTipoActividad
        /// </summary>
        public AgendaTipoActividadService()
        {
            BOAgendaTipoActividad = new AgendaTipoActividadBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110202"; } }

        /// <summary>
        /// Servicio para crear AgendaTipoActividad
        /// </summary>
        /// <param name="pEntity">Entidad AgendaTipoActividad</param>
        /// <returns>Entidad AgendaTipoActividad creada</returns>
        public AgendaTipoActividad CrearAgendaTipoActividad(AgendaTipoActividad pAgendaTipoActividad, Usuario pUsuario)
        {
            try
            {
                return BOAgendaTipoActividad.CrearAgendaTipoActividad(pAgendaTipoActividad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaTipoActividadService", "CrearAgendaTipoActividad", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar AgendaTipoActividad
        /// </summary>
        /// <param name="pAgendaTipoActividad">Entidad AgendaTipoActividad</param>
        /// <returns>Entidad AgendaTipoActividad modificada</returns>
        public AgendaTipoActividad ModificarAgendaTipoActividad(AgendaTipoActividad pAgendaTipoActividad, Usuario pUsuario)
        {
            try
            {
                return BOAgendaTipoActividad.ModificarAgendaTipoActividad(pAgendaTipoActividad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaTipoActividadService", "ModificarAgendaTipoActividad", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar AgendaTipoActividad
        /// </summary>
        /// <param name="pId">identificador de AgendaTipoActividad</param>
        public void EliminarAgendaTipoActividad(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOAgendaTipoActividad.EliminarAgendaTipoActividad(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarAgendaTipoActividad", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener AgendaTipoActividad
        /// </summary>
        /// <param name="pId">identificador de AgendaTipoActividad</param>
        /// <returns>Entidad AgendaTipoActividad</returns>
        public AgendaTipoActividad ConsultarAgendaTipoActividad(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOAgendaTipoActividad.ConsultarAgendaTipoActividad(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaTipoActividadService", "ConsultarAgendaTipoActividad", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de AgendaTipoActividads a partir de unos filtros
        /// </summary>
        /// <param name="pAgendaTipoActividad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de AgendaTipoActividad obtenidos</returns>
        public List<AgendaTipoActividad> ListarAgendaTipoActividad(AgendaTipoActividad pAgendaTipoActividad, Usuario pUsuario)
        {
            try
            {
                return BOAgendaTipoActividad.ListarAgendaTipoActividad(pAgendaTipoActividad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaTipoActividadService", "ListarAgendaTipoActividad", ex);
                return null;
            }
        }

        /// <summary>
        /// Determina si un usuario esta activo y es asesor
        /// </summary>
        /// <param name="cod"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public Int32 UsuarioEsAsesor(int cod, Usuario pUsuario)
        {
            return BOAgendaTipoActividad.UsuarioEsAsesor(cod, pUsuario);
        }

        public List<Persona> correo(int cod, Usuario pUsuario)
        {
            return BOAgendaTipoActividad.correo(cod, pUsuario);
        }

        public List<Persona> Listarclientes(long cod, Usuario pUsuario)
        {
            return BOAgendaTipoActividad.Listarclientes(cod, pUsuario);
        }
    }
}