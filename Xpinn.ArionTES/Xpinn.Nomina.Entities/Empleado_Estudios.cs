using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class Empleado_Estudios
    {
        [DataMember]
        public Int64 consecutivo_empleado { get; set; }
        [DataMember]
        public Int64 consecutivo { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public string semestre { get; set; }
        [DataMember]
        public string profesion { get; set; }
        [DataMember]
        public Int64? horario_estudio { get; set; }
        [DataMember]
        public DateTime? fecha_inicio { get; set; }
        [DataMember]
        public string titulo_obtenido { get; set; }
        [DataMember]
        public string establecimiento { get; set; }
        [DataMember]
        public DateTime? fecha_terminacion { get; set; }
        [DataMember]
        public Int64? horario_titulo { get; set; }
        [DataMember]
        public Int64 estudia { get; set; }

    }
}
