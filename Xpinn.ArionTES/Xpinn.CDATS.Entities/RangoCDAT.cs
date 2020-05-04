using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.CDATS.Entities
{
    [DataContract]
    [Serializable]
    public class RangoCDAT
    {
        [DataMember]
        public Int64? cod_rango { get; set; }
        [DataMember]
        public string cod_lineacdat { get; set; }
        [DataMember]
        public int? tipo_tope { get; set; }
        [DataMember]
        public string minimo { get; set; }
        [DataMember]
        public string maximo { get; set; }
        [DataMember]
        public string descripcion { get; set; }

    }
}