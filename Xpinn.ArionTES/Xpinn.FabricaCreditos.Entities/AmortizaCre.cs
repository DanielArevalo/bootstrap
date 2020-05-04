using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class AmortizaCre
    {
        [DataMember]
        public Int64 idamortiza { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public int? cod_atr { get; set; }
        [DataMember]
        public DateTime? fecha_cuota { get; set; }
        [DataMember]
        public decimal? valor { get; set; }
        [DataMember]
        public decimal? saldo { get; set; }
        [DataMember]
        public decimal saldo_base { get; set; }
        [DataMember]
        public decimal tasa_base { get; set; }
        [DataMember]
        public int dias_base { get; set; }
        [DataMember]
        public string estado { get; set; }
    }
}