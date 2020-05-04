using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Package.Entities
{
    [DataContract]
    [Serializable]
    public class cuotasextras
    {
        [DataMember]
        public int cod_cuota { get; set; }
        [DataMember]
        public int numero_radicacion { get; set; }
        [DataMember]
        public DateTime fecha_pago { get; set; }
        [DataMember]
        public decimal? valor { get; set; }
        [DataMember]
        public decimal? valor_capital { get; set; }
        [DataMember]
        public decimal? valor_interes { get; set; }
        [DataMember]
        public decimal? saldo_capital { get; set; }
        [DataMember]
        public decimal? saldo_interes { get; set; }
        [DataMember]
        public string forma_pago { get; set; }
    }
}