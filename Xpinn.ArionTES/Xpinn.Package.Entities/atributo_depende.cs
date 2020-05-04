using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Package.Entities
{
    [DataContract]
    [Serializable]
    public class atributo_depende
    {
        [DataMember]
        public int cod_atr { get; set; }
        [DataMember]
        public int depende { get; set; }
    }
}