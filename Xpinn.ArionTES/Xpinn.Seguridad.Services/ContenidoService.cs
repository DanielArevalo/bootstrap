using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Seguridad.Business;
using Xpinn.Seguridad.Entities;

namespace Xpinn.Seguridad.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ContenidoService
    {
        private ContenidoBusiness BOContenido;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Contenido
        /// </summary>
        public ContenidoService()
        {
            BOContenido = new ContenidoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "90118"; } }

        /// <summary>
        /// Servicio para crear Contenido
        /// </summary>
        /// <param name="pEntity">Entidad Contenido</param>
        /// <returns>Entidad Contenido creada</returns>
        public Contenido CrearContenido(Contenido pContenido, Usuario pUsuario)
        {
            try
            {
                return BOContenido.CrearContenido(pContenido, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContenidoService", "CrearContenido", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Contenido
        /// </summary>
        /// <param name="pContenido">Entidad Contenido</param>
        /// <returns>Entidad Contenido modificada</returns>
        public Contenido ModificarContenido(Contenido pContenido, Usuario pUsuario)
        {
            try
            {
                return BOContenido.ModificarContenido(pContenido, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContenidoService", "ModificarContenido", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Contenido
        /// </summary>
        /// <param name="pId">identificador de Contenido</param>
        public void EliminarContenido(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOContenido.EliminarContenido(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarContenido", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Contenido
        /// </summary>
        /// <param name="pId">identificador de Contenido</param>
        /// <returns>Entidad Contenido</returns>
        public Contenido ConsultarContenido(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOContenido.ConsultarContenido(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContenidoService", "ConsultarContenido", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Opcions a partir de unos filtros
        /// </summary>
        /// <param name="pContenido">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Contenido obtenidos</returns>
        public List<Contenido> ListarContenido(Contenido pContenido, Usuario pUsuario)
        {
            try
            {
                return BOContenido.ListarContenido(pContenido, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContenidoService", "ListarContenido", ex);
                return null;
            }
        }

        public Contenido ObtenerContenido(long pId, Usuario pUsuario)
        {
            try
            {
                return BOContenido.ObtenerContenido(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContenidoService", "ObtenerContenido", ex);
                return null;
            }
        }
    }
}
