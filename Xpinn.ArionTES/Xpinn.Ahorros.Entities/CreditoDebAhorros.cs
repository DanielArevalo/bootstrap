using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xpinn.Ahorros.Entities
{
    [DataContract]
    [Serializable]
    public class CreditoDebAhorros
    {
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public Int64 cod_cliente { get; set; }
        [DataMember]
        public string cod_linea_credito { get; set; }
        [DataMember]
        public string nom_linea { get; set; }
        [DataMember]
        public DateTime? fecha_aprobacion { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string apellidos { get; set; }
        [DataMember]
        public decimal? saldo_capital { get; set; }
        [DataMember]
        public decimal? valor_cuota { get; set; }
        [DataMember]
        public DateTime fecha_proximo_pago { get; set; }
        [DataMember]
        public DateTime fecha_ultimo_pago { get; set; }
        [DataMember]
        public String numero_cuenta { get; set; }
        [DataMember]
        public decimal? saldo_total { get; set; }
        [DataMember]
        public decimal? saldo_disponible { get; set; }
        [DataMember]
        public decimal? valor_pagar { get; set; }
    }
}
