using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Xpinn.Comun.Entities
{
    [DataContract]
    [Serializable]
    public class Homologacion
    {
        [DataMember]
        public string tipo_identificacion_banbogota { get; set; }
        [DataMember]
        public int? tipo_identificacion_bancolombia { get; set; }
        [DataMember]
        public int? tipo_identificacion_cifin { get; set; }
        [DataMember]
        public int? tipo_identificacion_data { get; set; }
        [DataMember]
        public string tipo_identificacion_enpacto { get; set; }
        [DataMember]
        public int? tipo_identificacion_banpopular { get; set; }
        [DataMember]
        public int? tipo_identificacion_banagrario { get; set; }
    }

    public class Persona
    {
        [DataMember]
        public string cod_persona { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string digito_verificacion { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string apellidos { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
    }

}
