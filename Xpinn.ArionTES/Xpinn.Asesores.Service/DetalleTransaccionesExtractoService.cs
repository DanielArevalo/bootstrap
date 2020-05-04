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

    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class DetalleTransaccionesExtractoService
    {
        private DetalleTransaccionesExtractoBusiness BODetalleTransaccionesExtracto;
        private ExcepcionBusiness BOExcepcion;

        
        public DetalleTransaccionesExtractoService()
        {
            BODetalleTransaccionesExtracto = new DetalleTransaccionesExtractoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }


        public List<DetalleTransaccionesExtracto> ListarDetalleTransaccionesExtractos(DetalleTransaccionesExtracto DetalleTransaccionesExtracto, Usuario pUsuario)
        {
            try
            {
                return BODetalleTransaccionesExtracto.ListarDetalleTransaccionesExtractos(DetalleTransaccionesExtracto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DetalleTransaccionesExtractoService", "ListarDetalleTransaccionesExtractos", ex);
                return null;
            }
        }
    }
}