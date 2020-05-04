using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Scoring.Entities
{
    public class Modelo
    {
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public Int64 Codigo { get; set; }
    }
}

