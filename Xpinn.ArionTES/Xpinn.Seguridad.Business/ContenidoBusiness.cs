using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Seguridad.Data;
using Xpinn.Seguridad.Entities;

namespace Xpinn.Seguridad.Business
{
    /// <summary>
    /// Objeto de negocio para Contenido
    /// </summary>
    public class ContenidoBusiness : GlobalBusiness
    {
        private ContenidoData DAContenido;

        /// <summary>
        /// Constructor del objeto de negocio para Contenido
        /// </summary>
        public ContenidoBusiness()
        {
            DAContenido = new ContenidoData();
        }   

        /// <summary>
        /// Crea un Contenido para oficina virtual
        /// </summary>
        /// <param name="pContenido">Entidad Contenido</param>
        /// <returns>Entidad Contenido creada</returns>
        public Contenido CrearContenido(Contenido pContenido, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pContenido = DAContenido.CrearContenido(pContenido, pUsuario);

                    ts.Complete();
                }

                return pContenido;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContenidoBusiness", "CrearContenido", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Contenido
        /// </summary>
        /// <param name="pContenido">Entidad Contenido</param>
        /// <returns>Entidad Contenido modificada</returns>
        public Contenido ModificarContenido(Contenido pContenido, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pContenido = DAContenido.ModificarContenido(pContenido, pUsuario);

                    ts.Complete();
                }

                return pContenido;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContenidoBusiness", "ModificarContenido", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Contenido
        /// </summary>
        /// <param name="pId">Identificador de Contenido</param>
        public void EliminarContenido(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAContenido.EliminarContenido(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContenidoBusiness", "EliminarContenido", ex);
            }
        }

        /// <summary>
        /// Obtiene un Contenido
        /// </summary>
        /// <param name="pId">Identificador de Contenido</param>
        /// <returns>Entidad Contenido</returns>
        public Contenido ConsultarContenido(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAContenido.ConsultarContenido(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContenidoBusiness", "ConsultarContenido", ex);
                return null;
            }
        }



        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pContenido">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Contenido obtenidos</returns>
        public List<Contenido> ListarContenido(Contenido pContenido, Usuario pUsuario)
        {
            try
            {
                return DAContenido.ListarContenido(pContenido, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContenidoBusiness", "ListarContenido", ex);
                return null;
            }
        }

        public Contenido ObtenerContenido(long pId, Usuario pUsuario)
        {
            try
            {
                return DAContenido.ObtenerContenido(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContenidoBusiness", "ObtenerContenido", ex);
                return null;
            }
        }
    }
}