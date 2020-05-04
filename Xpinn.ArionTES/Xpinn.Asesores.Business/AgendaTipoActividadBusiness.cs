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
    /// Objeto de negocio para AgendaTipoActividad
    /// </summary>
    public class AgendaTipoActividadBusiness : GlobalBusiness
    {
        private AgendaTipoActividadData DAAgendaTipoActividad;

        /// <summary>
        /// Constructor del objeto de negocio para AgendaTipoActividad
        /// </summary>
        public AgendaTipoActividadBusiness()
        {
            DAAgendaTipoActividad = new AgendaTipoActividadData();
        }

        /// <summary>
        /// Crea un AgendaTipoActividad
        /// </summary>
        /// <param name="pAgendaTipoActividad">Entidad AgendaTipoActividad</param>
        /// <returns>Entidad AgendaTipoActividad creada</returns>
        public AgendaTipoActividad CrearAgendaTipoActividad(AgendaTipoActividad pAgendaTipoActividad, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAgendaTipoActividad = DAAgendaTipoActividad.CrearAgendaTipoActividad(pAgendaTipoActividad, pUsuario);

                    ts.Complete();
                }

                return pAgendaTipoActividad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaTipoActividadBusiness", "CrearAgendaTipoActividad", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un AgendaTipoActividad
        /// </summary>
        /// <param name="pAgendaTipoActividad">Entidad AgendaTipoActividad</param>
        /// <returns>Entidad AgendaTipoActividad modificada</returns>
        public AgendaTipoActividad ModificarAgendaTipoActividad(AgendaTipoActividad pAgendaTipoActividad, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAgendaTipoActividad = DAAgendaTipoActividad.ModificarAgendaTipoActividad(pAgendaTipoActividad, pUsuario);

                    ts.Complete();
                }

                return pAgendaTipoActividad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaTipoActividadBusiness", "ModificarAgendaTipoActividad", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un AgendaTipoActividad
        /// </summary>
        /// <param name="pId">Identificador de AgendaTipoActividad</param>
        public void EliminarAgendaTipoActividad(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAAgendaTipoActividad.EliminarAgendaTipoActividad(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaTipoActividadBusiness", "EliminarAgendaTipoActividad", ex);
            }
        }

        /// <summary>
        /// Obtiene un AgendaTipoActividad
        /// </summary>
        /// <param name="pId">Identificador de AgendaTipoActividad</param>
        /// <returns>Entidad AgendaTipoActividad</returns>
        public AgendaTipoActividad ConsultarAgendaTipoActividad(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAAgendaTipoActividad.ConsultarAgendaTipoActividad(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaTipoActividadBusiness", "ConsultarAgendaTipoActividad", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pAgendaTipoActividad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de AgendaTipoActividad obtenidos</returns>
        public List<AgendaTipoActividad> ListarAgendaTipoActividad(AgendaTipoActividad pAgendaTipoActividad, Usuario pUsuario)
        {
            try
            {
                return DAAgendaTipoActividad.ListarAgendaTipoActividad(pAgendaTipoActividad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AgendaTipoActividadBusiness", "ListarAgendaTipoActividad", ex);
                return null;
            }
        }

        public List<Persona> Listarclientes(long cod, Usuario pUsuario)
        {

            return DAAgendaTipoActividad.Listarclientes(cod, pUsuario);
           
        }

        /// <summary>
        /// Determina si un usuario especifico esta activo y es un asesor activo
        /// </summary>
        /// <param name="cod"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public Int32 UsuarioEsAsesor(int cod, Usuario pUsuario)
        {
            return DAAgendaTipoActividad.UsuarioEsAsesor(cod, pUsuario);
        }

        public List<Persona> correo(int cod, Usuario pUsuario)
        {
            return DAAgendaTipoActividad.correo(cod, pUsuario);
        }

    }
}