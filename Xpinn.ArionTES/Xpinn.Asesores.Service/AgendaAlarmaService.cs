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
    public class AgendaAlarmaService
    {
        private AgendaAlarmaBusiness BOAgendaAlarma;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para AgendaAlarma
        /// </summary>
        public AgendaAlarmaService()
        {
            BOAgendaAlarma = new AgendaAlarmaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110110"; } }

        /// <summary>
        /// Servicio para crear AgendaAlarma
        /// </summary>
        /// <param name="pEntity">Entidad AgendaAlarma</param>
        /// <returns>Entidad AgendaAlarma creada</returns>
        public AgendaAlarma CrearAgendaAlarma(AgendaAlarma pAgendaAlarma, Usuario pUsuario)
        {
            try
            {
                return BOAgendaAlarma.CrearAgendaAlarma(pAgendaAlarma, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaAlarmaService", "CrearAgendaAlarma", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar AgendaAlarma
        /// </summary>
        /// <param name="pAgendaAlarma">Entidad AgendaAlarma</param>
        /// <returns>Entidad AgendaAlarma modificada</returns>
        public AgendaAlarma ModificarAgendaAlarma(AgendaAlarma pAgendaAlarma, Usuario pUsuario)
        {
            try
            {
                return BOAgendaAlarma.ModificarAgendaAlarma(pAgendaAlarma, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaAlarmaService", "ModificarAgendaAlarma", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar AgendaAlarma
        /// </summary>
        /// <param name="pId">identificador de AgendaAlarma</param>
        public void EliminarAgendaAlarma(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOAgendaAlarma.EliminarAgendaAlarma(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarAgendaAlarma", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener AgendaAlarma
        /// </summary>
        /// <param name="pId">identificador de AgendaAlarma</param>
        /// <returns>Entidad AgendaAlarma</returns>
        public AgendaAlarma ConsultarAgendaAlarma(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOAgendaAlarma.ConsultarAgendaAlarma(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaAlarmaService", "ConsultarAgendaAlarma", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de AgendaAlarmas a partir de unos filtros
        /// </summary>
        /// <param name="pAgendaAlarma">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de AgendaAlarma obtenidos</returns>
        public List<AgendaAlarma> ListarAgendaAlarma(AgendaAlarma pAgendaAlarma, Usuario pUsuario)
        {
            try
            {
                return BOAgendaAlarma.ListarAgendaAlarma(pAgendaAlarma, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaAlarmaService", "ListarAgendaAlarma", ex);
                return null;
            }
        }
    }
}