using System;
using System.Runtime.Serialization;

namespace Xpinn.Sincronizacion.Entities
{
    [DataContract]
    [Serializable]
    public class SyncCajero
    {
        [DataMember]
        public Int64 cod_cajero { get; set; }
        [DataMember]
        public Int64? cod_usuario { get; set; }
        [DataMember]
        public Int64? cod_caja { get; set; }
        [DataMember]
        public DateTime? fecha_creacion { get; set; }
        [DataMember]
        public DateTime? fecha_retiro { get; set; }
        [DataMember]
        public int? estado { get; set; }
        [DataMember]
        public Int64? cod_caja_des { get; set; }
    }
}
