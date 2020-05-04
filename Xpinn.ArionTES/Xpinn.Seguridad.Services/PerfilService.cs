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
    public class PerfilService
    {
        private PerfilBusiness BOPerfil;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Perfil
        /// </summary>
        public PerfilService()
        {
            BOPerfil = new PerfilBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "90104"; } }

        /// <summary>
        /// Servicio para crear Perfil
        /// </summary>
        /// <param name="pEntity">Entidad Perfil</param>
        /// <returns>Entidad Perfil creada</returns>
        /// 

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario) 
        {
            try
            {
                return BOPerfil.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilService", "CrearPerfil", ex);
                return -1;
            }
        }
        public Perfil CrearPerfil(Perfil pPerfil, Usuario pUsuario)
        {
            try
            {
                return BOPerfil.CrearPerfil(pPerfil, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilService", "CrearPerfil", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Perfil
        /// </summary>
        /// <param name="pPerfil">Entidad Perfil</param>
        /// <returns>Entidad Perfil modificada</returns>
        public Perfil ModificarPerfil(Perfil pPerfil, Usuario pUsuario)
        {
            try
            {
                return BOPerfil.ModificarPerfil(pPerfil, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilService", "ModificarPerfil", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Perfil
        /// </summary>
        /// <param name="pId">identificador de Perfil</param>
        public void EliminarPerfil(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOPerfil.EliminarPerfil(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarPerfil", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Perfil
        /// </summary>
        /// <param name="pId">identificador de Perfil</param>
        /// <returns>Entidad Perfil</returns>
        public Perfil ConsultarPerfil(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOPerfil.ConsultarPerfil(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilService", "ConsultarPerfil", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Perfils a partir de unos filtros
        /// </summary>
        /// <param name="pPerfil">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Perfil obtenidos</returns>
        public List<Perfil> ListarPerfil(Perfil pPerfil, Usuario pUsuario)
        {
            try
            {
                return BOPerfil.ListarPerfil(pPerfil, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilService", "ListarPerfil", ex);
                return null;
            }
        }


        public List<Acceso> ListarOpciones(Int64 IdPerfil, Int64 CodModulo, Usuario pUsuario)
        {
            try
            {
                return BOPerfil.ListarOpciones(IdPerfil, CodModulo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilService", "ListarOpciones", ex);
                return null;
            }
        }

        public List<CamposPermiso> ConsultarCamposPerfil(CamposPermiso pPerfil, Usuario pUsuario)
        {
            try
            {
                return BOPerfil.ConsultarCamposPerfil(pPerfil, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilService", "ConsultarCamposPerfil", ex);
                return null;
            }
        }

        public bool CrearCamposPerfil(CamposPermiso cPerfil, Usuario pUsuario)
        {
            try
            {
                return BOPerfil.CrearCamposPerfil(cPerfil, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilService", "CrearCamposPerfil", ex);
                return false;
            }
        }

        public bool EliminarCamposPerfil(CamposPermiso cPerfil, Usuario pUsuario)
        {
            try
            {
                return BOPerfil.EliminarCamposPerfil(cPerfil, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilService", "EliminarCamposPerfil", ex);
                return false;
            }
        }



    }
}