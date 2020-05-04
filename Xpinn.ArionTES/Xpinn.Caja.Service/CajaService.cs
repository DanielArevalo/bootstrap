using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Caja.Business;
using Xpinn.Caja.Entities;
using Xpinn.Util;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Xpinn.Caja.Services
{
    /// <summary>
    /// Servicio para Caja
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CajaService
    {
        private CajaBusiness BOCaja;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Caja
        /// </summary>
        public CajaService()
        {
            BOCaja = new CajaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public int CodigoCaja;
        public string CodigoPrograma { get { return "120205"; } }
        public string CodigoPrograma2 { get { return "120115"; } }

        /// <summary>
        /// Crea una Caja
        /// </summary>
        /// <param name="pEntity">Entidad Programa</param>
        /// <returns>Entidad creada</returns>
        public Caja.Entities.Caja CrearCaja(Caja.Entities.Caja pCaja, GridView gvTopes, GridView gvOperaciones, Usuario pUsuario)
        {
            try
            {
                return BOCaja.CrearCaja(pCaja, gvTopes, gvOperaciones, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaService", "CrearCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica una Caja
        /// </summary>
        /// <param name="pEntity">Entidad Caja</param>
        /// <returns>Entidad modificada</returns>
        public Caja.Entities.Caja ModificarCaja(Caja.Entities.Caja pCaja,GridView gvTopes, GridView gvOperaciones, Usuario pUsuario)
        {
            try
            {
                return BOCaja.ModificarCaja(pCaja, gvTopes , gvOperaciones, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaService", "ModificarCaja", ex);
                return null;
            }

        }

        /// <summary>
        /// Elimina una Caja
        /// </summary>
        /// <param name="pId">identificador de la Caja</param>
        public void EliminarCaja(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOCaja.EliminarCaja(pId,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaService", "EliminarCaja", ex);
            }
        }

        /// <summary>
        /// Obtiene una Caja
        /// </summary>
        /// <param name="pId">identificador de Caja</param>
        /// <returns>Caja consultada</returns>
        public Caja.Entities.Caja ConsultarCaja(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOCaja.ConsultarCaja(pId,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaService", "ConsultarCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene una Caja
        /// </summary>
        /// <param name="pId">identificador de Caja</param>
        /// <returns>Caja consultada</returns>
        public Caja.Entities.Caja ConsultarCajaPrincipal(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOCaja.ConsultarCajaPrincipal(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaService", "ConsultarCajaPrincipal", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Cajas dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Cajas obtenidos</returns>
        public List<Caja.Entities.Caja> ListarCaja(Caja.Entities.Caja pCaja, Usuario pUsuario)
        {
            try
            {
                return BOCaja.ListarCaja(pCaja,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaService", "ListarCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Cajas dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Cajas obtenidos</returns>
        public List<Caja.Entities.Caja> ListarComboCaja(Caja.Entities.Caja pCaja, Usuario pUsuario)
        {
            try
            {
                return BOCaja.ListarComboCaja(pCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaService", "ListarComboCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Cajas dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Cajas obtenidos</returns>
        public List<Caja.Entities.Caja> ListarComboCajaXOficina(Caja.Entities.Caja pCaja, Usuario pUsuario)
        {
            try
            {
                return BOCaja.ListarComboCajaXOficina(pCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaService", "ListarComboCajaXOficinayaja", ex);
                return null;
            }
        }

        public List<Caja.Entities.Caja> ListarComboCajaXOficinayCaja(Caja.Entities.Caja pCaja, Usuario pUsuario)
        {
            try
            {
                return BOCaja.ListarComboCajaXOficinaycaja(pCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaService", "ListarComboCajaXOficinayCaja", ex);
                return null;
            }
        }




        /// <summary>
        /// Obtiene la lista de Cajas dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Cajas obtenidos</returns>
        public List<Caja.Entities.Caja> ListarComboCajaXOficinaActiva(Caja.Entities.Caja pCaja, Usuario pUsuario)
        {
            try
            {
                return BOCaja.ListarComboCajaXOficinaActiva(pCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaService", "ListarComboCajaXOficinaActiva", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Cajas dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Cajas obtenidos por todas las oficinas</returns>
        public List<Caja.Entities.Caja> ListarCajaAllOficinas(Caja.Entities.Caja pCaja, Usuario pUsuario)
        {
            try
            {
                return BOCaja.ListarCajaAllOficinas(pCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaService", "ListarCajaAllOficinas", ex);
                return null;
            }
        }





        /// <summary>
        /// Obtiene la lista de Cajas que tenga datafono
        /// </summary>
        /// <returns>Conjunto de Cajas obtenidos</returns>
        public List<Caja.Entities.Caja> ListarComboCajaXDatafono(Usuario pUsuario)
        {
            try
            {
                return BOCaja.ListarComboCajaXDatafono(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaService", "ListarComboCajaXDatafono", ex);
                return null;
            }
        }

    }
}
