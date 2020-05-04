using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Caja.Entities
{
    [DataContract]
    [Serializable]
    public class TipoTope
    {
        [DataMember]
        public Int64 IdTipoTope { get; set; }
        [DataMember]
        public string cod_moneda { get; set; }
        [DataMember]
        public string descTope { get; set; }
        [DataMember]
        public string descMoneda { get; set; }
        [DataMember]
        public Int64 tipotope { get; set; }
        [DataMember]
        public string simbol { get; set; }
        [DataMember]
        public Int64 valor_minimo { get; set; }
        [DataMember]
        public Int64 valor_maximo { get; set; }
    }
}
