using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Xpinn.Asesores.Entities.Common;
namespace Xpinn.Asesores.Entities
{
    public class Barrios
    {
        [DataMember]
        public Int64 CODCIUDAD { get; set; }
        [DataMember]
        public string NOMCIUDAD { get; set; }
    }
}
