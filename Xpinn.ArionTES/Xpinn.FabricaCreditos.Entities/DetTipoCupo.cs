using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class DetTipoCupo
    {
        [DataMember]
        public Int64 iddetalle { get; set; }
        [DataMember]
        public Int64 tipo_cupo { get; set; }
        [DataMember]
        public Int64 idvariable { get; set; }
        [DataMember]
        public string valor { get; set; }
    }

}