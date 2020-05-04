using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Caja.Entities
{
    [DataContract]
    [Serializable]
    public class TipoMoneda
    {
        [DataMember]
        public Int64 IdMoneda { get; set; }
        [DataMember]
        public Int64 cod_moneda{ get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string simbolo { get; set; }
    }
}
