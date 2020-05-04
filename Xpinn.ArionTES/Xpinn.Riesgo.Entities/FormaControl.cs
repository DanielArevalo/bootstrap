using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Riesgo.Entities
{
    [DataContract]
    [Serializable]
    public class FormaControl
    {
        [DataMember]
        public int cod_formacontrol { get; set; }
        [DataMember]
        public int cod_atributo { get; set; }
        [DataMember]
        public string atributo { get; set; }
        [DataMember]
        public int cod_opcion { get; set; }
        [DataMember]
        public string opcion { get; set; }
        [DataMember]
        public int valor { get; set; }
    }
}
