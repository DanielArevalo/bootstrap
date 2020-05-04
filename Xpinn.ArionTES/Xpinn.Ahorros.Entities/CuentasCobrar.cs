using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xpinn.Ahorros.Entities
{
    [DataContract]
    [Serializable]
    public class CuentasCobrar
    {
        [DataMember]
        public String id_cuenta_cobrar { get; set; }
        [DataMember]
        public string num_cuenta_producto { get; set; }
        [DataMember]
        public DateTime? fecha { get; set; }
        [DataMember]
        public string cod_ope { get; set; }
        [DataMember]
        public Decimal valor { get; set; }
        [DataMember]
        public Decimal saldo { get; set; }
        [DataMember]
        public Int32 estado { get; set; }
        [DataMember]
        public Int64? cod_ope_anular { get; set; }
        [DataMember]
        public Int64? tipo_tran { get; set; }
        [DataMember]
        public Int64? num_tran_tarjeta { get; set; }

    }
}
