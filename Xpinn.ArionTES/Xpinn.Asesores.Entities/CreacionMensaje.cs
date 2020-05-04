using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Xpinn.Asesores.Entities
{
    public class CreacionMensaje
    {
        [DataMember]
        public Int64 IdMensaje { get; set; }
        [DataMember]
        public string Descripcion { get; set; }

        [DataMember]
        public Int32 IdEstado { get; set; }
        [DataMember]
        public string  Estado { get; set; }
        [DataMember]
        public string documentoPersona { get; set; }
    }

    public class PesonasTemp
    {
        [DataMember]
        public string documento { get; set; }
        [DataMember]
        public string Nombre { get; set; }
    }
}
