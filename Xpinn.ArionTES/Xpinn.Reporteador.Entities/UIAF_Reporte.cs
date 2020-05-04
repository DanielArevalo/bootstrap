using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Reporteador.Entities
{
    [DataContract]
    [Serializable]
    public class UIAF_Reporte
    {
        [DataMember]
        public Int64 idreporte { get; set; }
        [DataMember]
        public string idformato { get; set; }
        [DataMember]
        public DateTime? fecha_generacion { get; set; }
        [DataMember]
        public int? numero_registros { get; set; }
        [DataMember]
        public Int64 codusuario { get; set; }
        [DataMember]
        public DateTime? fecha_inicial { get; set; }
        [DataMember]
        public DateTime? fecha_final { get; set; }
    }
}