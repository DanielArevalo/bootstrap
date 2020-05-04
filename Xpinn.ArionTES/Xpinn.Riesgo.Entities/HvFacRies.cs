using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Riesgo.Entities
{
    [DataContract]
    [Serializable]
    public class HvFacRies
    {
        [DataMember]
        public string pPersona { get; set; }
        [DataMember]
        public string nivel_probabilidad { get; set; }
        [DataMember]
        public string nivel_impacto { get; set; }
        [DataMember]
        public string valor_Rinherente { get; set; }
        [DataMember]
        public string valor_rresidaul { get; set; }
        [DataMember]
        public Int64 clase { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string frecuencia { get; set; }
        [DataMember]
        public string desc_frecuencia { get; set; }
        [DataMember]
        public string impacto_reputacional { get; set; }
        [DataMember]
        public string impacto_legal { get; set; }
        [DataMember]
        public string impacto_operativo { get; set; }
        [DataMember]
        public string contagio { get; set; }

    }
}
