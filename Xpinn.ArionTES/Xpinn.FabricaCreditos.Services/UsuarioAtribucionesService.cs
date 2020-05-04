using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class UsuarioAtribucionesService
    {
        private UsuarioAtribucionesBusiness BOUsuarioAtribuciones;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para UsuarioAtribuciones
        /// </summary>
        public UsuarioAtribucionesService()
        {
            BOUsuarioAtribuciones = new UsuarioAtribucionesBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100136"; } }

        /// <summary>
        /// Servicio para crear UsuarioAtribuciones
        /// </summary>
        /// <param name="pEntity">Entidad UsuarioAtribuciones</param>
        /// <returns>Entidad UsuarioAtribuciones creada</returns>
        public UsuarioAtribuciones CrearUsuarioAtribuciones(UsuarioAtribuciones pUsuarioAtribuciones, Usuario pUsuario)
        {
            try
            {
                return BOUsuarioAtribuciones.CrearUsuarioAtribuciones(pUsuarioAtribuciones, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioAtribucionesService", "CrearUsuarioAtribuciones", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar UsuarioAtribuciones
        /// </summary>
        /// <param name="pUsuarioAtribuciones">Entidad UsuarioAtribuciones</param>
        /// <returns>Entidad UsuarioAtribuciones modificada</returns>
        public UsuarioAtribuciones ModificarUsuarioAtribuciones(UsuarioAtribuciones pUsuarioAtribuciones, Usuario pUsuario)
        {
            try
            {
                return BOUsuarioAtribuciones.ModificarUsuarioAtribuciones(pUsuarioAtribuciones, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioAtribucionesService", "ModificarUsuarioAtribuciones", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar UsuarioAtribuciones
        /// </summary>
        /// <param name="pId">identificador de UsuarioAtribuciones</param>
        public void EliminarUsuarioAtribuciones(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOUsuarioAtribuciones.EliminarUsuarioAtribuciones(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarUsuarioAtribuciones", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener UsuarioAtribuciones
        /// </summary>
        /// <param name="pId">identificador de UsuarioAtribuciones</param>
        /// <returns>Entidad UsuarioAtribuciones</returns>
        public UsuarioAtribuciones ConsultarUsuarioAtribuciones(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOUsuarioAtribuciones.ConsultarUsuarioAtribuciones(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioAtribucionesService", "ConsultarUsuarioAtribuciones", ex);
                return null;
            }
        }
        
        /// <summary>
        /// Método para saber si un usuario tiene una atribuciòn especifica
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public UsuarioAtribuciones ConsultarUsuarioAtribuciones(Int64 pId, Int64 pTip, Usuario pUsuario)
        {
            try
            {
                return BOUsuarioAtribuciones.ConsultarUsuarioAtribuciones(pId, pTip, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioAtribucionesService", "ConsultarUsuarioAtribuciones", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para obtener lista de UsuarioAtribucioness a partir de unos filtros
        /// </summary>
        /// <param name="pUsuarioAtribuciones">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de UsuarioAtribuciones obtenidos</returns>
        public List<UsuarioAtribuciones> ListarUsuarioAtribuciones(UsuarioAtribuciones pUsuarioAtribuciones, Usuario pUsuario)
        {
            try
            {
                return BOUsuarioAtribuciones.ListarUsuarioAtribuciones(pUsuarioAtribuciones, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioAtribucionesService", "ListarUsuarioAtribuciones", ex);
                return null;
            }
        }



        /// <summary>
        /// Obtiene  listas desplegables
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de elementos obtenidos</returns>
        public List<UsuarioAtribuciones> ListasDesplegables(String ListaSolicitada, Usuario pUsuario)
        {
            try
            {
                return BOUsuarioAtribuciones.ListasDesplegables(ListaSolicitada, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosClienteService", "ListarListasMenu", ex);
                return null;
            }
        }

        
    }
}