using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class DetallePagoService
    {

        private DetallePagoBusiness BODetallePago;
        private ExcepcionBusiness BOExcepcion;

        public DetallePagoService()
        {
            BODetallePago = new DetallePagoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<DetallePago> Listar(DateTime pFecha, Int64 pNumeroRadicacion, Usuario pUsuario)
        {
            try
            {
                return BODetallePago.Listar(pFecha, pNumeroRadicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DetallePagoServices", "Listar", ex);
                return null;
            }
        }

        public List<DetallePago> DistribuirPago(Int64 pNumeroRadicacion, DateTime pFechaPago, Int64 pValorPago, String pTipoPago, Usuario pUsuario)
        {
            try
            {
                return BODetallePago.DistribuirPago( pNumeroRadicacion, pFechaPago, pValorPago, pTipoPago, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DetallePagoServices", "Listar", ex);
                return null;
            }
        }


        public List<Atributo> ListarDetallePago(DateTime pFecha, Int64 pNumeroRadicacion, Usuario pUsuario)
        {
            try
            {
                return BODetallePago.ListarDetallePago(pFecha, pNumeroRadicacion, pUsuario);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
