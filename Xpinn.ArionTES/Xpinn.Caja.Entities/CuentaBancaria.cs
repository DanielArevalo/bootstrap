using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Caja.Entities
{
    [DataContract]
    [Serializable]
    public class CuentaBancaria
    {
        [DataMember]
        public Int64 IdCtaBancaria { get; set; }
        [DataMember]
        public Int64 cod_banco { get; set; }
        [DataMember]
        public string num_cuenta { get; set; }
        [DataMember]
        public Int64? tipo_cuenta { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string estado { get; set; }
    }
}
