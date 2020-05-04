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
    /// Objeto de negocio para Usuario
    /// </summary>
    public class UsuarioAseBusiness : GlobalBusiness
    {
        private UsuarioAseData DAUsuario;

        /// <summary>
        /// Constructor del objeto de negocio para Usuario
        /// </summary>
        public UsuarioAseBusiness()
        {
            DAUsuario = new UsuarioAseData();
        }

        /// <summary>
        /// Crea un Usuario
        /// </summary>
        /// <param name="pUsuario">Entidad Usuario</param>
        /// <returns>Entidad Usuario creada</returns>
        public UsuarioAse CrearUsuario(UsuarioAse pUsuarioAse, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pUsuarioAse = DAUsuario.CrearUsuario(pUsuarioAse, pUsuario);

                    ts.Complete();
                }

                return pUsuarioAse;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioBusiness", "CrearUsuario", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Usuario
        /// </summary>
        /// <param name="pUsuario">Entidad Usuario</param>
        /// <returns>Entidad Usuario modificada</returns>
        public UsuarioAse ModificarUsuario(UsuarioAse pUsuarioAse, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pUsuarioAse = DAUsuario.ModificarUsuario(pUsuarioAse, pUsuario);

                    ts.Complete();
                }

                return pUsuarioAse;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioBusiness", "ModificarUsuario", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Usuario
        /// </summary>
        /// <param name="pId">Identificador de Usuario</param>
        public void EliminarUsuario(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAUsuario.EliminarUsuario(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioBusiness", "EliminarUsuario", ex);
            }
        }

        /// <summary>
        /// Obtiene un Usuario
        /// </summary>
        /// <param name="pId">Identificador de Usuario</param>
        /// <returns>Entidad Usuario</returns>
        public UsuarioAse ConsultarUsuario(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAUsuario.ConsultarUsuario(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioBusiness", "ConsultarUsuario", ex);
                return null;
            }
        }
        public UsuarioAse ConsultarUsuarios(string pId, Usuario pUsuario)
        {
            try
            {
                return DAUsuario.ConsultarUsuarios(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioBusiness", "ConsultarUsuario", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pUsuario">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuario obtenidos</returns>
        public List<UsuarioAse> ListartodosUsuarios(UsuarioAse pUsuarioAse, Usuario pUsuario)
        {
            try
            {
                return DAUsuario.ListartodosUsuarios(pUsuarioAse, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioBusiness", "ListarUsuario", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pUsuario">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuario obtenidos</returns>
        public List<UsuarioAse> ListarUsuario(UsuarioAse pUsuarioAse, Usuario pUsuario)
        {
            try
            {
                return DAUsuario.ListarUsuario(pUsuarioAse, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioBusiness", "ListarUsuario", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la  lista de Usuarios-abogados dados unos filtros
        /// </summary>
        /// <param name="pUsuario">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuarios-abogados obtenidos</returns>
        public List<UsuarioAse> ListarUsuarioAbogados(UsuarioAse pUsuarioAse, Usuario pUsuario)
        {
            try
            {
                return DAUsuario.ListarUsuarioAbogados(pUsuarioAse, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioBusiness", "ListarUsuarioAbogados", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pUsuario">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuarios perfil abogados obtenidos</returns>
        public List<UsuarioAse> ListarUsuariosPerfilAbogado(UsuarioAse pUsuarioAse, Usuario pUsuario)
        {
            try
            {
                return DAUsuario.ListarUsuariosPerfilAbogado(pUsuarioAse, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioBusiness", "ListarUsuariosPerfilAbogado", ex);
                return null;
            }
        }

    }
}