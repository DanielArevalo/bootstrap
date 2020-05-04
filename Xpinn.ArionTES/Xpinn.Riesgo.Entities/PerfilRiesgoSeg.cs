using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Riesgo.Entities
{

    [DataContract]
    [Serializable()]

    public class PerfilRiesgoSeg
    {

        [DataMember]
        public Int64 COD_PERSONA { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string primer_nombre { get; set; }
        [DataMember]
        public string segundo_nombre { get; set; }
        [DataMember]
        public string primer_apellido { get; set; }
        [DataMember]
        public Int64 valoracion { get; set; }
        [DataMember]
        public string perfil { get; set; }
        [DataMember]
        public string tipo_rol { get; set; }
        [DataMember]
        public string nom_tipo_rol { get; set; }
    }
}
