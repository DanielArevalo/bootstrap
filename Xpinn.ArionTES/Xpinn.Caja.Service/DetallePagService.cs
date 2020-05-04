using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Caja.Business;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class DetallePagService
    {

        private DetallePagBusiness BODetallePago;
        private ExcepcionBusiness BOExcepcion;

        public DetallePagService()
        {
            BODetallePago = new DetallePagBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<DetallePagos> Listar(DateTime pFecha, Int64 pNumeroRadicacion, Usuario pUsuario)
        {
            try
            {
                return BODetallePago.Listar(pFecha, pNumeroRadicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DetallePagService", "Listar", ex);
                return null;
            }
        }

       

        public List<DetallePagos> DistribuirPago(Int64 pTipoProducto, Int64 pNumeroRadicacion, DateTime pFechaPago, decimal pValorPago, String pTipoPago, Usuario pUsuario)
        {
            try
            {
                return BODetallePago.DistribuirPago(pTipoProducto, pNumeroRadicacion, pFechaPago, pValorPago, pTipoPago, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DetallePagService", "Listar", ex);
                return null;
            }
        }


    }
}
