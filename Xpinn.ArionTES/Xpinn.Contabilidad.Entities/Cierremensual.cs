using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class Cierremensual
    {
        [DataMember]      
        public DateTime fecha { get; set; }
        [DataMember]
        public Int64 centro_costo { get; set; }   
        [DataMember]
        public Double valor { get; set; }
        [DataMember]
        public String estado { get; set; }
        [DataMember]
        public Int64 terceros { get; set; }
    }          
       
}
