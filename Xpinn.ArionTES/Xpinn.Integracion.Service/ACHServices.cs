using System;
using System.Collections.Generic;
using System.ServiceModel;
using Xpinn.Integracion.Business;
using Xpinn.Integracion.Entities;
using Xpinn.Util;

namespace Xpinn.Integracion.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ACHServices
    {
        private ACHBusiness BOPayment;
        private ExcepcionBusiness BOExcepcion;

        public ACHServices()
        {
            BOPayment = new ACHBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public ACHPayment CreatePaymentACHServices(ACHPayment pPaymentACH, Usuario vUsuario)
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

        public ACHPayment ModifyPaymentACHServices(ACHPayment pPaymentACH, Usuario vUsuario)
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

        public ACHPayment ConsultPaymentACHServices(string filtro, ref string pError, Usuario vUsuario)
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

        public List<ACHPayment> ListPaymentACHServices(string filtro, ref string pError, Usuario vUsuario)
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


        public bool UpdatePaymentsACH(long cod_persona, Usuario pUsuario)
        {
            try
            {
                return BOPayment.UpdatePaymentsACH(cod_persona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PaymentACHServices", "UpdatePaymentsACH", ex);
                return false;
            }
        }

        public string getCredencialesACH(Usuario pUsuario)
        {
            string _configuracion = BOPayment.getCredencialesACH(pUsuario);
            return _configuracion;
        }


    }
}
