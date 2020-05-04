using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Caja.Business;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class UsuariosService
    {
        private UsuariosBusiness BOUsuarios;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Usuarios
        /// </summary>
        public UsuariosService()
        {
            BOUsuarios = new UsuariosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "SG030"; } }

        /// <summary>
        /// Servicio para crear Usuarios
        /// </summary>
        /// <param name="pEntity">Entidad Usuarios</param>
        /// <returns>Entidad Usuarios creada</returns>
        public Usuarios CrearUsuarios(Usuarios pUsuarios, Usuario pUsuario)
        {
            try
            {
                return BOUsuarios.CrearUsuarios(pUsuarios, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuariosService", "CrearUsuarios", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Usuarios
        /// </summary>
        /// <param name="pUsuarios">Entidad Usuarios</param>
        /// <returns>Entidad Usuarios modificada</returns>
        public Usuarios ModificarUsuarios(Usuarios pUsuarios, Usuario pUsuario)
        {
            try
            {
                return BOUsuarios.ModificarUsuarios(pUsuarios, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuariosService", "ModificarUsuarios", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Usuarios
        /// </summary>
        /// <param name="pId">identificador de Usuarios</param>
        public void EliminarUsuarios(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOUsuarios.EliminarUsuarios(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarUsuarios", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Usuarios
        /// </summary>
        /// <param name="pId">identificador de Usuarios</param>
        /// <returns>Entidad Usuarios</returns>
        public Usuarios ConsultarUsuarios(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOUsuarios.ConsultarUsuarios(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuariosService", "ConsultarUsuarios", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Usuarioss a partir de unos filtros
        /// </summary>
        /// <param name="pUsuarios">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuarios obtenidos</returns>
        public List<Usuarios> ListarUsuarios(Usuarios pUsuarios, Usuario pUsuario)
        {
            try
            {
                return BOUsuarios.ListarUsuarios(pUsuarios, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuariosService", "ListarUsuarios", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Usuarioss a partir de unos filtros
        /// </summary>
        /// <param name="pUsuarios">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuarios obtenidos</returns>
        public List<Usuarios> ListarUsuariosXOficina(Usuarios pUsuarios, Usuario pUsuario)
        {
            try
            {
                return BOUsuarios.ListarUsuariosXOficina(pUsuarios, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuariosService", "ListarUsuariosXOficinas", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para obtener lista de Usuarioss a partir de unos filtros
        /// </summary>
        /// <param name="pUsuarios">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuarios obtenidos</returns>
        public List<Usuarios> ListarUsuariosXOficina2(Usuarios pUsuarios, Usuario pUsuario)
        {
            try
            {
                return BOUsuarios.ListarUsuariosXOficina2(pUsuarios, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuariosService", "ListarUsuariosXOficinas", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para obtener lista de Usuarioss a partir de unos filtros
        /// </summary>
        /// <param name="pUsuarios">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuarios obtenidos</returns>
        public List<Usuarios> ListarComboCajero(Usuarios pEntidad, Int32? pEstado = null, Usuario pUsuario = null)
        {
            try
            {
                return BOUsuarios.ListarComboCajero(pEntidad, pEstado, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuariosService", "ListarComboCajero", ex);
                return null;
            }
        }

    }
}