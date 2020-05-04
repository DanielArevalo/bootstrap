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
    public class ComprobantePagoExtractoService
    {
        private ComprobantePagoExtractoBusiness BOComprobantePagoExtracto;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Caja
        /// </summary>
        public ComprobantePagoExtractoService()
        {
            BOComprobantePagoExtracto = new ComprobantePagoExtractoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<ComprobantePagoExtracto> ListarComprobantePagoExtractos(ComprobantePagoExtracto ComprobantePagoExtracto, Usuario pUsuario)
        {
            try
            {
                return BOComprobantePagoExtracto.ListarComprobantePagoExtractos(ComprobantePagoExtracto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobantePagoExtractoService", "ListarComprobantePagoExtractos", ex);
                return null;
            }
        }
    }
}