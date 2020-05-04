using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class BalanceTerceros
    {
        [DataMember]
        public DateTime? fecha { get; set; }
        [DataMember]
        public DateTime fechames13cons { get; set; }
        [DataMember]
        public Int64? fechames13 { get; set; }
        [DataMember]
        public Int64? centro_costo { get; set; }
        [DataMember]
        public Int64 nivel { get; set; }
        [DataMember]
        public Int16 cuentascero { get; set; }
        [DataMember]
        public Int16 comparativo { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string nombrecuenta { get; set; }
        [DataMember]
        public string tercero { get; set; }
        [DataMember]
        public string nombreTercero { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public Double valor { get; set; }
        [DataMember]
        public Double SaldoIni { get; set; }
        [DataMember]
        public Double Creditos { get; set; }
        [DataMember]
        public Double Debitos { get; set; }
        [DataMember]
        public Double SaldoFin { get; set; }
        [DataMember]
        public Int64 centro_costo_fin { get; set; }
        [DataMember]
        public Int64? mostrarmovper13 { get; set; }
        [DataMember]
        public Int64 cod_moneda { get; set; }
        [DataMember]
        public Double saldo_inicial { get; set; }
        [DataMember]
        public Double debitos { get; set; }
        [DataMember]
        public Double creditos { get; set; }
        [DataMember]
        public Double saldo_final { get; set; }
        [DataMember]
        public string descipcion { get; set; }
        [DataMember]
        public Double valorcentro { get; set; }
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
        public bool esniif { get; set; }
    }

}
