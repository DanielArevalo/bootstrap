using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Asesores.Data;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Business
{
    public class DetallePagoBusiness : GlobalBusiness
    {
        private DetallePagoData DADetallePago;
        private List<DetallePago> DetallePago = new List<DetallePago>();

        public DetallePagoBusiness()
        {
            DADetallePago = new DetallePagoData();
        }

        public List<DetallePago> Listar(DateTime pFecha, Int64 pNumeroRadicacion, Usuario pUsuario)
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

        public List<DetallePago> DistribuirPago(Int64 pNumeroRadicacion, DateTime pFechaPago, Int64 pValorPago, String pTipoPago, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    DetallePago = DADetallePago.DistribuirPago(pNumeroRadicacion, pFechaPago, pValorPago, pTipoPago, pUsuario);

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

        public List<Atributo> ListarDetallePago(DateTime pFecha, Int64 pNumeroRadicacion, Usuario pUsuario)
        {
            List<Atributo> lstAtributo = new List<Atributo>();
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    lstAtributo = DADetallePago.ListarDetallePago(pFecha, pNumeroRadicacion, pUsuario);
                    ts.Complete();
                }
                return lstAtributo;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}
