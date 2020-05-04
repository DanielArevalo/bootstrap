using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Comun.Entities
{
    [DataContract]
    [Serializable]
    public class GestionDocumental

    {
        [DataMember]
        public Int64 IdTipo { get; set; } 
        [DataMember]
        public string NombreTabla { get; set; }
        [DataMember]
        public string CampoTabla { get; set; } 
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public Int64 CodPersona { get; set; }

    }
}
