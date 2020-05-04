using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Servicios.Entities
{
    [DataContract]
    [Serializable]
    public class ExclusionServicios
    {
        [DataMember]
        public Int64 idexclusión { get; set; }
        [DataMember]
        public int numero_servicio { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public int? codmotivo { get; set; }
        [DataMember]
        public int? cuo_noexcluir { get; set; }
        [DataMember]
        public int? cod_ope { get; set; }
        [DataMember]
        public DateTime? feccrea { get; set; }
        [DataMember]
        public int? usuariocrea { get; set; }
    }

}
