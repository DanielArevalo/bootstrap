using Microsoft.Web.Services3.Design;
using System;

namespace Xpinn.Util.PaymentACH
{
    public class XpnPaymentWS : IDisposable
    {
        private XpnPaymentWSServices proxy = null;

        public void Open(string URL, bool useWSSecurity)
        {
            proxy = new XpnPaymentWSServices();
            proxy.Url = URL;
            if (useWSSecurity)
                proxy.SetPolicy(Policies.Default["PseClientPolicy"]);
            proxy.CookieContainer = new System.Net.CookieContainer();
            System.Net.ServicePointManager.SecurityProtocol = /*System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Ssl3 |*/ System.Net.SecurityProtocolType.Tls12;
            System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(myCertificateValidation);
        }

        public bool myCertificateValidation(Object sender, System.Security.Cryptography.X509Certificates.X509Certificate cert, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors Errors)
        {
            return true;
        }

        public void Dispose()
        {
            if (proxy != null)
                proxy.Dispose();
        }

        public PSEHostingCreateTransactionReturn createTransactionPaymentHosting(int ticketOfficeID, decimal amount, decimal vatAmount, string paymentID, string paymentDescription, string referenceNumber1, string referenceNumber2, string referenceNumber3, string serviceCode, string email, PSEHostingField[] fields, string entity_url)
        {
            return proxy.createTransactionPaymentHosting(ticketOfficeID,
                    amount,
                    vatAmount,
                    paymentID,
                    paymentDescription,
                    referenceNumber1,
                    referenceNumber2,
                    referenceNumber3,
                    serviceCode,
                    email,
                    fields,
                    entity_url);
        }

        public PSEHostingTransactionInformationReturn getTransactionInformationHosting(int ticketOfficeID, string password, string paymentID)
        {
            return proxy.getTransactionInformationHosting(ticketOfficeID, password, paymentID);
        }

    }
}
