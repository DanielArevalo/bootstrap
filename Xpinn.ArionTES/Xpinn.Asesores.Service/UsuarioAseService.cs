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
    public class UsuarioAseService
    {
        private UsuarioAseBusiness BOUsuario;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Usuario
        /// </summary>
        public UsuarioAseService()
        {
            BOUsuario = new UsuarioAseBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110116"; } }

        /// <summary>
        /// Servicio para crear Usuario
        /// </summary>
        /// <param name="pEntity">Entidad Usuario</param>
        /// <returns>Entidad Usuario creada</returns>
        public UsuarioAse CrearUsuario(UsuarioAse pUsuarioAse, Usuario pUsuario)
        {
            try
            {
                return BOUsuario.CrearUsuario(pUsuarioAse, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "CrearUsuario", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Usuario
        /// </summary>
        /// <param name="pUsuario">Entidad Usuario</param>
        /// <returns>Entidad Usuario modificada</returns>
        public UsuarioAse ModificarUsuario(UsuarioAse pUsuarioAse, Usuario pUsuario)
        {
            try
            {
                return BOUsuario.ModificarUsuario(pUsuarioAse, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "ModificarUsuario", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Usuario
        /// </summary>
        /// <param name="pId">identificador de Usuario</param>
        public void EliminarUsuario(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOUsuario.EliminarUsuario(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarUsuario", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Usuario 
        /// </summary>
        /// <param name="pId">identificador de Usuario</param>
        /// <returns>Entidad Usuario</returns>
        public UsuarioAse ConsultarUsuario(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOUsuario.ConsultarUsuario(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "ConsultarUsuario", ex);
                return null;
            }
        }
        public UsuarioAse ConsultarUsuarios(string pId, Usuario pUsuario)
        {
            try
            {
                return BOUsuario.ConsultarUsuarios(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "ConsultarUsuario", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Usuarios a partir de unos filtros
        /// </summary>
        /// <param name="pUsuario">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuario obtenidos</returns>
       
        public List<UsuarioAse> ListarUsuario(UsuarioAse pUsuarioAse, Usuario pUsuario)
        {
            try
            {
                return BOUsuario.ListarUsuario(pUsuarioAse, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "ListarUsuario", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener lista de Usuarios-abogados asignados a partir de unos filtros
        /// </summary>
        /// <param name="pUsuario">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuarios-abogados asignados obtenidos</returns>
        /// ListartodosUsuarios
        public List<UsuarioAse> ListartodosUsuarios(UsuarioAse pUsuarioAse, Usuario pUsuario)
        {
            try
            {
                return BOUsuario.ListartodosUsuarios(pUsuarioAse, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "ListarUsuario", ex);
                return null;
            }
        }
        public List<UsuarioAse> ListarUsuarioAbogados(UsuarioAse pUsuarioAse, Usuario pUsuario)
        {
            try
            {
                return BOUsuario.ListarUsuarioAbogados(pUsuarioAse, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "ListarUsuarioAbogados", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener lista de Usuarios a partir de unos filtros
        /// </summary>
        /// <param name="pUsuario">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuarios con perfil de abogados obtenidos</returns>
        public List<UsuarioAse> ListarUsuariosPerfilAbogado(UsuarioAse pUsuarioAse, Usuario pUsuario)
        {
            try
            {
                return BOUsuario.ListarUsuariosPerfilAbogado(pUsuarioAse, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioService", "ListarUsuario", ex);
                return null;
            }
        }
    }
}