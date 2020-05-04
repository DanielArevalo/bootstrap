using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class Factura
    {
        [DataMember]
        public String numerofactura { get; set; }
        [DataMember]
        public String resolucion { get; set; }
        [DataMember]
        public String entidad { get; set; }
    }
}
