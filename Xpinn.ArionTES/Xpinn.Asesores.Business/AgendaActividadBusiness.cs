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
    /// Objeto de negocio para AgendaActividad
    /// </summary>
    public class AgendaActividadBusiness : GlobalBusiness
    {
        private AgendaActividadData DAAgendaActividad;

        /// <summary>
        /// Constructor del objeto de negocio para AgendaActividad
        /// </summary>
        public AgendaActividadBusiness()
        {
            DAAgendaActividad = new AgendaActividadData();
        }

        /// <summary>
        /// Crea un AgendaActividad
        /// </summary>
        /// <param name="pAgendaActividad">Entidad AgendaActividad</param>
        /// <returns>Entidad AgendaActividad creada</returns>
        public AgendaActividad CrearAgendaActividad(AgendaActividad pAgendaActividad, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                  pAgendaActividad = DAAgendaActividad.CrearAgendaActividad(pAgendaActividad, pUsuario);
                  ts.Complete();
                }
                return pAgendaActividad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaActividadBusiness", "CrearAgendaActividad", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un AgendaActividad
        /// </summary>
        /// <param name="pAgendaActividad">Entidad AgendaActividad</param>
        /// <returns>Entidad AgendaActividad modificada</returns>
        public AgendaActividad ModificarAgendaActividad(AgendaActividad pAgendaActividad, Usuario pUsuario)
        {
            //try
            //{
            //    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            //    {
            pAgendaActividad = DAAgendaActividad.ModificarAgendaActividad(pAgendaActividad, pUsuario);

            //        ts.Complete();
            //    }

                return pAgendaActividad;
            //}
            //catch (Exception ex)
            //{
            //    BOExcepcion.Throw("AgendaActividadBusiness", "ModificarAgendaActividad", ex);
            //    return null;
            //}
        }

        /// <summary>
        /// Elimina un AgendaActividad
        /// </summary>
        /// <param name="pId">Identificador de AgendaActividad</param>
        public void EliminarAgendaActividad(Int64 pId, Usuario pUsuario)
        {
            //try
            //{
            //    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            //    {
                    DAAgendaActividad.EliminarAgendaActividad(pId, pUsuario);

            //        ts.Complete();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    BOExcepcion.Throw("AgendaActividadBusiness", "EliminarAgendaActividad", ex);
            //}
        }

        /// <summary>
        /// Obtiene un AgendaActividad
        /// </summary>
        /// <param name="pId">Identificador de AgendaActividad</param>
        /// <returns>Entidad AgendaActividad</returns>
        public AgendaActividad ConsultarAgendaActividad(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAAgendaActividad.ConsultarAgendaActividad(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaActividadBusiness", "ConsultarAgendaActividad", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pAgendaActividad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de AgendaActividad obtenidos</returns>
        public List<AgendaActividad> ListarAgendaActividad(AgendaActividad pAgendaActividad, Usuario pUsuario)
        {
            try
            {
                return DAAgendaActividad.ListarAgendaActividad(pAgendaActividad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaActividadBusiness", "ListarAgendaActividad", ex);
                return null;
            }
        }

    }
}