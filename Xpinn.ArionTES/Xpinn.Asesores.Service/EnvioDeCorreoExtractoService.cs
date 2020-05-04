using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;
using Xpinn.Util;
using System.Data;

namespace Xpinn.Asesores.Services
{
    /// <summary>
    /// Listado de clientes generado para la generación de extracto
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class EnvioDeCorreoExtractoService
    {
        private EnvioDeCorreoExtractoBusiness BOEnvioDeCorreoExtracto;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Caja
        /// </summary>
        public EnvioDeCorreoExtractoService()
        {
            BOEnvioDeCorreoExtracto = new EnvioDeCorreoExtractoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<EnvioDeCorreoExtracto> ListarEnvioDeCorreoExtractos(EnvioDeCorreoExtracto EnvioDeCorreoExtracto, Usuario pUsuario)
        {
            try
            {
                return BOEnvioDeCorreoExtracto.ListarEnvioDeCorreoExtractos(EnvioDeCorreoExtracto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EnvioDeCorreoExtractoService", "ListarEnvioDeCorreoExtractos", ex);
                return null;
            }
        }
    }
}
