using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Riesgo.Entities
{
    [DataContract]
    [Serializable]
    public class valoracion_control
    {
        [DataMember]
        public Int64 cod_control { get; set; }
        [DataMember]
        public Int64? valor { get; set; }
        [DataMember]
        public string calificacion { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int? frecuencia { get; set; }
        [DataMember]
        public string desc_frecuencia { get; set; }
        [DataMember]
        public int? rango { get; set; }
        [DataMember]
        public int? rango_minimo { get; set; }
        [DataMember]
        public int? rango_maximo { get; set; }

       
    }
}
