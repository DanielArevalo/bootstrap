using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class Detalle_Dotacion
    {
        [DataMember]
        public Int64 id_detalle_dotacion { get; set; }
        [DataMember]
        public Int64 id_dotacion { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public Decimal valor { get; set; }
        [DataMember]
        public string caracteristica { get; set; }

    }
}
