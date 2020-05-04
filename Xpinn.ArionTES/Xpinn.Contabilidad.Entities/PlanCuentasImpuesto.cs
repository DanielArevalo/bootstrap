using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class PlanCuentasImpuesto
    {
        [DataMember]
        public int idimpuesto { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public int? cod_tipo_impuesto { get; set; }
        [DataMember]
        public decimal? porcentaje_impuesto { get; set; }
        [DataMember]
        public decimal? base_minima { get; set; }
        [DataMember]
        public string cod_cuenta_imp { get; set; }
        [DataMember]
        public int asumido { get; set; }
        [DataMember]
        public string cod_cuenta_asumido { get; set; }
    }
}
