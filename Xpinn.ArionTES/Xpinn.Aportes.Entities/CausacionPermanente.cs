using System;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class CausacionPermanente
    {
        [DataMember]
        public int idcausacion { get; set; }
        [DataMember]
        public DateTime? fecha_causacion { get; set; }
        [DataMember]
        public Int64 numero_aporte { get; set; }
        [DataMember]
        public decimal saldo_base { get; set; }
        [DataMember]
        public decimal tasa { get; set; }
        [DataMember]
        public int dias_causados { get; set; }
        [DataMember]
        public decimal int_causados { get; set; }
        [DataMember]
        public decimal retencion { get; set; }
        [DataMember]
        public decimal valor_acumulado { get; set; }
        [DataMember]
        public Int64? cod_ope { get; set; }
        [DataMember]
        public int cod_linea_aporte { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public int cod_oficina { get; set; }
        [DataMember]
        public decimal saldo_total { get; set; }
        [DataMember]
        public string usuario { get; set; }
        [DataMember]
        public string estado { get; set; }
    }
}
