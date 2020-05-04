using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class DetImpuesto
    {
        [DataMember]
        public Int64 id_detimpuesto { get; set; }
        [DataMember]
        public Int64 num_comp { get; set; }
        [DataMember]
        public Int64 tipo_comp { get; set; }
        [DataMember]
        public Int64 codigo_comp { get; set; }
        [DataMember]
        public Int64 cod_tipo_impuesto { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public decimal base_impuesto { get; set; }
        [DataMember]
        public decimal porcentaje { get; set; }
        [DataMember]
        public decimal valor { get; set; }
    }
}
