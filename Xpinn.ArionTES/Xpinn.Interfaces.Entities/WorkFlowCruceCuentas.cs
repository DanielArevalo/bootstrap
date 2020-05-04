using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Interfaces.Entities
{
    [DataContract]
    [Serializable]
    public class WorkFlowCruceCuentas
    {
        [DataMember]
        public long consecutivo { get; set; }
        [DataMember]
        public Int64 codigopersona { get; set; }
        [DataMember]
        public int? codigoidgiro { get; set; }
        [DataMember]
        public string barcode { get; set; }
    }
}