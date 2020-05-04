using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Xpinn.Integracion.Entities
{
    public class ACHPayment
    {
        private long id;
        private PaymentTypeEnum type;
        private decimal amount;
        private decimal vatAmount;
        private string paymentID;
        private string paymentDescription;
        private string email;
        private string serviceCode;
        private string referenceNumber1;
        private string referenceNumber2;
        private string referenceNumber3;
        private string entity_url;
        //private DynamicFields dynamicFields;
        private PaymentStatusEnum status;
        private bool confirmedGET;
        private string bankCode;
        private string bankName;
        private int typeProduct;
        private string numberProduct;
        private string trazabilityCode;
        private int transactionCycle;
        private DateTime solicitedDate;
        private string identifier;
        private string fields;

        public long ID
        {
            get { return id; }
            set { id = value; }
        }

        public PaymentTypeEnum Type
        {
            get { return type; }
            set { type = value; }
        }

        public string TypeLabel
        {
            get
            {
                switch (Type)
                {
                    case PaymentTypeEnum.normal:
                        return "normal";
                    case PaymentTypeEnum.multicredit:
                        return "multicrédito";
                    default:
                        return "desconecido";
                }
            }
        }

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public decimal VATAmount
        {
            get { return vatAmount; }
            set { vatAmount = value; }
        }

        public string PaymentID
        {
            get { return paymentID; }
            set { paymentID = value; }
        }

        public string PaymentDescription
        {
            get { return paymentDescription; }
            set { paymentDescription = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string ServiceCode
        {
            get { return serviceCode; }
            set { serviceCode = value; }
        }

        public string ReferenceNumber1
        {
            get { return referenceNumber1; }
            set { referenceNumber1 = value; }
        }

        public string ReferenceNumber2
        {
            get { return referenceNumber2; }
            set { referenceNumber2 = value; }
        }

        public string ReferenceNumber3
        {
            get { return referenceNumber3; }
            set { referenceNumber3 = value; }
        }

        public string Entity_url
        {
            get { return entity_url; }
            set { entity_url = value; }
        }

        /*
        public DynamicFields DynamicFields
        {
            get
            {
                if (dynamicFields == null)
                    dynamicFields = new DynamicFields();
                return dynamicFields;
            }
            set { dynamicFields = value; }
        }
        */

        public PaymentStatusEnum Status
        {
            get { return status; }
            set { status = value; }
        }

        public string StatusDescription
        {
            get
            {
                switch (Status)
                {
                    case PaymentStatusEnum.created:
                        return "Creado";
                    case PaymentStatusEnum.pending:
                        return "Pendiente";
                    case PaymentStatusEnum.approved:
                        return "Aprobado";
                    case PaymentStatusEnum.rejected:
                        return "Rechazado";
                    case PaymentStatusEnum.failed:
                        return "Fallida";
                    default:
                        return "Desconecido";
                }
            }
        }

        public bool ConfirmedGET
        {
            get { return confirmedGET; }
            set { confirmedGET = value; }
        }

        public string BankCode
        {
            get { return bankCode; }
            set { bankCode = value; }
        }

        public string BankName
        {
            get { return bankName; }
            set { bankName = value; }
        }

        public int TypeProduct
        {
            get { return typeProduct; }
            set { typeProduct = value; }
        }

        public string NumberProduct
        {
            get { return numberProduct; }
            set { numberProduct = value; }
        }

        public string TrazabilityCode
        {
            get { return trazabilityCode; }
            set { trazabilityCode = value; }
        }

        public int TransactionCycle
        {
            get { return transactionCycle; }
            set { transactionCycle = value; }
        }

        public DateTime SolicitedDate
        {
            get { return solicitedDate; }
            set { solicitedDate = value; }
        }

        public string Identifier
        {
            get { return identifier; }
            set { identifier = value; }
        }

        public string Fields
        {
            get { return fields; }
            set { fields = value; }
        }

        public long Cod_persona { get; set; }
        public long Cod_ope { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class ACHCredentials
    {
        public int TicketOfficeId { get; set; } // 2506;
        public string password { get; set; } // "123";
        public bool use_ws_security { get; set; } // false;
        public string serviceCode { get; set; } // "1989";
        public string dynamic_fields { get; set; } // "id_cliente;identificacion_cliente;nombre_cliente";
    }
    public class PaymentACHResult
    {
        public ACHPayment PaymentObj { get; set; }
        public List<ACHPayment> PaymentList { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }

    public enum PaymentTypeEnum
    {
        normal = 0,
        multicredit = 1
    }

    public enum PaymentStatusEnum
    {
        created = 0,
        pending = 1,
        approved = 2,
        rejected = 3,
        failed = 4
    }

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

}
