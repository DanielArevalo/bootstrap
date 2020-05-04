using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Asesores.Entities
{
    /// <summary>
    /// Entidad AgendaAlarma
    /// </summary>
    [DataContract]
    [Serializable]
    public class AgendaAlarma 
    {
        [DataMember]
        public Int64 idalarma { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public Int64 hora { get; set; }
        [DataMember]
        public Int64 tipoalarma { get; set; }
        [DataMember]
        public Int64 idcliente { get; set; }
        [DataMember]
        public Int64 tipoactividad { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public Int64 repeticiones { get; set; }
        [DataMember]
        public Int64 estado { get; set; }
        [DataMember]
        public Int64 idasesor { get; set; }

    }
}