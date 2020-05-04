using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class BeneficiarioPersonaAfi
    {
        [DataMember]
        public long cod_solicitud { get; set; }
        [DataMember]
        public int cod_beneficiario { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string apellidos { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public Int64? codparentesco { get; set; }
        [DataMember]
        public decimal? porcentaje { get; set; }
        [DataMember]
        public int tipo_id { get; set; }
        [DataMember]
        public int sexo { get; set; }
        [DataMember]
        public DateTime fecha_nac { get; set; }
        [DataMember]
        public string ocupacion { get; set; }
        [DataMember]
        public int nivel_educativo { get; set; }
    }
}