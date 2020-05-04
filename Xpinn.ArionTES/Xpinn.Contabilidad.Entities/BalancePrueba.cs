using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class BalancePrueba
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
        public string tipo { get; set; }
        [DataMember]
        public Double valor { get; set; }
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
        public string tercero { get; set; }
        [DataMember]
        public string nombreTercero { get; set; }
        [DataMember]
        public int essaldocuenta { get; set; }
    }

}
