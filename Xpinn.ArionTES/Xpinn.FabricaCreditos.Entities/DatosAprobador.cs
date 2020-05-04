using System;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class DatosAprobador
    {
        [DataMember]
        public Int64 Radicacion { get; set; }
        [DataMember]
        public Int32 Nivel { get; set; }
        [DataMember]
        public string Nombres { get; set; }
        [DataMember]
        public string Observaciones { get; set; }
        [DataMember]
        public string EstadoDescripcion { get; set; }
    }
}
