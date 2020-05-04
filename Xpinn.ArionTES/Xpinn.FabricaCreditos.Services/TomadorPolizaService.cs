using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;
using System.ServiceModel;
using System.Data;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicio para Familiares
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TomadorPolizaService
    {
        private  TomadorPolizaBusiness BOTomadorPoliza;
        private ExcepcionBusiness BOExcepcion;

        public int identificacion;
        /// <summary>
        /// Constructor del servicio para  Familiares
        /// </summary>
        public TomadorPolizaService()
        {
            BOTomadorPoliza = new TomadorPolizaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        /// <summary>
        /// ObtieneRegistro dfel Tomador de la Poliza
        /// </summary>
        /// <param name="pId">identificador de TomadorPoliza</param>
        /// <returns>TomadorPoliza consultada</returns>
        public TomadorPoliza ConsultarTomadorPolizas( Usuario pUsuario)
        {
            try
            {
                return BOTomadorPoliza.ConsultarTomadorPoliza(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TomadorPolizaService", "ConsultarTomadorPoliza", ex);
                return null;
            }
        }
       
    }
}