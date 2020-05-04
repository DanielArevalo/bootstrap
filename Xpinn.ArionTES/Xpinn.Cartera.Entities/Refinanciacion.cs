using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Cartera.Entities
{
    [DataContract]
    [Serializable]
    public class Refinanciacion
    {
        [DataMember]
        public Int64 idrefinancia { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public Int64 cod_deudor { get; set; }
        [DataMember]
        public DateTime fecha_refinancia { get; set; }
        [DataMember]
        public int numero_cuotas_ant { get; set; }
        [DataMember]
        public DateTime fecha_proximo_pago_ant { get; set; }
        [DataMember]
        public int valor_cuota_ant { get; set; }
        [DataMember]
        public DateTime fecha_primerpago_ant { get; set; }
        [DataMember]
        public DateTime fecha_vencimiento_ant { get; set; }
        [DataMember]
        public int monto_aprobado_ant { get; set; }
        [DataMember]
        public int saldo_capital_ant { get; set; }
        [DataMember]
        public int cuotas_pagadas_ant { get; set; }
        [DataMember]
        public int plazo_ref { get; set; }
        [DataMember]
        public DateTime fecha_prox_pago_ref { get; set; }
        [DataMember]
        public Decimal cuota_ref { get; set; }
        [DataMember]
        public DateTime fecha_vencimiento_ref { get; set; }
        [DataMember]
        public Decimal valor_pago { get; set; }
        [DataMember]
        public Decimal valor_refinancia { get; set; }
        [DataMember]
        public Int64 cod_usuario { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public List<AtributosRefinanciar> lstAtributosRefinanciar { get; set; }
        [DataMember]
        public long cod_linea_resstruc { get; set; }
        [DataMember]
        public int cod_nueva_periodicidad { get; set; }
    }

    [DataContract]
    [Serializable]
    public class AtributosRefinanciar
    {
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public Int64 cod_atr { get; set; }
        [DataMember]
        public string nom_atr { get; set; }
        [DataMember]
        public Decimal valor { get; set; }
        [DataMember]
        public Int64 refinanciar { get; set; }

    }
}