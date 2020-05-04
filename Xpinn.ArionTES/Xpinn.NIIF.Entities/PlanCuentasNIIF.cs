using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.NIIF.Entities
{
    [DataContract]
    [Serializable]
    public class PlanCuentasNIIF
    {
        [DataMember]
        public string cod_cuenta_niif { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public Int64? nivel { get; set; }
        [DataMember]
        public string depende_de { get; set; }
        [DataMember]
        public Int64? cod_moneda { get; set; }
        [DataMember]
        public string moneda { get; set; }
        [DataMember]
        public Int64? maneja_cc { get; set; }
        [DataMember]
        public Int64? maneja_ter { get; set; }
        [DataMember]
        public Int64? maneja_sc { get; set; }
        [DataMember]
        public Int64? estado { get; set; }
        [DataMember]
        public Int64? impuesto { get; set; }
        [DataMember]
        public Int64? maneja_gir { get; set; }
        [DataMember]
        public Decimal? porcentaje_impuesto { get; set; }
        [DataMember]
        public Decimal? base_minima { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public int corriente { get; set; }
        [DataMember]
        public int nocorriente { get; set; }
        [DataMember]
        public int tipo_distribucion { get; set; }
        [DataMember]
        public decimal porcentaje_distribucion { get; set; }
        [DataMember]
        public decimal valor_distribucion { get; set; }
        [DataMember]
        public string error { get; set; }
        
        [DataMember]
        public decimal saldo { get; set; }
        [DataMember]
        public decimal saldo_local { get; set; }
        [DataMember]
        public string nombre_local { get; set; }
        [DataMember]
        public decimal diferencia { get; set; }
        [DataMember] 
        public Int64? reportarmayor { get; set; }
}
}