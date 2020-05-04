using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class PendienteAporte
    {
        [DataMember]
        public Int64 codpendiente { get; set; }
        [DataMember]
        public Int64 numero_aporte { get; set; }
        [DataMember]
        public int cod_atributo { get; set; }
        [DataMember]
        public DateTime fecha_cuota { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public decimal saldo { get; set; }
        [DataMember]
        public DateTime fecha_mora { get; set; }
        [DataMember]
        public decimal valor_mora { get; set; }
    }
}