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
    public class AgendaHoraService
    {
        private AgendaHoraBusiness BOAgendaHora;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para AgendaHora
        /// </summary>
        public AgendaHoraService()
        {
            BOAgendaHora = new AgendaHoraBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110203"; } }

        /// <summary>
        /// Servicio para crear AgendaHora
        /// </summary>
        /// <param name="pEntity">Entidad AgendaHora</param>
        /// <returns>Entidad AgendaHora creada</returns>
        public AgendaHora CrearAgendaHora(AgendaHora pAgendaHora, Usuario pUsuario)
        {
            try
            {
                return BOAgendaHora.CrearAgendaHora(pAgendaHora, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaHoraService", "CrearAgendaHora", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar AgendaHora
        /// </summary>
        /// <param name="pAgendaHora">Entidad AgendaHora</param>
        /// <returns>Entidad AgendaHora modificada</returns>
        public AgendaHora ModificarAgendaHora(AgendaHora pAgendaHora, Usuario pUsuario)
        {
            try
            {
                return BOAgendaHora.ModificarAgendaHora(pAgendaHora, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaHoraService", "ModificarAgendaHora", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar AgendaHora
        /// </summary>
        /// <param name="pId">identificador de AgendaHora</param>
        public void EliminarAgendaHora(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOAgendaHora.EliminarAgendaHora(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarAgendaHora", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener AgendaHora
        /// </summary>
        /// <param name="pId">identificador de AgendaHora</param>
        /// <returns>Entidad AgendaHora</returns>
        public AgendaHora ConsultarAgendaHora(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOAgendaHora.ConsultarAgendaHora(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaHoraService", "ConsultarAgendaHora", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de AgendaHoras a partir de unos filtros
        /// </summary>
        /// <param name="pAgendaHora">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de AgendaHora obtenidos</returns>
        public List<AgendaHora> ListarAgendaHora(AgendaHora pAgendaHora, Usuario pUsuario)
        {
            try
            {
                return BOAgendaHora.ListarAgendaHora(pAgendaHora, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaHoraService", "ListarAgendaHora", ex);
                return null;
            }
        }
    }
}