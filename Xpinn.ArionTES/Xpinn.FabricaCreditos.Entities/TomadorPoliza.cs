using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


namespace  Xpinn.FabricaCreditos.Entities
{

    [DataContract]
    [Serializable]
    public class TomadorPoliza
    {
        [DataMember]
        public Int64 identificacion { get; set; }
        [DataMember]
        public String razonsocial { get; set; }
        
    }
}
