using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Riesgo.Entities
{
    [DataContract]
    [Serializable]

    public class SegmentacionPerfil
    {

        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public String identificacion { get; set; }
        [DataMember]
        public String tipo_persona { get; set; }
        [DataMember]
        public String primer_nombre { get; set; }
        [DataMember]
        public String segundo_nombre { get; set; }
        [DataMember]
        public String primer_apellido { get; set; }
        [DataMember]
        public Int64? codciudadresidencia { get; set; }
        [DataMember]
        public Int64? coddeparesidencia { get; set; }
        [DataMember]
        public Int64? act_ciiu_empresa { get; set; }
        [DataMember]
        public Int64? Es_peps { get; set; }
        [DataMember]
        public Int64? Parentesco_PEPS { get; set; }
        [DataMember]
        public Int64? cod_especial { get; set; }
        [DataMember]
        public Int64? miembro_control { get; set; }
        [DataMember]
        public Int64? miembro_administracion { get; set; }
        [DataMember]
        public Int64 valoracion { get; set; }
        [DataMember]
        public String perfil { get; set; }


    }
}
