using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Package.Entities
{
    [DataContract]
    [Serializable]
    public class rangostopes
    {
        [DataMember]
        public int idtope { get; set; }
        [DataMember]
        public int cod_rango_atr { get; set; }
        [DataMember]
        public string minimo { get; set; }
        [DataMember]
        public string maximo { get; set; }
        [DataMember]
        public int? tipo_tope { get; set; }
        [DataMember]
        public string cod_linea_credito { get; set; }
    }
}