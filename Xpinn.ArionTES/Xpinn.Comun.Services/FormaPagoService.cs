using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Comun.Entities;
using Xpinn.Comun.Business;
using System.Web;

namespace Xpinn.Comun.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class  FormaPagoService
    {
        private FormaPagoBusiness BOFormaPago;
        private FormaPagoBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para CierreHistorio
        /// </summary>
        public FormaPagoService()
        {
            BOFormaPago = new FormaPagoBusiness();
            BOExcepcion = new FormaPagoBusiness();
        }

        public List<FormaPago> ListarFormaPago(FormaPago pTipo, Usuario pUsuario)
        {
            try
            {
                return BOFormaPago.ListarFormaPago(pTipo, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        
    }
}
