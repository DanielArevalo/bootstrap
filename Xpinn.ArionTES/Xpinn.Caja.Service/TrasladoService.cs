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
    /// Servicio para Traslado
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TrasladoService
    {
        private TrasladoBusiness BOTraslado;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del Servicio para Traslado
        /// </summary>
        public TrasladoService()
        {
            BOTraslado = new TrasladoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }
        
        public string CodigoPrograma { get { return "120102"; } }

        /// <summary>
        /// Crea un Traslado
        /// </summary>
        /// <param name="pEntity">Entidad Traslado</param>
        /// <returns>Entidad creada</returns>
        public Traslado CrearTraslado(Traslado pTraslado, Usuario pUsuario)
        {
            try
            {
                return BOTraslado.CrearTraslado(pTraslado, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TrasladoService", "CrearTraslado", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un Traslado
        /// </summary>
        /// <param name="pUsuario">identificador del Usuario</param>
        /// <returns>Reitegro consultada</returns>
        public Traslado ConsultarCajero(Usuario pUsuario)
        {
            try
            {
                return BOTraslado.ConsultarCajero(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TrasladoService", "ConsultarCajero", ex);
                return null;
            }
        }

        public void EliminarTraslado(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOTraslado.EliminarTraslado(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobadorService", "EliminarTraslado", ex);
            }        
        }
        /// <summary>
        /// Obtiene un Traslado
        /// </summary>
        /// <param name="pUsuario">identificador del Usuario</param>
        /// <returns>Reitegro consultada</returns>
        public Cajero ConsultarCajaXCajero(Cajero pEntidad, Usuario pUsuario)
        {
            try
            {
                return BOTraslado.ConsultarCajaXCajero(pEntidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TrasladoService", "ConsultarTraslado", ex);
                return null;
            }
        }
    }
}
