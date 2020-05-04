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
    /// Servicio para Reintegro
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ReintegroService
    {
        private ReintegroBusiness BOReintegro;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del Servicio para Reintegro
        /// </summary>
        public ReintegroService()
        {
            BOReintegro = new ReintegroBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }
        
        public string CodigoPrograma { get { return "120101"; } }

        /// <summary>
        /// Crea un Reintegro
        /// </summary>
        /// <param name="pEntity">Entidad Reintegro</param>
        /// <returns>Entidad creada</returns>
        public Reintegro CrearReintegro(Reintegro pReintegro, Usuario pUsuario)
        {
            try
            {
                return BOReintegro.CrearReintegro(pReintegro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReintegroService", "CrearReintegro", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un Reintegro
        /// </summary>
        /// <param name="pUsuario">identificador del Usuario</param>
        /// <returns>Reitegro consultada</returns>
        public Reintegro ConsultarCajero(Usuario pUsuario)
        {
            try
            {
                return BOReintegro.ConsultarCajero(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReintegroService", "ConsultarReintegro", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene un Reintegro
        /// </summary>
        /// <param name="pUsuario">identificador del Usuario</param>
        /// <returns>Reitegro consultada</returns>
        public Reintegro ConsultarFecUltCierre(Usuario pUsuario)
        {
            try
            {
                return BOReintegro.ConsultarFecUltCierre(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReintegroService", "ConsultarFecUltCierre", ex);
                return null;
            }
        }

    }
}
