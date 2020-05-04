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
    public class AccesoService
    {
        private AccesoBusiness BOAcceso;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Acceso
        /// </summary>
        public AccesoService()
        {
            BOAcceso = new AccesoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "90105"; } }

        /// <summary>
        /// Servicio para crear Acceso
        /// </summary>
        /// <param name="pEntity">Entidad Acceso</param>
        /// <returns>Entidad Acceso creada</returns>
        public Acceso CrearAcceso(Acceso pAcceso, Usuario pUsuario)
        {
            try
            {
                return BOAcceso.CrearAcceso(pAcceso, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AccesoService", "CrearAcceso", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Acceso
        /// </summary>
        /// <param name="pAcceso">Entidad Acceso</param>
        /// <returns>Entidad Acceso modificada</returns>
        public Acceso ModificarAcceso(Acceso pAcceso, Usuario pUsuario)
        {
            try
            {
                return BOAcceso.ModificarAcceso(pAcceso, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AccesoService", "ModificarAcceso", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Acceso
        /// </summary>
        /// <param name="pId">identificador de Acceso</param>
        public void EliminarAcceso(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOAcceso.EliminarAcceso(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarAcceso", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Acceso
        /// </summary>
        /// <param name="pId">identificador de Acceso</param>
        /// <returns>Entidad Acceso</returns>
        public Acceso ConsultarAcceso(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOAcceso.ConsultarAcceso(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AccesoService", "ConsultarAcceso", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Accesos a partir de unos filtros
        /// </summary>
        /// <param name="pAcceso">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Acceso obtenidos</returns>
        public List<Acceso> ListarAcceso(Acceso pAcceso, Usuario pUsuario)
        {
            try
            {
                return BOAcceso.ListarAcceso(pAcceso, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AccesoService", "ListarAcceso", ex);
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
                return BOAcceso.ListarAcceso(pIdPerfil, pUsuario,Idioma);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AccesoService", "ListarAcceso", ex);
                return null;
            }
        }

        public List<Acceso> ListarAccesoAAC(Int64 pIdModulo, Usuario pUsuario)
        { 
            try
            {
                return BOAcceso.ListarAccesoAAC(pIdModulo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AccesoService", "ListarAccesoAAC", ex);
                return null;
            }
        }

        public List<Acceso> ListarAccesoApp(Usuario pUsuario)
        {
            try
            {
                return BOAcceso.ListarAccesoApp(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AccesoService", "ListarAccesoApp", ex);
                return null;
            }
        }
    }
}