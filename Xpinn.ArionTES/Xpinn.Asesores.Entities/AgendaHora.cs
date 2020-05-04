using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities
{
    /// <summary>
    /// Entidad AgendaHora
    /// </summary>
    [DataContract]
    [Serializable]
    public class AgendaHora
    {
        [DataMember]
        public Int64 idhora { get; set; }
        [DataMember]
        public decimal hora { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public string horatipo { get; set; }
        [DataMember]
        public Int64 idactividad { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string respuesta { get; set; }

    }
}