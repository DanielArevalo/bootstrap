using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Riesgo.Entities
{

    [DataContract]
    [Serializable()]
    
 public class ActDatos
    {

        [DataMember]
        public Int64 Id_update { get; set; }
        [DataMember]
        public string Identificacion { get; set; }
        [DataMember]
        public string Primer_nombre { get; set; }
        [DataMember]
        public string Segundo_nombre { get; set; }
        [DataMember]
        public string Primer_Apellido { get; set; }
        [DataMember]
        public string Fecha_act { get; set; }
    }
}
