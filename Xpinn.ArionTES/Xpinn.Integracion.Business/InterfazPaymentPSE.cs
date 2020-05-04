using Xpinn.Integracion.Entities;
using Xpinn.Util.PaymentACH;
/// <summary>
/// Summary description for InterfazPaymentPSE
/// </summary>
public class InterfazPaymentPSE
{
    public InterfazPaymentPSE()
    {
    }

    public PSEHostingCreateTransactionReturn CreateProcessPayment(long cod_persona, int ticketOfficeID, decimal amount, decimal vatAmount, string paymentDescription,
        string referenceNumber1, string referenceNumber2, string referenceNumber3, string serviceCode, string email, Xpinn.Util.PaymentACH.PSEHostingField[] fields, string entity_url, int typeProduct, string numberProduct, int ID)
    {
        ACHPayment pPaymentACH = new ACHPayment();
        PSEHostingCreateTransactionReturn ret = null;        
        // GENERAR CONSUMO DE SERVICIO
        XpnPaymentWS ws = new XpnPaymentWS();
        ws.Open(AppConstants.PSE_URL, AppConstants.USE_WS_SECURITY);
        ret = ws.createTransactionPaymentHosting(ticketOfficeID, amount, vatAmount, ID.ToString(), paymentDescription, referenceNumber1, referenceNumber2, referenceNumber3,
            serviceCode, email, fields, entity_url);

        pPaymentACH.Identifier = ret.PaymentIdentifier;
        if (ret.ReturnCode == PSEHostingCreateTransactionReturnCode.ERRORS)
            pPaymentACH.Status = Xpinn.Integracion.Entities.PaymentStatusEnum.failed;

        return ret;
    }


    public PSEHostingTransactionInformationReturn VerifyPayment(int ticketOfficeID, string password, string paymentID)
    {
        XpnPaymentWS ws = new XpnPaymentWS();
        ws.Open(AppConstants.PSE_URL, AppConstants.USE_WS_SECURITY);
        return ws.getTransactionInformationHosting(ticketOfficeID, password, paymentID);
    }


}