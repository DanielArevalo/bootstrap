using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Reporteador.Entities
{
    [DataContract]
    [Serializable]
    public class UIAF_Exonerados
    {
        [DataMember]
        public Int64 idexonerado { get; set; }
        [DataMember]
        public Int64 idreporte { get; set; }
        [DataMember]
        public Int64 consecutivo { get; set; }
        [DataMember]
        public DateTime fecha_exoneracion { get; set; }
        [DataMember]
        public string tipo_identificacion { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string primer_apellido { get; set; }
        [DataMember]
        public string segundo_apellido { get; set; }
        [DataMember]
        public string primer_nombre { get; set; }
        [DataMember]
        public string segundo_nombre { get; set; }
        [DataMember]
        public string razon_social { get; set; }
    }
}