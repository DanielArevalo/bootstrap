using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Xpinn.Asesores.Entities
{
    [DataContract]
    [Serializable]
    public class MovGralCredito
    {
        [DataMember]
        public Int64 codRadicacion { get; set; }
        [DataMember]
        public Int64 idPersona { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string apellidos { get; set; }
        [DataMember]
        public Int64 numeroDocumento { get; set; }
        [DataMember]
        public string nombreTipoIdentificacion { get; set; }
        [DataMember]
        public string linea { get; set; }
        [DataMember]
        public string mov { get; set; }
    }
}
