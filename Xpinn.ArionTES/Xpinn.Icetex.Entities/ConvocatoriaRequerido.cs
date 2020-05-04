using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Icetex.Entities
{
    [DataContract]
    [Serializable]
    public class ConvocatoriaRequerido
    {
        [DataMember]
        public int cod_convreq { get; set; }
        [DataMember]
        public int cod_convocatoria { get; set; }
        [DataMember]
        public int? tipo_proceso { get; set; }
        [DataMember]
        public int tipo_requisito { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int? obligatorio { get; set; }
        [DataMember]
        public string mensaje { get; set; }
        [DataMember]
        public int IsVisible { get; set; }
        [DataMember]
        public string observacion { get; set; }
    }
}
