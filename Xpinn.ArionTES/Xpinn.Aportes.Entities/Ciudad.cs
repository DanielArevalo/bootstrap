using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class Ciudad
    {
        [DataMember]
        public Int64 codciudad { get; set; }
        [DataMember]
        public string nomciudad { get; set; }
        [DataMember]
        public int tipo { get; set; }
        [DataMember]
        public Int64 depende_de { get; set; }
        [DataMember]
        public string nomtipo { get; set; }
    }
}
