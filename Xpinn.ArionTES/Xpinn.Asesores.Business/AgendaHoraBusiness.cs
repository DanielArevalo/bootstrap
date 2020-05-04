using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Asesores.Data;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Business
{
    /// <summary>
    /// Objeto de negocio para AgendaHora
    /// </summary>
    public class AgendaHoraBusiness : GlobalBusiness
    {
        private AgendaHoraData DAAgendaHora;

        /// <summary>
        /// Constructor del objeto de negocio para AgendaHora
        /// </summary>
        public AgendaHoraBusiness()
        {
            DAAgendaHora = new AgendaHoraData();
        }

        /// <summary>
        /// Crea un AgendaHora
        /// </summary>
        /// <param name="pAgendaHora">Entidad AgendaHora</param>
        /// <returns>Entidad AgendaHora creada</returns>
        public AgendaHora CrearAgendaHora(AgendaHora pAgendaHora, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAgendaHora = DAAgendaHora.CrearAgendaHora(pAgendaHora, pUsuario);

                    ts.Complete();
                }

                return pAgendaHora;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaHoraBusiness", "CrearAgendaHora", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un AgendaHora
        /// </summary>
        /// <param name="pAgendaHora">Entidad AgendaHora</param>
        /// <returns>Entidad AgendaHora modificada</returns>
        public AgendaHora ModificarAgendaHora(AgendaHora pAgendaHora, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAgendaHora = DAAgendaHora.ModificarAgendaHora(pAgendaHora, pUsuario);

                    ts.Complete();
                }

                return pAgendaHora;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaHoraBusiness", "ModificarAgendaHora", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un AgendaHora
        /// </summary>
        /// <param name="pId">Identificador de AgendaHora</param>
        public void EliminarAgendaHora(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAAgendaHora.EliminarAgendaHora(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaHoraBusiness", "EliminarAgendaHora", ex);
            }
        }

        /// <summary>
        /// Obtiene un AgendaHora
        /// </summary>
        /// <param name="pId">Identificador de AgendaHora</param>
        /// <returns>Entidad AgendaHora</returns>
        public AgendaHora ConsultarAgendaHora(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAAgendaHora.ConsultarAgendaHora(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaHoraBusiness", "ConsultarAgendaHora", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pAgendaHora">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de AgendaHora obtenidos</returns>
        public List<AgendaHora> ListarAgendaHora(AgendaHora pAgendaHora, Usuario pUsuario)
        {
            try
            {
                return DAAgendaHora.ListarAgendaHora(pAgendaHora, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaHoraBusiness", "ListarAgendaHora", ex);
                return null;
            }
        }
    }
}