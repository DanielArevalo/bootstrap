using System;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class AnexoCredito
    {
        [DataMember]
        public Int64 Radicacion { get; set; }
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Link { get; set; }
    }
}
