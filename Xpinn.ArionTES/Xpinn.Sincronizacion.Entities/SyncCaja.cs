using System;
using System.Runtime.Serialization;

namespace Xpinn.Sincronizacion.Entities
{
    [DataContract]
    [Serializable]
    public class SyncCaja
    {
        [DataMember]
        public Int64 cod_caja { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public DateTime? fecha_creacion { get; set; }
        [DataMember]
        public int? esprincipal { get; set; }
        [DataMember]
        public int? estado { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string cod_datafono { get; set; }
    }
}
