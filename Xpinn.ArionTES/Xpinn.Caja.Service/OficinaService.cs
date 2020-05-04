using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Caja.Business;
using Xpinn.Caja.Entities;
using Xpinn.Util;

namespace Xpinn.Caja.Services
{
    /// <summary>
    /// Servicio para oficina
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class OficinaService
    {
        private OficinaBusiness BOOficina;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del objeto de negocio para Oficina
        /// </summary>
        public OficinaService()
        {
            BOOficina = new OficinaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public int CodigoOficina;
        public string CodigoPrograma { get { return "120201"; } }

        /// <summary>
        /// Crea una Oficina
        /// </summary>
        /// <param name="pEntity">Entidad oficina</param>
        /// <returns>Entidad creada</returns>
        public Oficina CrearOficina(Oficina pOficina, Usuario pUsuario)
        {
            try
            {
                return BOOficina.CrearOficina(pOficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OficinaService", "CrearOficina", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica una Oficina
        /// </summary>
        /// <param name="pEntity">Entidad Oficina</param>
        /// <returns>Entidad modificada</returns>
        public Oficina ModificarOficina(Oficina pOficina, Usuario pUsuario)
        {
            try
            {
                return BOOficina.ModificarOficina(pOficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OficinaService", "ModificarOficina", ex);
                return null;
            }

        }

        /// <summary>
        /// Elimina una Oficina
        /// </summary>
        /// <param name="pId">identificador de la oficina</param>
        public void EliminarOficina(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOOficina.EliminarOficina(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OficinaService", "EliminarOficina", ex);
            }
        }

        /// <summary>
        /// Obtiene una Oficina
        /// </summary>
        /// <param name="pId">identificador de la Oficina</param>
        /// <returns>Caja consultada</returns>
        public Oficina ConsultarOficina(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOOficina.ConsultarOficina(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OficinaService", "ConsultarOficina", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene una Oficina por parametro de consulta
        /// </summary>
        /// <param name="pId">identificador de la Oficina</param>
        /// <returns>Oficina consultada</returns>
        public Oficina ConsultarXOficina(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOOficina.ConsultarXOficina(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OficinaService", "ConsultarXOficina", ex);
                return null;
            }
        }
        

        /// <summary>
        /// Obtiene la lista de Oficinas dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Oficinas obtenidos</returns>
        public List<Oficina> ListarOficina(Oficina pOficina, Usuario pUsuario)
        {
            try
            {
                return BOOficina.ListarOficina(pOficina, pUsuario); 
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OficinaService", "ListarOficina", ex);
                return null;
            }
        }

        public List<Oficina> ListarOficina(Oficina pOficina, Usuario pUsuario, string filtro)
        {
            try
            {
                return BOOficina.ListarOficina(pOficina, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OficinaService", "ListarOficina", ex);
                return null;
            }
        }

        public Oficina ConsultarDireccionYCiudadDeUnaOficina(long codigoOficina, Usuario usuario)
        {
            try
            {
                return BOOficina.ConsultarDireccionYCiudadDeUnaOficina(codigoOficina, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OficinaService", "ConsultarDireccionYCiudadDeUnaOficina", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Oficinas dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Oficinas obtenidos</returns>
        public List<Oficina> ListarOficinaUsuario(Oficina pOficina, Usuario pUsuario)
        {
            try
            {
                return BOOficina.ListarOficinaUsuario(pOficina, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OficinaService", "ListarOficinaUsuario", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene el conteo de usuarios asociados a la oficina especifica 
        /// </summary>
        /// <param name="pId">identificador de la Oficina</param>
        /// <returns>Oficina consultada</returns>
        public Oficina ConsultarUsersXOficina(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOOficina.ConsultarUsersXOficina(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OficinaService", "ConsultarUsersXOficina", ex);
                return null;
            }
        }
        


    }
}
