using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.CDATS.Entities
{
    [DataContract]
    [Serializable]
    public class SolicitudRenovacion
    {
        [DataMember]
        public Int64 idrenovacion { get; set; }
        [DataMember]
        public Int64 codigo_cdat { get; set; }
        [DataMember]
        public DateTime? fecha_vencimiento { get; set; }
        [DataMember]
        public DateTime? fecha_solicitud { get; set; }
        [DataMember]
        public string cod_lineacdat { get; set; }
        [DataMember]
        public int? plazo { get; set; }
        [DataMember]
        public string observacion { get; set; }
        [DataMember]
        public int? estado { get; set; }
        [DataMember]
        public string mensaje_error { get; set; }
    }
}