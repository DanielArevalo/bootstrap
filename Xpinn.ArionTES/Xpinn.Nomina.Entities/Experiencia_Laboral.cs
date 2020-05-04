using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class Experiencia_Laboral
    {
        [DataMember]
        public Int64 consecutivo_empleado { get; set; }
        [DataMember]
        public Int64 consecutivo { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public string nombre_empresa { get; set; }
        [DataMember]
        public int? codcargo { get; set; }
        [DataMember]
        public DateTime? fecha_ingreso { get; set; }
        [DataMember]
        public string motivo_retiro { get; set; }
        [DataMember]
        public DateTime? fecha_retiro { get; set; }
    }
}
