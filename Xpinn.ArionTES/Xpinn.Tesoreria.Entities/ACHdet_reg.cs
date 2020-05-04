using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class ACHdet_reg
    {
        [DataMember]
        public int registro { get; set; }
        [DataMember]
        public int campo { get; set; }
        [DataMember]
        public int orden { get; set; }
    }
}