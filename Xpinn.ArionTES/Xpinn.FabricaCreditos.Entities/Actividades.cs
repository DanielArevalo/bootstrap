using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class Actividades
    {
        [DataMember]
        public Int64 idactividad { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public DateTime? fecha_realizacion { get; set; }
        [DataMember]
        public decimal? tipo_actividad { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int? participante { get; set; }
        [DataMember]
        public string calificacion { get; set; }
        [DataMember]
        public string duracion { get; set; }
        [DataMember]
        public string codactividad { get; set; }
        
    }
}