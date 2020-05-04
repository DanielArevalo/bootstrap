using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Caja.Business;
using Xpinn.Caja.Entities;
using Xpinn.Util;
using System.Web.UI.WebControls;

namespace Xpinn.Caja.Services
{
    /// <summary>
    /// Servicio para Cajero
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CajeroService
    {
        private CajeroBusiness BOCajero;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Cajero
        /// </summary>
        public CajeroService()
        {
            BOCajero = new CajeroBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public int CodigoCajero;
        public string CodigoPrograma { get { return "120203"; } }

        /// <summary>
        /// Crea un Cajero
        /// </summary>
        /// <param name="pEntity">Entidad Cajero</param>
        /// <returns>Entidad creada</returns>
        public Cajero CrearCajero(Cajero pCaja, Usuario pUsuario)
        {
            try
            {
                return BOCajero.CrearCajero(pCaja,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroService", "CrearCajero", ex);
                return null;
            }
        }

        /// <summary>
        /// Crea una Caja
        /// </summary>
        /// <param name="pEntity">Entidad Programa</param>
        /// <returns>Entidad creada</returns>
        public Caja.Entities.Cajero CrearCajeroMass(Caja.Entities.Cajero pCajero, GridView gvCajeros, Usuario pUsuario)
        {
            try
            {
                return BOCajero.CrearCajeroMass(pCajero, gvCajeros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroService", "CrearCajeroMass", ex);
                return null;
            }
        }


        /// <summary>
        /// Modifica un Cajero
        /// </summary>
        /// <param name="pEntity">Entidad Cajero</param>
        /// <returns>Entidad modificada</returns>
        public Cajero ModificarCajero(Cajero pCaja, Usuario pUsuario)
        {
            try
            {
                return BOCajero.ModificarCajero(pCaja,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroService", "ModificarCajero", ex);
                return null;
            }

        }


        /// <summary>
        /// Elimina un Cajero
        /// </summary>
        /// <param name="pId">identificador del Cajero</param>
        public void EliminarCajero(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOCajero.EliminarCajero(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroService", "EliminarCajero", ex);
            }
        }

        /// <summary>
        /// Obtiene un Cajero
        /// </summary>
        /// <param name="pId">identificador del Cajero</param>
        /// <returns>Caja consultada</returns>
        public Cajero ConsultarCajero(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOCajero.ConsultarCajero(pId,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroService", "ConsultarCajero", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Cajeros dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Cajeros obtenidos</returns>
        public List<Cajero> ListarTCajero(Cajero pCaja, Usuario pUsuario)
        {
            try
            {
                return BOCajero.ListarTCajero(pCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroService", "ListarTCajero", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Cajeros dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Cajeros obtenidos</returns>
        public List<Cajero> ListarCajeroXCaja(Cajero pCaja, Usuario pUsuario)
        {
            try
            {
                return BOCajero.ListarCajeroXCaja(pCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroService", "ListarCajeroXCaja", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de Cajeros dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Cajeros obtenidos</returns>
        public List<Cajero> ListarCajero(Cajero pCaja, Usuario pUsuario)
        {
            try
            {
                return BOCajero.ListarCajero(pCaja,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroService", "ListarCajero", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene un ConsultarCajeroXCaja
        /// </summary>
        /// <param name="pId">identificador del ConsultarCajeroXCaja</param>
        /// <returns>ConsultarCajeroXCaja consultada</returns>
        public Cajero ConsultarCajeroXCaja(Int64 pId, Int64 pCaja, Usuario pUsuario)
        {
            try
            {
                return BOCajero.ConsultarCajeroXCaja(pId, pCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroService", "ConsultarCajeroXCaja", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene una Caja
        /// </summary>
        /// <param name="pId">identificador de Caja</param>
        /// <returns>Caja consultada</returns>
        public Caja.Entities.Cajero ConsultarCajeroRelCaja(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOCajero.ConsultarCajeroRelCaja(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroService", "ConsultarCajeroRelCaja", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene un Cajero Principal
        /// </summary>
        /// <param name="pId">identificador de Caja</param>
        /// <returns>Caja consultada</returns>
        public Caja.Entities.Cajero ConsultarCajeroPrincipal(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOCajero.ConsultarCajeroPrincipal(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroService", "ConsultarCajeroPrincipal", ex);
                return null;
            }
        }


        public Caja.Entities.Cajero ConsultarCajeroPrincipalAsignadoAlCajero(Int64 cod_oficina, Int64 cod_cajero, Usuario pUsuario)
        {
            try
            {
                return BOCajero.ConsultarCajeroPrincipalAsignadoAlCajero(cod_oficina, cod_cajero, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroService", "ConsultarCajeroPrincipalAsignadoAlCajero", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene un Cajero Principal
        /// </summary>
        /// <param name="pId">identificador de Caja</param>
        /// <returns>Caja consultada</returns>
        public Caja.Entities.Cajero ConsultarIfUserIsntCajeroPrincipal(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOCajero.ConsultarIfUserIsntCajeroPrincipal(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroService", "ConsultarIfUserIsntCajeroPrincipal", ex);
                return null;
            }
        }


        public Caja.Entities.Reintegro ConsultarFecha(Usuario pUsuario)
        {
            try
            {
                return BOCajero.ConsultarFecha(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroService", "ConsultarFecha", ex);
                return null;
            }
        }


        public List<Cajero> ListarCajeroPorOficina(Cajero pCaja, Usuario pUsuario)
        {
            try
            {
                return BOCajero.ListarCajeroPorOficina(pCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroService", "ListarCajeroPorOficina", ex);
                return null;
            }
        }
    }
}
