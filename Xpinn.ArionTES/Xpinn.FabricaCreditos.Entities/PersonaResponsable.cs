using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class PersonaResponsable
    {
        [DataMember]
        public Int64 consecutivo { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public Int64 cod_persona_tutor { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public int tipo_identificacion { get; set; }
        [DataMember]
        public string nombre_ter { get; set; }
    }
}