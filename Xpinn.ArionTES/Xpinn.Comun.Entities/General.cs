using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Comun.Entities
{
    [DataContract]
    [Serializable]
    public class General
    {
        [DataMember]
        public int codigo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string valor { get; set; }
    }
}