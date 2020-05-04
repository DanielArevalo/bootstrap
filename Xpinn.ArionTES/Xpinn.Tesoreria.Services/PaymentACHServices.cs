using System;
using System.Collections.Generic;
using System.ServiceModel;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;
using Xpinn.Util;

namespace Xpinn.Tesoreria.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class PaymentACHServices
    {
        private PaymentACHBusiness BOPayment;
        private ExcepcionBusiness BOExcepcion;

        public PaymentACHServices()
        {
            BOPayment = new PaymentACHBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public PaymentACH CreatePaymentACHServices(PaymentACH pPaymentACH, Usuario vUsuario)
        {
            try
            {
                return BOPayment.CreatePaymentACHBusiness(pPaymentACH, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PaymentACHServices", "CreatePaymentACHServices", ex);
                return null;
            }
        }

        public PaymentACH ModifyPaymentACHServices(PaymentACH pPaymentACH, Usuario vUsuario)
        {
            try
            {
                return BOPayment.ModifyPaymentACHBusiness(pPaymentACH, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PaymentACHServices", "CreatePaymentACHServices", ex);
                return null;
            }
        }

        public PaymentACH ConsultPaymentACHServices(string filtro, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOPayment.ConsultPaymentACHBusiness(filtro, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PaymentACHServices", "ConsultPaymentACHBusiness", ex);
                return null;
            }
        }

        public List<PaymentACH> ListPaymentACHServices(string filtro, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOPayment.ListPaymentACHBusiness(filtro, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PaymentACHServices", "ListPaymentACHServices", ex);
                return null;
            }
        }

    }
}
