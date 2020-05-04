using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class LineaAporte
    {
        [DataMember]
        public int cod_linea_aporte { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public int tipo_aporte { get; set; }
        [DataMember]
        public int tipo_cuota { get; set; }
        [DataMember]
        public decimal valor_cuota_minima { get; set; }
        [DataMember]
        public decimal valor_cuota_maximo { get; set; }
        [DataMember]
        public int obligatorio { get; set; }
        [DataMember]
        public int tipo_liquidacion { get; set; }
        [DataMember]
        public decimal min_valor_pago { get; set; }
        [DataMember]
        public decimal min_valor_retiro { get; set; }
        [DataMember]
        public Int64 saldo_minimo { get; set; }
        [DataMember]
        public decimal valor_cierre { get; set; }
        [DataMember]
        public Int64 dias_cierre { get; set; }
        [DataMember]
        public int cruzar { get; set; }
        [DataMember]
        public decimal porcentaje_cruce { get; set; }
        [DataMember]
        public int cobra_mora { get; set; }
        [DataMember]
        public int provisionar { get; set; }
        [DataMember]
        public int permite_retiros { get; set; }
        [DataMember]
        public int permite_traslados { get; set; }
        [DataMember]
        public decimal porcentaje_minimo { get; set; }
        [DataMember]
        public decimal porcentaje_maximo { get; set; }
        [DataMember]
        public int estado { get; set; }
        [DataMember]
        public int? distribuye { get; set; }
        [DataMember]
        public double? porcentaje_distr { get; set; }
        [DataMember]
        public decimal? cap_minimo_irreduptible { get; set; }
        [DataMember]
        public int? permite_pagoprod { get; set; }
    }
}