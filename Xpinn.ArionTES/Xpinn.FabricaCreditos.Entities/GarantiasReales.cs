using System;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    public class GarantiasReales
    {
        [DataMember]
        public Int64 consecutivo { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string TipoGarantia { get; set; }
        [DataMember]
        public string ctadebito { get; set; }
        [DataMember]
        public string ctacredito { get; set; }
    }
}
