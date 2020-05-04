using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Xpinn.Comun.Entities
{
    public class EliminarRegistro
    {
        [DataMember]
        public long IdConsecutivo { get; set; }
        [DataMember]
        public string NombreTabla { get; set; }
        [DataMember]
        public string IdTablaRes { get; set; }
        [DataMember]
        public string Perror { get; set; }
    }
}
