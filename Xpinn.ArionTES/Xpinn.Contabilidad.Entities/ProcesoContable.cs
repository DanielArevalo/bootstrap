using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class ProcesoContable
    {
        [DataMember]
        public Int64 cod_proceso { get; set; }
        [DataMember]
        public Int64 tipo_ope { get; set; }
        [DataMember]
        public string nom_tipo_ope { get; set; }
        [DataMember]
        public Int64 tipo_comp { get; set; }
        [DataMember]
        public string nom_tipo_comp { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public DateTime fecha_inicial { get; set; }
        [DataMember]
        public DateTime fecha_final { get; set; }
        [DataMember]
        public Int64 concepto { get; set; }
        [DataMember]
        public string nom_concepto { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public Int64 cod_est_det { get; set; }
        [DataMember]
        public Int64 nom_est_det { get; set; }
        [DataMember]
        public int? tipo_mov { get; set; }
    }
}
