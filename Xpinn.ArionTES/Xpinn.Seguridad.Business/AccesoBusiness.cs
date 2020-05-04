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
    /// Objeto de negocio para Acceso
    /// </summary>
    public class AccesoBusiness : GlobalBusiness
    {
        private AccesoData DAAcceso;

        /// <summary>
        /// Constructor del objeto de negocio para Acceso
        /// </summary>
        public AccesoBusiness()
        {
            DAAcceso = new AccesoData();
        }

        /// <summary>
        /// Crea un Acceso
        /// </summary>
        /// <param name="pAcceso">Entidad Acceso</param>
        /// <returns>Entidad Acceso creada</returns>
        public Acceso CrearAcceso(Acceso pAcceso, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAcceso = DAAcceso.CrearAcceso(pAcceso, pUsuario);

                    ts.Complete();
                }

                return pAcceso;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AccesoBusiness", "CrearAcceso", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Acceso
        /// </summary>
        /// <param name="pAcceso">Entidad Acceso</param>
        /// <returns>Entidad Acceso modificada</returns>
        public Acceso ModificarAcceso(Acceso pAcceso, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAcceso = DAAcceso.ModificarAcceso(pAcceso, pUsuario);

                    ts.Complete();
                }

                return pAcceso;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AccesoBusiness", "ModificarAcceso", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Acceso
        /// </summary>
        /// <param name="pId">Identificador de Acceso</param>
        public void EliminarAcceso(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAAcceso.EliminarAcceso(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AccesoBusiness", "EliminarAcceso", ex);
            }
        }

        /// <summary>
        /// Obtiene un Acceso
        /// </summary>
        /// <param name="pId">Identificador de Acceso</param>
        /// <returns>Entidad Acceso</returns>
        public Acceso ConsultarAcceso(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAAcceso.ConsultarAcceso(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AccesoBusiness", "ConsultarAcceso", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pAcceso">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Acceso obtenidos</returns>
        public List<Acceso> ListarAcceso(Acceso pAcceso, Usuario pUsuario)
        {
            try
            {
                return DAAcceso.ListarAcceso(pAcceso, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AccesoBusiness", "ListarAcceso", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene todos los permisos a las opciones para un perfil especifico
        /// </summary>
        /// <param name="pIdPerfil">identificador del perfil</param>
        /// <returns>Conjunto de opciones</returns>
        public List<Acceso> ListarAcceso(Int64 pIdPerfil, Usuario pUsuario,String Idioma)
        {
            try
            {
                return DAAcceso.ListarAcceso(pIdPerfil, pUsuario, Idioma);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AccesoBusiness", "ListarAcceso", ex);
                return null;
            }
        }

        public List<Acceso> ListarAccesoAAC(Int64 pIdModulo, Usuario pUsuario)
        {
            try
            {
                return DAAcceso.ListarAccesoAAC(pIdModulo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AccesoBusiness", "ListarAccesoAAC", ex);
                return null;
            }
        }

        public List<Acceso> ListarAccesoApp(Usuario pUsuario)
        {
            try
            {
                return DAAcceso.ListarAccesoApp(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AccesoBusiness", "ListarAccesoApp", ex);
                return null;
            }
        }
    }
}