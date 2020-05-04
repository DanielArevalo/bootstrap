using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class Dotacion
    {
        [DataMember]
        public Int64 id_dotacion { get; set; }
        [DataMember]
        public Int64? cod_empleado { get; set; }
        [DataMember]
        public DateTime? fecha { get; set; }
        [DataMember]
        public string ubicacion { get; set; }
        [DataMember]
        public Int64? cantidad { get; set; }
        [DataMember]
        public int? centro_costo { get; set; }
        [DataMember]
        public string nombre_empleado { get; set; }
        [DataMember]
        public string desc_centro_costo { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public long cod_tipo_identificacion { get; set; }
    }
}
