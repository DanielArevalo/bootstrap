using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Ahorros.Entities
{
    [DataContract]
    [Serializable]
    public class LineaAhorro
    {
        [DataMember]
        public string cod_linea_ahorro { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int estado { get; set; }
        [DataMember]
        public int cod_moneda { get; set; }
        [DataMember]
        public int? prioridad { get; set; }
        [DataMember]
        public decimal valor_apertura { get; set; }
        [DataMember]
        public decimal saldo_minimo { get; set; }
        [DataMember]
        public decimal movimiento_minimo { get; set; }
        [DataMember]
        public decimal maximo_retiro_diario { get; set; }
        [DataMember]
        public decimal? retiro_max_efectivo { get; set; }
        [DataMember]
        public decimal? retiro_min_cheque { get; set; }
        [DataMember]
        public int? requiere_libreta { get; set; }
        [DataMember]
        public decimal? valor_libreta { get; set; }
        [DataMember]
        public int? num_desprendibles_lib { get; set; }
        [DataMember]
        public int? cobra_primera_libreta { get; set; }
        [DataMember]
        public int? cobra_perdida_libreta { get; set; }
        [DataMember]
        public int? canje_automatico { get; set; }
        [DataMember]
        public int? dias_canje { get; set; }
        [DataMember]
        public int? inactivacion_automatica { get; set; }
        [DataMember]
        public int? dias_inactiva { get; set; }
        [DataMember]
        public int? cobro_cierre { get; set; }
        [DataMember]
        public decimal? cierre_valor { get; set; }
        [DataMember]
        public int? cierre_dias { get; set; }
        [DataMember]
        public int? tipo_saldo_int { get; set; }
        [DataMember]
        public int? cod_periodicidad_int { get; set; }
        [DataMember]
        public int? dias_gracia { get; set; }
        [DataMember]
        public int? realiza_provision { get; set; }
        [DataMember]
        public decimal? interes_dia_retencion { get; set; }
        [DataMember]
        public int? interes_por_cuenta { get; set; }
        [DataMember]
        public int? forma_tasa { get; set; }
        [DataMember]
        public int? tipo_historico { get; set; }
        [DataMember]
        public decimal? desviacion { get; set; }
        [DataMember]
        public int? tipo_tasa { get; set; }
        [DataMember]
        public decimal? tasa { get; set; }
        [DataMember]
        public DateTime fechacreacion { get; set; }
        [DataMember]
        public string usuariocreacion { get; set; }
        [DataMember]
        public DateTime fecultmod { get; set; }
        [DataMember]
        public string usuultmod { get; set; }
        [DataMember]
        public int? retencion_por_cuenta { get; set; }
         [DataMember]
        public int? saldo_minimo_liq { get; set; }
        [DataMember]
        public int debito_automatico { get; set; }

    }
}
