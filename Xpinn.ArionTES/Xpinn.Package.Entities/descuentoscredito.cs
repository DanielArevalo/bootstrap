using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Package.Entities
{
    [DataContract]
    [Serializable]
    public class descuentoscredito
    {
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public int cod_atr { get; set; }
        [DataMember]
        public int? tipo_descuento { get; set; }
        [DataMember]
        public int? tipo_liquidacion { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public int cobra_mora { get; set; }
        [DataMember]
        public decimal? numero_cuotas { get; set; }
        [DataMember]
        public int forma_descuento { get; set; }
        [DataMember]
        public int tipo_impuesto { get; set; }
    }
}