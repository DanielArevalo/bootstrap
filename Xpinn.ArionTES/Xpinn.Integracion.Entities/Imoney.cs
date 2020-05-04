using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Integracion.Entities
{
    [DataContract]
    [Serializable]
    public class Agreements
    {
        [DataMember]
        public int? id { get; set; }

        [DataMember]
        public string local_id { get; set; }

        [DataMember]
        public string type { get; set; }

        [DataMember]
        public string status { get; set; }

        [DataMember]
        public string dst_product_number { get; set; }

        [DataMember]
        public string dst_product_type { get; set; }

        [DataMember]
        public string dst_product_line { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public string commerce_type { get; set; }

        [DataMember]
        public string commercial_activity { get; set; }

        [DataMember]
        public string business_name { get; set; }

        [DataMember]
        public string address { get; set; }

        [DataMember]
        public string neighborhood { get; set; }

        [DataMember]
        public decimal? latitude { get; set; }

        [DataMember]
        public decimal? longitude { get; set; }

        [DataMember]
        public int? country_id { get; set; }

        [DataMember]
        public int? region_id { get; set; }

        [DataMember]
        public int? city_id { get; set; }

        [DataMember]
        public List<Commissions> commissions { get; set; }
        
        [DataMember]
        public int? tenant_id { get; set; }

        [DataMember]
        public Tenant tenant { get; set; }

        [DataMember]
        public List<Tags> tags { get; set; }

        [DataMember]
        public List<Taxes> taxes { get; set; }

        [DataMember]
        public string api_key { get; set; }
    }

    [DataContract]
    [Serializable]
    public class AgreementsResult
    {
        [DataMember]
        public int size { get; set; }
        [DataMember]
        public List<Agreements> results { get; set; }
    }

    [DataContract]
    [Serializable]
    public class Auth
    {
        [DataMember]
        public string jwt { get; set; }
        [DataMember]
        public int exp { get; set; }
    }

    [DataContract]
    [Serializable]
    public class Charges
    {

        [DataMember]
        public int size { get; set; }

        [DataMember]
        public Results results { get; set; }

        [DataMember]
        public bool is_package { get; set; }
    }

    [DataContract]
    [Serializable]
    public class Commissions
    {
        [DataMember]
        public decimal value { get; set; }

        [DataMember]
        public string type { get; set; }

        [DataMember]
        public string currency { get; set; }

        [DataMember]
        public string transaction_type { get; set; }
    }

    [DataContract]
    [Serializable]
    public class Fullmovil
    {
        [DataMember]
        public int? id { get; set; }

        [DataMember]
        public string agreement_ref_id { get; set; }

        [DataMember]
        public int? agreement_id { get; set; }

        [DataMember]
        public string status { get; set; }

        [DataMember]
        public decimal? subtotal { get; set; }

        [DataMember]
        public List<Taxes> taxes { get; set; }

        [DataMember]
        public int? tip { get; set; }

        [DataMember]
        public decimal? total { get; set; }

        [DataMember]
        public string currency { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public string payer_local_id { get; set; }

        [DataMember]
        public string transaction_type { get; set; }

        [DataMember]
        public string expiration_date { get; set; }

        [DataMember]
        public int store_id { get; set; }

        [DataMember]
        public int ref_id { get; set; }

        [DataMember]
        public int top_up_status { get; set; }

        //FullmovilTransaction
        [DataMember]
        public string phone { get; set; }

        [DataMember]
        public string operador { get; set; }

        [DataMember]
        public string package_id { get; set; }


        //INT_TRAN_IMONEY
        [DataMember]
        public int id_transaccion { get; set; }
        [DataMember]
        public int cod_persona { get; set; }
        [DataMember]
        public string descripcion_plan { get; set; }
        [DataMember]
        public DateTime fecha_tran { get; set; }
    }

    [DataContract]
    [Serializable]
    public class Operators
    {

        [DataMember]
        public int operator_id { get; set; }

        [DataMember]
        public string operator_name { get; set; }

        [DataMember]
        public bool is_package { get; set; }
    }

    [DataContract]
    [Serializable]
    public class Package
    {
        [DataMember]
        public string description { get; set; }

        [DataMember]
        public int package_id { get; set; }

        [DataMember]
        public string operator_name { get; set; }

        [DataMember]
        public int total { get; set; }
    }

    [DataContract]
    [Serializable]
    public class Payment
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string product_number { get; set; }

        [DataMember]
        public string product_type { get; set; }

        [DataMember]
        public string ref_id { get; set; }

        [DataMember]
        public string agreement_ref_id { get; set; }

        [DataMember]
        public string tenant_ref_id { get; set; }

        [DataMember]
        public string agreement_local_id { get; set; }

        [DataMember]
        public Agreements agrement { get; set; }

        [DataMember]
        public int store_id { get; set; }

        [DataMember]
        public string tenant_id { get; set; }

        [DataMember]
        public string type { get; set; }

        [DataMember]
        public string status { get; set; }

        [DataMember]
        public string currency { get; set; }

        [DataMember]
        public int subtotal { get; set; }

        [DataMember]
        public List<Taxes> taxes { get; set; }

        [DataMember]
        public int tip { get; set; }

        [DataMember]
        public int total { get; set; }

        [DataMember]
        public int commission { get; set; }

        [DataMember]
        public int commission_tax { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public int dst_tenant_id { get; set; }

        [DataMember]
        public string dst_product_number { get; set; }

        [DataMember]
        public string dst_product_type { get; set; }

        [DataMember]
        public User_data user_data { get; set; }
    }

    [DataContract]
    [Serializable]
    public class Results
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string agreement_ref_id { get; set; }

        [DataMember]
        public Agreements agrement { get; set; }

        [DataMember]
        public int agreement_id { get; set; }

        [DataMember]
        public string status { get; set; }

        [DataMember]
        public int subtotal { get; set; }

        [DataMember]
        public List<Taxes> taxes { get; set; }

        [DataMember]
        public int tip { get; set; }

        [DataMember]
        public string total { get; set; }

        [DataMember]
        public string currency { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public string payer_local_id { get; set; }

        [DataMember]
        public string transaction_type { get; set; }

        [DataMember]
        public string expiration_date { get; set; }

        [DataMember]
        public int store_id { get; set; }

    }

    [DataContract]
    [Serializable]
    public class Tags
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string type { get; set; }

        [DataMember]
        public string description { get; set; }
    }

    [DataContract]
    [Serializable]
    public class Taxes
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string type { get; set; }

        [DataMember]
        public string based_on { get; set; }

        [DataMember]
        public string percentage { get; set; }

        [DataMember]
        public int? value { get; set; }
    }

    [DataContract]
    [Serializable]
    public class Tenant
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string local_id { get; set; }
        [DataMember]
        public string local_id_type { get; set; }
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string bank_number { get; set; }
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string parent_tenant_id { get; set; }
        [DataMember]
        public string parent_product_number { get; set; }
        [DataMember]
        public string parent_product_type { get; set; }
        [DataMember]
        public string url { get; set; }
    }

    [DataContract]
    [Serializable]
    public class TenantResult
    {
        [DataMember]
        public int size { get; set; }
        [DataMember]
        public List<Tenant> results { get; set; }
    }

    [DataContract]
    [Serializable]
    public class User_data
    {

        [DataMember]
        public string first_name { get; set; }

        [DataMember]
        public string last_name { get; set; }

        [DataMember]
        public string local_id { get; set; }

        [DataMember]
        public string local_id_type { get; set; }

    }
}
      

