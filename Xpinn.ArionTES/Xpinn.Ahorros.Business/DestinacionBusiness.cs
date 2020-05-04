using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Ahorros.Data;
using Xpinn.Ahorros.Entities;

namespace Xpinn.Ahorros.Business
{
    /// <summary>
    /// Objeto de negocio para Destinacion
    /// </summary>
    public class DestinacionBusiness : GlobalBusiness
    {
        private DestinacionData DADestinacion;

        /// <summary>
        /// Constructor del objeto de negocio para Destinacion
        /// </summary>
        public DestinacionBusiness()
        {
            DADestinacion = new DestinacionData();
        }

        /// <summary>
        /// Crea un Destinacion
        /// </summary>
        /// <param name="pDestinacion">Entidad Destinacion</param>
        /// <returns>Entidad Destinacion creada</returns>
        public Destinacion CrearDestinacion(Destinacion pDestinacion, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pDestinacion = DADestinacion.CrearDestinacion(pDestinacion, pUsuario);
                    ts.Complete();
                }

                return pDestinacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DestinacionBusiness", "CrearDestinacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Destinacion
        /// </summary>
        /// <param name="pDestinacion">Entidad Destinacion</param>
        /// <returns>Entidad Destinacion modificada</returns>
        public Destinacion ModificarDestinacion(Destinacion pDestinacion, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pDestinacion = DADestinacion.ModificarDestinacion(pDestinacion, pUsuario);

                    ts.Complete();
                }

                return pDestinacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DestinacionBusiness", "ModificarDestinacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Destinacion
        /// </summary>
        /// <param name="pId">Identificador de Destinacion</param>
        public void EliminarDestinacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DADestinacion.EliminarDestinacion(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DestinacionBusiness", "EliminarDestinacion", ex);
            }
        }

        /// <summary>
        /// Obtiene un Destinacion
        /// </summary>
        /// <param name="pId">Identificador de Destinacion</param>
        /// <returns>Entidad Destinacion</returns>
        public Destinacion ConsultarDestinacion(Int64 pId, Usuario vUsuario)
        {
            try
            {
                Destinacion Destinacion = new Destinacion();
                Destinacion = DADestinacion.ConsultarDestinacion(pId, vUsuario);
                return Destinacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DestinacionBusiness", "ConsultarDestinacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pDestinacion">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Destinacion obtenidos</returns>
        public List<Destinacion> ListarDestinacion(Destinacion pDestinacion, Usuario pUsuario)
        {
            try
            {
                return DADestinacion.ListarDestinacion(pDestinacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DestinacionBusiness", "ListarDestinacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pDestinacion">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Destinacion obtenidos</returns>
        public List<Destinacion> ListarDestinacion_Ahorros(Destinacion pDestinacion, Usuario pUsuario)
        {
            try
            {
                return DADestinacion.ListarDestinacion_Ahorros(pDestinacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DestinacionBusiness", "ListarDestinacion_Ahorros", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return DADestinacion.ObtenerSiguienteCodigo(pUsuario);
            }
            catch
            {
                return 1;
            }
        }

    }
}