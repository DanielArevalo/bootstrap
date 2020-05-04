using System;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    public class Oficina
    {
        [DataMember]
        public Int32 Codigo { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public Int32 iusuario { get; set; }
        [DataMember]
        public Int64 estado { get; set; }
    }
}
