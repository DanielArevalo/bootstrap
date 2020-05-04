using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Caja.Entities
{
    [DataContract]
    [Serializable]
    public class Ciudad
    {
        [DataMember]
        public Int64 IdCiudad { get; set; }
        [DataMember]
        public Int64 cod_ciudad { get; set; }
        [DataMember]
        public string nom_ciudad { get; set; }
    }
}
