using System;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]

    public class Motivo
    {
        [DataMember]
        public Int32 Codigo { get; set; }
        [DataMember]
        public String Descripcion { get; set; }
        [DataMember]
        public String Tipo { get; set; }
    }
}
