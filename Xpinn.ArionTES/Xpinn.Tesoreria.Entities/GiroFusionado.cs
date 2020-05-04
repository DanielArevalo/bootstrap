using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class GiroFusionado
    {
        [DataMember]
        public int idfusion { get; set; }
        [DataMember]
        public  Int64? idgiro_fus { get; set; }
        [DataMember]
        public int? idgiro_nue { get; set; }
        [DataMember]
        public DateTime? fecha_fusion { get; set; }
        [DataMember]
        public decimal? valor { get; set; }
        [DataMember]
        public int? estado { get; set; }
    }
}