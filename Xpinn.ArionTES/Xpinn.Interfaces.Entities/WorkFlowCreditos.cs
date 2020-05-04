using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Interfaces.Entities
{
    [DataContract]
    [Serializable]
    public class WorkFlowCreditos
    {
        [DataMember]
        public long consecutivo { get; set; }
        [DataMember]
        public long codigopersona { get; set; }
        [DataMember]
        public long workflowid { get; set; }
        [DataMember]
        public long numeroradicacion { get; set; }
        [DataMember]
        public string barCodeRadicacion { get; set; }
        [DataMember]
        public bool documentosFueronGenerados { get; set; }
    }
}
