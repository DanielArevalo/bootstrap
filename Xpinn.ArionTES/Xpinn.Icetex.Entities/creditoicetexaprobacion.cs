using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Icetex.Entities
{
    [DataContract]
    [Serializable]
    public class CreditoIcetexAprobacion
    {
        [DataMember]
        public int idaprobacion { get; set; }
        [DataMember]
        public Int64? numero_credito { get; set; }
        [DataMember]
        public DateTime fecha_aprobacion { get; set; }
        [DataMember]
        public int cod_usuario { get; set; }
        [DataMember]
        public string observaciones { get; set; }
        [DataMember]
        public byte[] documento_soporte { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember] // tipo [ 1 = Pre Inscritos, 2 = Inscritos ]
        public int? tipo_aprobacion { get; set; }
        [DataMember]
        public string descripcion { get; set; }
    }
}
