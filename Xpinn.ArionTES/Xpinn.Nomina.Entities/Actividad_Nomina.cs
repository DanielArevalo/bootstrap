using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class Actividad_Nomina
    {
        [DataMember]
        public Int64 consecutivo { get; set; }
        [DataMember]
        public string nombre_actividad { get; set; }
        [DataMember]
        public string objetivo { get; set; }
        [DataMember]
        public string fecha_inicio { get; set; }
        [DataMember]
        public string fecha_terminacion { get; set; }
        [DataMember]
        public string cod_persona { get; set; }
        [DataMember]
        public string centro_costo { get; set; }
    }
}
