using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Caja.Data;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Business
{
    /// <summary>
    /// Objeto de negocio para Usuarios
    /// </summary>
    public class UsuariosBusiness : GlobalData
    {
        private UsuariosData DAUsuarios;

        /// <summary>
        /// Constructor del objeto de negocio para Usuarios
        /// </summary>
        public UsuariosBusiness()
        {
            DAUsuarios = new UsuariosData();
        }

        /// <summary>
        /// Crea un Usuarios
        /// </summary>
        /// <param name="pUsuarios">Entidad Usuarios</param>
        /// <returns>Entidad Usuarios creada</returns>
        public Usuarios CrearUsuarios(Usuarios pUsuarios, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pUsuarios = DAUsuarios.CrearUsuarios(pUsuarios, pUsuario);

                    ts.Complete();
                }

                return pUsuarios;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuariosBusiness", "CrearUsuarios", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Usuarios
        /// </summary>
        /// <param name="pUsuarios">Entidad Usuarios</param>
        /// <returns>Entidad Usuarios modificada</returns>
        public Usuarios ModificarUsuarios(Usuarios pUsuarios, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pUsuarios = DAUsuarios.ModificarUsuarios(pUsuarios, pUsuario);

                    ts.Complete();
                }

                return pUsuarios;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuariosBusiness", "ModificarUsuarios", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Usuarios
        /// </summary>
        /// <param name="pId">Identificador de Usuarios</param>
        public void EliminarUsuarios(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAUsuarios.EliminarUsuarios(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuariosBusiness", "EliminarUsuarios", ex);
            }
        }

        /// <summary>
        /// Obtiene un Usuarios
        /// </summary>
        /// <param name="pId">Identificador de Usuarios</param>
        /// <returns>Entidad Usuarios</returns>
        public Usuarios ConsultarUsuarios(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAUsuarios.ConsultarUsuarios(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuariosBusiness", "ConsultarUsuarios", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pUsuarios">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuarios obtenidos</returns>
        public List<Usuarios> ListarUsuarios(Usuarios pUsuarios, Usuario pUsuario)
        {
            try
            {
                return DAUsuarios.ListarUsuarios(pUsuarios, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuariosBusiness", "ListarUsuarios", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pUsuarios">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuarios obtenidos</returns>
        public List<Usuarios> ListarUsuariosXOficina(Usuarios pUsuarios, Usuario pUsuario)
        {
            try
            {
                return DAUsuarios.ListarUsuariosXOficina(pUsuarios, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuariosBusiness", "ListarUsuariosXOficina", ex);
                return null;
            }
        }



        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pUsuarios">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuarios obtenidos</returns>
        public List<Usuarios> ListarUsuariosXOficina2(Usuarios pUsuarios, Usuario pUsuario)
        {
            try
            {
                return DAUsuarios.ListarUsuariosXOficina2(pUsuarios, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuariosBusiness", "ListarUsuariosXOficina2", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pUsuarios">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuarios obtenidos</returns>
        public List<Usuarios> ListarComboCajero(Usuarios pEntidad, Int32? pEstado = null, Usuario pUsuario = null)
        {
            try
            {
                return DAUsuarios.ListarComboCajero(pEntidad, pEstado, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuariosBusiness", "ListarComboUsuarios", ex);
                return null;
            }
        }

    }
}