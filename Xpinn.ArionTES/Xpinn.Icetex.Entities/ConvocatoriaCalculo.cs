using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Icetex.Entities
{
    [DataContract]
    [Serializable]
    public class ConvocatoriaCalculo
    {
        [DataMember]
        public int cod_convcal { get; set; }
        [DataMember]
        public int cod_convacatoria { get; set; }
        [DataMember]
        public int tipo_proceso { get; set; }
        [DataMember]
        public DateTime fecha_inicial { get; set; }
        [DataMember]
        public DateTime fecha_final { get; set; }
    }
}
