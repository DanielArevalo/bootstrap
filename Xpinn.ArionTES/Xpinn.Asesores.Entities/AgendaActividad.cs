using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Asesores.Entities
{
    /// <summary>
    /// Entidad AgendaActividad
    /// </summary>
    [DataContract]
    [Serializable]
    public class AgendaActividad
    {
        [DataMember]
        public Int64 idactividad { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public Int64 hora { get; set; }
        [DataMember]
        public Int64 idcliente { get; set; }
        [DataMember]
        public string nombrecliente { get; set; }
        [DataMember]
        public string tipoactividad { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string atendido { get; set; }
        [DataMember]
        public Int64 idparentesco { get; set; }
        [DataMember]
        public string respuesta { get; set; }
        [DataMember]
        public string observaciones { get; set; }
        [DataMember]
        public Int64 idasesor { get; set; }
        [DataMember]
        public string email { get; set; }
    }
}