using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Package.Entities
{
    [DataContract]
    [Serializable]
    public class descuentosvalores
    {
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public int cod_atr { get; set; }
        [DataMember]
        public Int64 num_cuota { get; set; }
        [DataMember]
        public decimal? valor_presente { get; set; }
        [DataMember]
        public decimal? valor { get; set; }
    }
}