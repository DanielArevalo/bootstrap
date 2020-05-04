using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class ivdatospago
    {
        [DataMember]
        public Int64 id_venta { get; set; }
        [DataMember]
        public Int64 id_forma_pago { get; set; }
        [DataMember]
        public double valor { get; set; }
        [DataMember]
        public Int64 id_venta_realizada { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string cod_cuenta_niif { get; set; }
    }
}
