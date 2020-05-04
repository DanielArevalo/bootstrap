using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class Directivo
    {
        [DataMember]
        public Int64 consecutivo { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public int calidad { get; set; }
        [DataMember]
        public string desc_calidad { get; set; }
        [DataMember]
        public DateTime? fecha_nombramiento { get; set; }
        [DataMember]
        public DateTime? vigencia_inicio { get; set; }
        [DataMember]
        public string parientes { get; set; }
        [DataMember]
        public string vinculos_organiza { get; set; }
        [DataMember]
        public int? tipo_directivo { get; set; }
        [DataMember]
        public string desc_tipo_directivo { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string desc_estado { get; set; }
        [DataMember]
        public DateTime? vigencia_final { get; set; }
        [DataMember]
        public DateTime? fecha_posesion { get; set; }
        [DataMember]
        public string empresa { get; set; }
        [DataMember]
        public long cod_persona { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string num_radi_pose { get; set; }
        [DataMember]
        public string tarj_rev_fiscar { get; set; }
    }
}
