using System;
using System.Collections.Generic;
using System.Transactions;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;
using Xpinn.Util;

namespace Xpinn.Tesoreria.Business
{
    public class PaymentACHBusiness : GlobalBusiness
    {
        private PaymentACHData DAPayment;
        public PaymentACHBusiness()
        {
            DAPayment = new PaymentACHData();
        }

        public PaymentACH CreatePaymentACHBusiness(PaymentACH pPaymentACH, Usuario pUsuario)
        {
            PaymentACH pResult = null;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
            {
                try
                {
                    string pError = string.Empty;
                    // CREANDO LA TRANSACCION
                    pResult = DAPayment.CreateModifyPaymentACH(pPaymentACH, ref pError, OperacionCRUD.Crear, pUsuario);

                    if (!string.IsNullOrEmpty(pError))
                    {
                        pResult = new PaymentACH();
                        pResult.Success = false;
                        pResult.ErrorMessage = pError;
                    }
                    else
                    {
                        if (pResult != null)
                        {
                            if (pResult.ID == 0)
                            {
                                pResult = new PaymentACH();
                                pResult.Success = false;
                                pResult.ErrorMessage = "SE GENERÓ UN ERROR AL CREAR LA TRANSACCIÓN";
                            }
                            else
                                pResult.Success = true;
                        }
                    }

                    ts.Complete();
                }
                catch (Exception ex)
                {
                    pResult = new PaymentACH();
                    pResult.Success = false;
                    pResult.ErrorMessage = ex.Message;
                    ts.Dispose();
                }
            }
            return pResult;
        }


        public PaymentACH ModifyPaymentACHBusiness(PaymentACH pPaymentACH, Usuario pUsuario)
        {
            PaymentACH pResult = null;
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
            {
                try
                {
                    string pError = string.Empty;
                    
                    // MODIFY PAYMENT
                    pResult = DAPayment.CreateModifyPaymentACH(pPaymentACH, ref pError, OperacionCRUD.Modificar, pUsuario);
                    
                    if (!string.IsNullOrEmpty(pError))
                    {
                        pResult = new PaymentACH();
                        pResult.Success = false;
                        pResult.ErrorMessage = pError;
                    }
                    else
                    {
                        if (pResult != null)
                        {
                            if (pResult.ID == 0)
                            {
                                pResult = new PaymentACH();
                                pResult.Success = false;
                                pResult.ErrorMessage = "SE GENERÓ UN ERROR AL CREAR LA TRANSACCIÓN";
                            }
                            else
                                pResult.Success = true;
                        }
                    }

                    ts.Complete();
                }
                catch (Exception ex)
                {
                    pResult = new PaymentACH();
                    pResult.Success = false;
                    pResult.ErrorMessage = ex.Message;
                    ts.Dispose();
                }
            }
            return pResult;
        }

        public PaymentACH ConsultPaymentACHBusiness(string filtro, ref string pError, Usuario vUsuario)
        {
            try
            {
                return DAPayment.ConsultPaymentACH(filtro, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PaymentACHBusiness", "ConsultPaymentACHBusiness", ex);
                return null;
            }
        }

        public List<PaymentACH> ListPaymentACHBusiness(string filtro, ref string pError, Usuario vUsuario)
        {
            try
            {
                return DAPayment.ListPaymentACH(filtro, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PaymentACHBusiness", "ListPaymentACHBusiness", ex);
                return null;
            }
        }


    }
}
