using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class ACHcampo
    {
        [DataMember]
        public Int64 codigo { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public int? tipo { get; set; }
        [DataMember]
        public string valor { get; set; }
        [DataMember]
        public int? tipo_dato { get; set; }
        [DataMember]
        public int? justificacion { get; set; }
        [DataMember]
        public string formato { get; set; }
        [DataMember]
        public string punto { get; set; }
        [DataMember]
        public int? num_dec { get; set; }
        [DataMember]
        public decimal? longitud { get; set; }
        [DataMember]
        public string llenado { get; set; }
        [DataMember]
        public decimal? suma { get; set; }
        [DataMember]
        public int? orden { get; set; }

        [DataMember]
        public int? registro { get; set; }
    }
}