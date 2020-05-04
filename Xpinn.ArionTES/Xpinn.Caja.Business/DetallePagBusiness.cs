using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Caja.Data;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Business
{
    public class DetallePagBusiness : GlobalBusiness
    {
        private DetallePagData DADetallePago;
        private List<DetallePagos> DetallePago = new List<DetallePagos>();

        public DetallePagBusiness()
        {
            DADetallePago = new DetallePagData();
        }

        public List<DetallePagos> Listar(DateTime pFecha, Int64 pNumeroRadicacion, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    DetallePago = DADetallePago.Listar(pFecha, pNumeroRadicacion, pUsuario);

                    ts.Complete();
                }

                return DetallePago;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DetallePagoBusiness", "Listar", ex);
                return null;
            }
        }

        public List<DetallePagos> DistribuirPago(Int64 pTipoProducto, Int64 pNumeroRadicacion, DateTime pFechaPago, decimal pValorPago, String pTipoPago, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    DetallePago = DADetallePago.DistribuirPago(pTipoProducto, pNumeroRadicacion, pFechaPago, pValorPago, pTipoPago, pUsuario);

                    ts.Complete();
                }

                return DetallePago;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DetallePagoBusiness", "Listar", ex);
                return null;
            }
        }

    }
}
