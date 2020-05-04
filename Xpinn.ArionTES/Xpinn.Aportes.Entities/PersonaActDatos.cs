using System;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class PersonaActDatos
    {
        [DataMember]
        public Int64 id_update { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public DateTime? fecha_act { get; set; }
        [DataMember]
        public Int64? cod_usua { get; set; }
    }
}
