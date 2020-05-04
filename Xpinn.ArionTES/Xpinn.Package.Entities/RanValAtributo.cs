using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Package.Entities
{
    [DataContract]
    [Serializable]
    public class RanValAtributo
    {
        [DataMember]
        public int? n_tipo_tope { get; set; }
        [DataMember]
        public decimal? n_desde { get; set; }
        [DataMember]
        public decimal? n_hasta { get; set; }
        [DataMember]
        public decimal? n_valor_tope { get; set; }
    }
}
