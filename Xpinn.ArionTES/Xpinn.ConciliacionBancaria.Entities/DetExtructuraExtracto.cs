using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.ConciliacionBancaria.Entities
{
    [DataContract]
    [Serializable]
    public class DetEstructuraExtracto
    {
        [DataMember]
        public int iddetestructura { get; set; }
        [DataMember]
        public int idestructuraextracto { get; set; }
        [DataMember]
        public int? codigo_campo { get; set; }
        [DataMember]
        public int? numero_columna { get; set; }
        [DataMember]
        public int? posicion_inicial { get; set; }
        [DataMember]
        public int? longitud { get; set; }
        [DataMember]
        public int? justificacion { get; set; }
        [DataMember]
        public string justificador { get; set; }
        [DataMember]
        public int? decimales { get; set; }
    }
}