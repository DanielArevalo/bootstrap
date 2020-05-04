using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Caja.Business;
using Xpinn.Caja.Entities;
using Xpinn.Util;
using System.Web;
using System.Web.UI.WebControls;

namespace Xpinn.Caja.Services
{
    /// <summary>
    /// Servicio para Recepcion
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class RecepcionService
    {
        private RecepcionBusiness BORecepcion;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del Servicio para Recepcion
        /// </summary>
        public RecepcionService()
        {
            BORecepcion = new RecepcionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }
        
        public string CodigoPrograma { get { return "120103"; } }

        /// <summary>
        /// Crea un Recepcion
        /// </summary>
        /// <param name="pEntity">Entidad Recepcion</param>
        /// <returns>Entidad creada</returns>
        public Recepcion CrearRecepcion(Recepcion pRecepcion, GridView gvTraslados,  Usuario pUsuario)
        {
            try
            {
                return BORecepcion.CrearRecepcion(pRecepcion, gvTraslados, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecepcionService", "CrearRecepcion", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un Recepcion
        /// </summary>
        /// <param name="pUsuario">identificador del Usuario</param>
        /// <returns>Reitegro consultada</returns>
        public Recepcion ConsultarCajero(Usuario pUsuario)
        {
            try
            {
                return BORecepcion.ConsultarCajero(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecepcionService", "ConsultarRecepcion", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Oficinas dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Oficinas obtenidos</returns>
        public List<Traslado> ListarTraslado(Recepcion pRecepcion, Usuario pUsuario)
        {
            try
            {
                return BORecepcion.ListarTraslado(pRecepcion, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecepcionService", "ListarTraslado", ex);
                return null;
            }
        }

        /// <summary>
        /// Consulta un traslado segun la caja de origen/destino
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Traslado obtenido</returns>
        public Traslado ConsultarTraslado(Recepcion pRecepcion, Usuario pUsuario)
        {
            try
            {
                return BORecepcion.ConsultarTraslado(pRecepcion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecepcionService", "ListarTraslado", ex);
                return null;
            }
        }
    }
}
