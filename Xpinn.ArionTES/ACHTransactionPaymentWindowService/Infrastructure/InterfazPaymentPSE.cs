using System;
using Xpinn.Util.PaymentACH;

namespace ACHTransactionPaymentWindowService.Infrastructure
{
    public class InterfazPaymentPSE
    {
        xpinnWSPayment.WSPaymentSoapClient PaymentService;
        public InterfazPaymentPSE()
        {
            PaymentService = new xpinnWSPayment.WSPaymentSoapClient();
        }


        public void GenerateProcessConsult()
        {
            // RECORRER TODOS LOS PAGOS EN PENDIENTE
            try
            {
                string pFilter = " WHERE STATE IN (0, 1) ORDER BY ID_PAYMENT";
                xpinnWSPayment.PaymentACHResult pResult = PaymentService.ListPaymentTransaction(pFilter);
                if (pResult == null)
                {
                    LogPaymentAction.Grabar(
                    new LogPayment(AppConstants.WindowServicesName, AppConstants.NameApplication, "CONSULTA DE LISTADO PROCESOS PENDIENTES", "NO SE OBTUVO NINGUN RESULTADO DE LA CONSULTA"),
                    AppConstants.UrlLogApplication);
                }
                if (!pResult.Success)
                {
                    LogPaymentAction.Grabar(
                    new LogPayment(AppConstants.WindowServicesName, AppConstants.NameApplication, "CONSULTA DE LISTADO PROCESOS PENDIENTES", pResult.ErrorMessage),
                    AppConstants.UrlLogApplication);
                }
                else
                {
                    if (pResult.PaymentList.Count > 0)
                    {
                        PSEHostingTransactionInformationReturn ret;
                        xpinnWSPayment.PaymentStatusEnum newState = xpinnWSPayment.PaymentStatusEnum.pending;
                        foreach (xpinnWSPayment.PaymentACH nPayment in pResult.PaymentList)
                        {
                            // CONSULTANDO PROCESO EN ACH
                            ret = VerifyPayment(AppConstants.ID_TICKETOFFICE, AppConstants.PASSWORD, nPayment.ID.ToString());
                            if (ret.ReturnCode == PSEHostingTransactionInformationReturnCode.OK)
                            {
                                // HOMOLOGANDO ESTADO.
                                switch (ret.State)
                                {
                                    case PSEHostingTransactionState.CREATED:
                                        newState = xpinnWSPayment.PaymentStatusEnum.created;
                                        break;
                                    case PSEHostingTransactionState.PENDING:
                                        newState = xpinnWSPayment.PaymentStatusEnum.pending;
                                        break;
                                    case PSEHostingTransactionState.OK:
                                        newState = xpinnWSPayment.PaymentStatusEnum.approved;
                                        break;
                                    case PSEHostingTransactionState.NOT_AUTHORIZED:
                                        newState = xpinnWSPayment.PaymentStatusEnum.rejected;
                                        break;
                                    case PSEHostingTransactionState.FAILED:
                                        newState = xpinnWSPayment.PaymentStatusEnum.failed;
                                        break;
                                }

                                // EN CASO TENGA ESTADO DIFERENTE ACTUALIZO EN BD
                                if (newState != nPayment.Status)
                                {
                                    nPayment.Status = newState;
                                    nPayment.BankCode = ret.BankCode;
                                    nPayment.BankName = ret.BankName;

                                    xpinnWSPayment.PaymentACH p = PaymentService.UpdatePaymentTransaction(nPayment);
                                    if (p == null)
                                    {
                                        LogPaymentAction.Grabar(
                                            new LogPayment(AppConstants.WindowServicesName, AppConstants.NameApplication, "MODIFICACION DE PAGO - WINDOWS SERVICES", "SE GENERÓ UN ERROR EN EL PROCESO DE MODIFICACIÓN."),
                                            AppConstants.UrlLogApplication);
                                    }
                                    else
                                    {
                                        if (!p.Success)
                                        {
                                            if (!string.IsNullOrEmpty(p.ErrorMessage))
                                            {
                                                LogPaymentAction.Grabar(
                                                    new LogPayment(AppConstants.WindowServicesName, AppConstants.NameApplication, "MODIFICACION DE PAGO - WINDOWS SERVICES", "SE GENERÓ UN ERROR EN EL PROCESO DE MODIFICACIÓN - INTERNO." + p.ErrorMessage),
                                                    AppConstants.UrlLogApplication);
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogPaymentAction.Grabar(
                    new LogPayment(AppConstants.WindowServicesName, AppConstants.NameApplication, ex.Message, ex.StackTrace),
                    AppConstants.UrlLogApplication);
            }
        }

        public PSEHostingTransactionInformationReturn VerifyPayment(int ticketOfficeID, string password, string paymentID)
        {
            XpnPaymentWS ws = new XpnPaymentWS();
            ws.Open(AppConstants.PSE_URL, AppConstants.USE_WS_SECURITY);
            return ws.getTransactionInformationHosting(ticketOfficeID, password, paymentID);
        }

    }
}
