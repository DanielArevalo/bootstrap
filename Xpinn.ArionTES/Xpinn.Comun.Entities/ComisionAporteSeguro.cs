using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Comun.Entities
{
    [DataContract]
    [Serializable]
    public class ComisionAporteSeguro
    {
        [DataMember]
        public Boolean comision { get; set; } 
        [DataMember]
        public Boolean aporte { get; set; }
        [DataMember]
        public Boolean seguro { get; set; }
    }
}
