using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class ivalmacen
    {
        [DataMember]
        public Int64 almacenid { get; set; }
        [DataMember]
        public string almacenname { get; set; }
        [DataMember]
        public string direccion { get; set; }
    }
}
