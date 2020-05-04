using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Caja.Entities
{
    [DataContract]
    [Serializable]
    public class ProcesoOficina
    {
        [DataMember]
        public Int64 IdProcesoOficina { get; set; }
        [DataMember]
        public Int64 consecutivo { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public Int64 tipo_horario { get; set; }
        [DataMember]
        public Int64 tipo_proceso { get; set; }
        [DataMember]
        public Int64 cod_usuario { get; set; }
        [DataMember]
        public String usuario_aperturo { get; set; }
        [DataMember]
        public DateTime fecha_proceso { get; set; }
        [DataMember]
        public Int64 estado { get; set; }// estado de la oficina, activa o inactiva
        [DataMember]
        public Int64 nuevoestado { get; set; }// Nuevo estado de la oficina, activa o inactiva
    }
}
