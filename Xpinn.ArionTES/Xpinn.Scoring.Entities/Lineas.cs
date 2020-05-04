using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Scoring.Entities
{
    public class Lineas
    {
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Codigo { get; set; }
       
    }    
}
