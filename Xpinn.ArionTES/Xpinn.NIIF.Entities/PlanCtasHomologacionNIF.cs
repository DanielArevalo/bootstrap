using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.NIIF.Entities
{
    [DataContract]
    [Serializable]
    public class PlanCtasHomologacionNIF
    {
        [DataMember]
        public Int64 idhomologa { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string cod_cuenta_niif { get; set; }
        [DataMember]
        public string nombre_cuenta { get; set; }
    }
}