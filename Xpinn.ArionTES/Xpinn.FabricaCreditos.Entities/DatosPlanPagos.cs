using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class DatosPlanPagos
    {
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public Int64 numerocuota { get; set; }
        [DataMember]
        public DateTime? fechacuota { get; set; }
        [DataMember]
        public decimal sal_ini { get; set; }
        [DataMember]
        public decimal capital { get; set; }
        [DataMember]
        public decimal int_1 { get; set; }
        [DataMember]
        public decimal int_2 { get; set; }
        [DataMember]
        public decimal int_3 { get; set; }
        [DataMember]
        public decimal int_4 { get; set; }
        [DataMember]
        public decimal int_5 { get; set; }
        [DataMember]
        public decimal int_6 { get; set; }
        [DataMember]
        public decimal int_7 { get; set; }
        [DataMember]
        public decimal int_8 { get; set; }
        [DataMember]
        public decimal int_9 { get; set; }
        [DataMember]
        public decimal int_10 { get; set; }
        [DataMember]
        public decimal int_11 { get; set; }
        [DataMember]
        public decimal int_12 { get; set; }
        [DataMember]
        public decimal int_13 { get; set; }
        [DataMember]
        public decimal int_14 { get; set; }
        [DataMember]
        public decimal int_15 { get; set; }
        [DataMember]
        public decimal cap_tf { get; set; }
        [DataMember]
        public decimal int_tf { get; set; }
        [DataMember]
        public decimal total { get; set; }
        [DataMember]
        public decimal sal_fin { get; set; }
        [DataMember]
        public decimal interes { get; set; }
        [DataMember]
        public decimal sal_pendiente { get; set; }
        [DataMember]
        public decimal seguro { get; set; }
        [DataMember]
        public decimal valorcuota { get; set; }

        [DataMember]
        public List<DescuentosCredito> lstDescuentos { get; set; }

        [DataMember]
        public List<DescuentosCredito> lstSumados { get; set; }


      
    }
}
