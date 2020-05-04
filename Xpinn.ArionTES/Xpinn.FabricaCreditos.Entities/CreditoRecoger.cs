using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad CreditoRecoger
    /// </summary>
    [DataContract]
    [Serializable]
    public class CreditoRecoger
    {
        [DataMember]
        public Int64 numero_credito { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public string linea_credito { get; set; }
        [DataMember]
        public decimal monto { get; set; }
        [DataMember]
        public decimal saldo_capital { get; set; }
        [DataMember]
        public decimal interes_corriente { get; set; }
        [DataMember]
        public decimal interes_mora { get; set; }
        [DataMember]
        public decimal otros { get; set; }
        [DataMember]
        public decimal seguro { get; set; }
        [DataMember]
        public decimal leymipyme { get; set; }
        [DataMember]
        public decimal iva_leymipyme { get; set; }
        [DataMember]
        public Boolean recoger { get; set; }
        [DataMember]
        public decimal valor_recoge { get; set; }
        [DataMember]
        public DateTime fecha_pago { get; set; }
        [DataMember]
        public decimal valor_total { get; set; }
        [DataMember]
        public string formapago { get; set; }
        [DataMember]
        public int cuotas_pagadas { get; set; }
        [DataMember]
        public int marcar { get; set; }
        [DataMember]
        public long orden_servicio { get; set; }
        [DataMember]
        public Int64? cod_deudor { get; set; }
        [DataMember]
        public bool solo_borrar { get; set; }
        [DataMember]
        public int cantidad_nominas { get; set; }
        [DataMember]
        public decimal valor_nominas { get; set; }
    }
}