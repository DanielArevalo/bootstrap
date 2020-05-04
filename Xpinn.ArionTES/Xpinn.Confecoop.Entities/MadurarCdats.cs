using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Confecoop.Entities
{
    [DataContract]
    [Serializable]
    public class MadurarCdats
    {
        [DataMember]
        public string numero_cdat { get; set; }
        [DataMember]
        public DateTime fecha_vencimiento { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public Int32 prorrogas { get; set; }
    }
}
