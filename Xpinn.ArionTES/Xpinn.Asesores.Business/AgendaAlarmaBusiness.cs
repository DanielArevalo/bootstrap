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
    /// Objeto de negocio para AgendaAlarma
    /// </summary>
    public class AgendaAlarmaBusiness : GlobalBusiness
    {
        private AgendaAlarmaData DAAgendaAlarma;

        /// <summary>
        /// Constructor del objeto de negocio para AgendaAlarma
        /// </summary>
        public AgendaAlarmaBusiness()
        {
            DAAgendaAlarma = new AgendaAlarmaData();
        }

        /// <summary>
        /// Crea un AgendaAlarma
        /// </summary>
        /// <param name="pAgendaAlarma">Entidad AgendaAlarma</param>
        /// <returns>Entidad AgendaAlarma creada</returns>
        public AgendaAlarma CrearAgendaAlarma(AgendaAlarma pAgendaAlarma, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAgendaAlarma = DAAgendaAlarma.CrearAgendaAlarma(pAgendaAlarma, pUsuario);

                    ts.Complete();
                }

                return pAgendaAlarma;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaAlarmaBusiness", "CrearAgendaAlarma", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un AgendaAlarma
        /// </summary>
        /// <param name="pAgendaAlarma">Entidad AgendaAlarma</param>
        /// <returns>Entidad AgendaAlarma modificada</returns>
        public AgendaAlarma ModificarAgendaAlarma(AgendaAlarma pAgendaAlarma, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAgendaAlarma = DAAgendaAlarma.ModificarAgendaAlarma(pAgendaAlarma, pUsuario);

                    ts.Complete();
                }

                return pAgendaAlarma;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaAlarmaBusiness", "ModificarAgendaAlarma", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un AgendaAlarma
        /// </summary>
        /// <param name="pId">Identificador de AgendaAlarma</param>
        public void EliminarAgendaAlarma(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAAgendaAlarma.EliminarAgendaAlarma(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaAlarmaBusiness", "EliminarAgendaAlarma", ex);
            }
        }

        /// <summary>
        /// Obtiene un AgendaAlarma
        /// </summary>
        /// <param name="pId">Identificador de AgendaAlarma</param>
        /// <returns>Entidad AgendaAlarma</returns>
        public AgendaAlarma ConsultarAgendaAlarma(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAAgendaAlarma.ConsultarAgendaAlarma(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaAlarmaBusiness", "ConsultarAgendaAlarma", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pAgendaAlarma">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de AgendaAlarma obtenidos</returns>
        public List<AgendaAlarma> ListarAgendaAlarma(AgendaAlarma pAgendaAlarma, Usuario pUsuario)
        {
            try
            {
                return DAAgendaAlarma.ListarAgendaAlarma(pAgendaAlarma, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaAlarmaBusiness", "ListarAgendaAlarma", ex);
                return null;
            }
        }

    }
}