using Xpinn.Util.PaymentACH;
/// <summary>
/// Summary description for InterfazPaymentPSE
/// </summary>
public class InterfazPaymentPSE
{
    xpinnWSPayment.WSPaymentSoapClient PaymentService;
    public InterfazPaymentPSE()
    {
        PaymentService = new xpinnWSPayment.WSPaymentSoapClient();
    }

    public PSEHostingCreateTransactionReturn CreateProcessPayment(long cod_persona, int ticketOfficeID, decimal amount, decimal vatAmount, string paymentDescription,
        string referenceNumber1, string referenceNumber2, string referenceNumber3, string serviceCode, string email, DynamicFields fields, string entity_url, int typeProduct, string numberProduct, string sec)
    {
        PSEHostingCreateTransactionReturn ret = null;
        // CREACION DE REGISTRO DE CONTROL
        xpinnWSPayment.PaymentACH p = new xpinnWSPayment.PaymentACH();
        p.Cod_persona = cod_persona;
        p.Type = xpinnWSPayment.PaymentTypeEnum.normal;
        p.Identifier = "";
        p.Status = xpinnWSPayment.PaymentStatusEnum.created;
        p.ConfirmedGET = false;
        p.Amount = amount;
        p.VATAmount = vatAmount;
        p.PaymentDescription = paymentDescription;
        p.Email = email;
        p.ServiceCode = serviceCode;
        p.ReferenceNumber1 = referenceNumber1;
        p.ReferenceNumber2 = referenceNumber2;
        p.ReferenceNumber3 = referenceNumber3;
        p.TypeProduct = typeProduct;
        p.NumberProduct = numberProduct;
        // PENDIENTE REGISTRO DEL PRODUCTO AL QUE PERTENECE EL PAGO
        p = PaymentService.CreatePaymentTransaction(p, sec);

        // VALIDAR QUE GENERE EL REGISTRO DE MANERA CORRECTA
        if (p != null)
        {
            if (p.ID == 0)
            {
                ret = new PSEHostingCreateTransactionReturn();
                ret.ReturnCode = PSEHostingCreateTransactionReturnCode.ERRORS;
                ret.ErrorMessage = string.IsNullOrEmpty(p.ErrorMessage) ? "ERROR EN LA CREACIÓN DE TRANSACCIÓN INICIAL" : p.ErrorMessage;
                return ret;
            }
        }
        
        // GENERAR CONSUMO DE SERVICIO
        XpnPaymentWS ws = new XpnPaymentWS();
        ws.Open(AppConstants.PSE_URL, AppConstants.USE_WS_SECURITY);
        if (entity_url == null || entity_url.Length == 0)
            entity_url = null;
        else
            entity_url += "?ID=" + p.ID.ToString();
        ret = ws.createTransactionPaymentHosting(ticketOfficeID, amount, vatAmount, p.ID.ToString(), paymentDescription, referenceNumber1, referenceNumber2, referenceNumber3,
            serviceCode, email, fields.SaveAsPSEHostingFields(), entity_url);

        p.Identifier = ret.PaymentIdentifier;
        if (ret.ReturnCode == PSEHostingCreateTransactionReturnCode.ERRORS)
            p.Status = xpinnWSPayment.PaymentStatusEnum.failed;
        
        // MODIFICACION DEL PROCESO TERMINADO
        p = PaymentService.UpdatePaymentTransaction(p, sec);

        return ret;
    }


    public PSEHostingTransactionInformationReturn VerifyPayment(int ticketOfficeID, string password, string paymentID)
    {
        XpnPaymentWS ws = new XpnPaymentWS();
        ws.Open(AppConstants.PSE_URL, AppConstants.USE_WS_SECURITY);
        return ws.getTransactionInformationHosting(ticketOfficeID, password, paymentID);
    }


}