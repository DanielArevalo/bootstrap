using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class ivpersonas_autoret
    {
        [DataMember]
        public string per_documento { get; set; }
        [DataMember]
        public int tipo_retencion { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public decimal porcentaje { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
    }
}