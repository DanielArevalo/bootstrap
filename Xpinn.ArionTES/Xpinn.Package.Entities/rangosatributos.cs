using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Package.Entities
{
    [DataContract]
    [Serializable]
    public class rangosatributos
    {
        [DataMember]
        public string cod_linea_credito { get; set; }
        [DataMember]
        public int cod_rango_atr { get; set; }
        [DataMember]
        public string nombre { get; set; }
    }
}