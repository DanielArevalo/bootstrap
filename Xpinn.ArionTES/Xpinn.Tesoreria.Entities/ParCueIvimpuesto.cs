using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class ParCueIvimpuesto
    {
        [DataMember]
        public Int64 idparametro { get; set; }
        [DataMember]
        public Int64 id_impuesto { get; set; }
        [DataMember]
        public string impuesto { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string nombre_cuenta { get; set; }
        [DataMember]
        public string cod_cuenta_niif { get; set; }
        [DataMember]
        public string nombre_cuenta_niif { get; set; }
        [DataMember]
        public int tipo { get; set; }

    }
}
