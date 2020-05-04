using Microsoft.Web.Services3;

namespace Xpinn.Util.PaymentACH
{

    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "PSEHostingWSSoap", Namespace = "http://www.achcolombia.com.co/PSEHostingWS")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(PSEHostingField[]))]
    public partial class XpnPaymentWSServices : WebServicesClientProtocol
    {

        #region PROPERTIES

        private System.Threading.SendOrPostCallback createTransactionPaymentHostingOperationCompleted;

        public event createTransactionPaymentHostingCompletedEventHandler createTransactionPaymentHostingCompleted;

        public event getTransactionInformationHostingCompletedEventHandler getTransactionInformationHostingCompleted;

        #endregion

        public XpnPaymentWSServices()
        {
            string UrlConfig = AppConstants.PSE_URL;
            this.Url = UrlConfig;
        }

        #region METHODS

        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.achcolombia.com.co/PSEHostingWS/createTransactionPaymentHosting", RequestNamespace = "http://www.achcolombia.com.co/PSEHostingWS", ResponseNamespace = "http://www.achcolombia.com.co/PSEHostingWS", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public PSEHostingCreateTransactionReturn createTransactionPaymentHosting(int ticketOfficeID, decimal amount, decimal vatAmount, string paymentID, string paymentDescription, string referenceNumber1, string referenceNumber2, string referenceNumber3, string serviceCode, string email, PSEHostingField[] fields, string entity_url)
        {
            object[] results = this.Invoke("createTransactionPaymentHosting", new object[] {
                    ticketOfficeID,
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
                    entity_url});
            return ((PSEHostingCreateTransactionReturn)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegincreateTransactionPaymentHosting(int ticketOfficeID, decimal amount, decimal vatAmount, string paymentID, string paymentDescription, string referenceNumber1, string referenceNumber2, string referenceNumber3, string serviceCode, string email, PSEHostingField[] fields, string entity_url, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("createTransactionPaymentHosting", new object[] {
                    ticketOfficeID,
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
                    entity_url}, callback, asyncState);
        }

        /// <remarks/>
        public PSEHostingCreateTransactionReturn EndcreateTransactionPaymentHosting(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((PSEHostingCreateTransactionReturn)(results[0]));
        }

        /// <remarks/>
        public void createTransactionPaymentHostingAsync(int ticketOfficeID, decimal amount, decimal vatAmount, string paymentID, string paymentDescription, string referenceNumber1, string referenceNumber2, string referenceNumber3, string serviceCode, string email, PSEHostingField[] fields, string entity_url)
        {
            this.createTransactionPaymentHostingAsync(ticketOfficeID, amount, vatAmount, paymentID, paymentDescription, referenceNumber1, referenceNumber2, referenceNumber3, serviceCode, email, fields, entity_url, null);
        }

        /// <remarks/>
        public void createTransactionPaymentHostingAsync(int ticketOfficeID, decimal amount, decimal vatAmount, string paymentID, string paymentDescription, string referenceNumber1, string referenceNumber2, string referenceNumber3, string serviceCode, string email, PSEHostingField[] fields, string entity_url, object userState)
        {
            if ((this.createTransactionPaymentHostingOperationCompleted == null))
            {
                this.createTransactionPaymentHostingOperationCompleted = new System.Threading.SendOrPostCallback(this.OncreateTransactionPaymentHostingOperationCompleted);
            }
            this.InvokeAsync("createTransactionPaymentHosting", new object[] {
                    ticketOfficeID,
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
                    entity_url}, this.createTransactionPaymentHostingOperationCompleted, userState);
        }

        private void OncreateTransactionPaymentHostingOperationCompleted(object arg)
        {
            if ((this.createTransactionPaymentHostingCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.createTransactionPaymentHostingCompleted(this, new createTransactionPaymentHostingCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.achcolombia.com.co/PSEHostingWS/getTransactionInformationHosting", RequestNamespace = "http://www.achcolombia.com.co/PSEHostingWS", ResponseNamespace = "http://www.achcolombia.com.co/PSEHostingWS", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public PSEHostingTransactionInformationReturn getTransactionInformationHosting(int ticketOfficeID, string password, string paymentID)
        {
            object[] results = this.Invoke("getTransactionInformationHosting", new object[] {
                    ticketOfficeID,
                    password,
                    paymentID});
            return ((PSEHostingTransactionInformationReturn)(results[0]));
        }

        public System.IAsyncResult BegingetTransactionInformationHosting(int ticketOfficeID, string password, string paymentID, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("getTransactionInformationHosting", new object[] {
                    ticketOfficeID,
                    password,
                    paymentID}, callback, asyncState);
        }

        /// <remarks/>
        public PSEHostingTransactionInformationReturn EndgetTransactionInformationHosting(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((PSEHostingTransactionInformationReturn)(results[0]));
        }

        private void OngetTransactionInformationHostingOperationCompleted(object arg)
        {
            if ((this.getTransactionInformationHostingCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getTransactionInformationHostingCompleted(this, new getTransactionInformationHostingCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }

        #endregion

    }

    #region ENTITY MODELS

    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.achcolombia.com.co/PSEHostingWS")]
    public partial class PSEHostingField
    {

        private string nameField;

        private object valueField;

        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public object Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.achcolombia.com.co/PSEHostingWS")]
    public partial class PSEHostingCreateTransactionReturn
    {

        private PSEHostingCreateTransactionReturnCode returnCodeField;

        private string errorMessageField;

        private string paymentIdentifierField;

        /// <remarks/>
        public PSEHostingCreateTransactionReturnCode ReturnCode
        {
            get
            {
                return this.returnCodeField;
            }
            set
            {
                this.returnCodeField = value;
            }
        }

        /// <remarks/>
        public string ErrorMessage
        {
            get
            {
                return this.errorMessageField;
            }
            set
            {
                this.errorMessageField = value;
            }
        }

        /// <remarks/>
        public string PaymentIdentifier
        {
            get
            {
                return this.paymentIdentifierField;
            }
            set
            {
                this.paymentIdentifierField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.achcolombia.com.co/PSEHostingWS")]
    public partial class PSEHostingTransactionInformationReturn
    {

        private PSEHostingTransactionInformationReturnCode returnCodeField;

        private string errorMessageField;

        private PSEHostingTransactionState stateField;

        private string paymentIDField;

        private decimal amountField;

        private decimal vATAmountField;

        private string bankCodeField;

        private string bankNameField;

        private string serviceCodeField;

        private string trazabilityCodeField;

        private int cycleNumberField;

        private string reference1Field;

        private string reference2Field;

        private string reference3Field;

        private System.DateTime solicitedDateField;

        /// <remarks/>
        public PSEHostingTransactionInformationReturnCode ReturnCode
        {
            get
            {
                return this.returnCodeField;
            }
            set
            {
                this.returnCodeField = value;
            }
        }

        /// <remarks/>
        public string ErrorMessage
        {
            get
            {
                return this.errorMessageField;
            }
            set
            {
                this.errorMessageField = value;
            }
        }

        /// <remarks/>
        public PSEHostingTransactionState State
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
            }
        }

        /// <remarks/>
        public string PaymentID
        {
            get
            {
                return this.paymentIDField;
            }
            set
            {
                this.paymentIDField = value;
            }
        }

        /// <remarks/>
        public decimal Amount
        {
            get
            {
                return this.amountField;
            }
            set
            {
                this.amountField = value;
            }
        }

        /// <remarks/>
        public decimal VATAmount
        {
            get
            {
                return this.vATAmountField;
            }
            set
            {
                this.vATAmountField = value;
            }
        }

        /// <remarks/>
        public string BankCode
        {
            get
            {
                return this.bankCodeField;
            }
            set
            {
                this.bankCodeField = value;
            }
        }

        /// <remarks/>
        public string BankName
        {
            get
            {
                return this.bankNameField;
            }
            set
            {
                this.bankNameField = value;
            }
        }

        /// <remarks/>
        public string ServiceCode
        {
            get
            {
                return this.serviceCodeField;
            }
            set
            {
                this.serviceCodeField = value;
            }
        }

        /// <remarks/>
        public string TrazabilityCode
        {
            get
            {
                return this.trazabilityCodeField;
            }
            set
            {
                this.trazabilityCodeField = value;
            }
        }

        /// <remarks/>
        public int CycleNumber
        {
            get
            {
                return this.cycleNumberField;
            }
            set
            {
                this.cycleNumberField = value;
            }
        }

        /// <remarks/>
        public string Reference1
        {
            get
            {
                return this.reference1Field;
            }
            set
            {
                this.reference1Field = value;
            }
        }

        /// <remarks/>
        public string Reference2
        {
            get
            {
                return this.reference2Field;
            }
            set
            {
                this.reference2Field = value;
            }
        }

        /// <remarks/>
        public string Reference3
        {
            get
            {
                return this.reference3Field;
            }
            set
            {
                this.reference3Field = value;
            }
        }

        /// <remarks/>
        public System.DateTime SolicitedDate
        {
            get
            {
                return this.solicitedDateField;
            }
            set
            {
                this.solicitedDateField = value;
            }
        }
    }


    #endregion


    #region ENUMS

    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.achcolombia.com.co/PSEHostingWS")]
    public enum PSEHostingCreateTransactionReturnCode
    {

        /// <remarks/>
        ERRORS,

        /// <remarks/>
        OK,
    }


    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.achcolombia.com.co/PSEHostingWS")]
    public enum PSEHostingTransactionInformationReturnCode
    {

        /// <remarks/>
        INVALIDTICKETORPASSWORD,

        /// <remarks/>
        INVALIDPAYMENTID,

        /// <remarks/>
        ERRORS,

        /// <remarks/>
        OK,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.achcolombia.com.co/PSEHostingWS")]
    public enum PSEHostingTransactionState
    {

        /// <remarks/>
        CREATED,

        /// <remarks/>
        PENDING,

        /// <remarks/>
        FAILED,

        /// <remarks/>
        NOT_AUTHORIZED,

        /// <remarks/>
        OK,
    }

    #endregion


    #region DELEGATE

    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void createTransactionPaymentHostingCompletedEventHandler(object sender, createTransactionPaymentHostingCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    public delegate void getTransactionInformationHostingCompletedEventHandler(object sender, getTransactionInformationHostingCompletedEventArgs e);

    #endregion


    #region EVENTS

    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class createTransactionPaymentHostingCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal createTransactionPaymentHostingCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public PSEHostingCreateTransactionReturn Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((PSEHostingCreateTransactionReturn)(this.results[0]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getTransactionInformationHostingCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal getTransactionInformationHostingCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public PSEHostingTransactionInformationReturn Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((PSEHostingTransactionInformationReturn)(this.results[0]));
            }
        }
    }


    #endregion


}
