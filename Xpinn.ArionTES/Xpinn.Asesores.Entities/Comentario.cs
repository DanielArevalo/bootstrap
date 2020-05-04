using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities
{
    [DataContract]
    [Serializable]
    public class Comentario
    {
        [DataMember]
        public Int64 idComentario { get; set; }
        [DataMember]
        public string fecha { get; set; }
        [DataMember]
        public string hora { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public Int64 idPersona { get; set; }
        [DataMember]
        public Int64 numeroCredito { get; set; }
        [DataMember]
        public bool puedeVerAsociado { get; set; }
    }
}