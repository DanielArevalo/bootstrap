using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Contabilidad.Business;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TipoMonedaService
    {
        private TipoMonedaBusiness BOTipoMoneda;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para TipoMoneda
        /// </summary>
        public TipoMonedaService()
        {
            BOTipoMoneda = new TipoMonedaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30207"; } }



        public List<TipoMoneda> ListarTipoMoneda( Usuario pUsuario)
        {
            try
            {
                return BOTipoMoneda.ListarTipoMoneda(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoMonedaService", "ListarTipoMoneda", ex);
                return null;
            }
        }

        
    }
}