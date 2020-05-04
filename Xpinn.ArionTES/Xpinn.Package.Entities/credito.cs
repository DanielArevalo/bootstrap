using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Package.Entities
{
    [DataContract]
    [Serializable]
    public class Credito
    {
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public string numero_obligacion { get; set; }
        [DataMember]
        public int cod_oficina { get; set; }
        [DataMember]
        public Int64 cod_deudor { get; set; }
        [DataMember]
        public string cod_linea_credito { get; set; }
        [DataMember]
        public decimal monto_solicitado { get; set; }
        [DataMember]
        public decimal? monto_aprobado { get; set; }
        [DataMember]
        public decimal? monto_desembolsado { get; set; }
        [DataMember]
        public int cod_moneda { get; set; }
        [DataMember]
        public DateTime fecha_solicitud { get; set; }
        [DataMember]
        public DateTime? fecha_aprobacion { get; set; }
        [DataMember]
        public DateTime? fecha_desembolso { get; set; }
        [DataMember]
        public DateTime? fecha_primerpago { get; set; }
        [DataMember]
        public decimal numero_cuotas { get; set; }
        [DataMember]
        public decimal? cuotas_pagadas { get; set; }
        [DataMember]
        public decimal? cuotas_pendientes { get; set; }
        [DataMember]
        public string cod_periodicidad { get; set; }
        [DataMember]
        public int tipo_liquidacion { get; set; }
        [DataMember]
        public decimal? valor_cuota { get; set; }
        [DataMember]
        public string forma_pago { get; set; }
        [DataMember]
        public DateTime? fecha_ultimo_pago { get; set; }
        [DataMember]
        public DateTime? fecha_vencimiento { get; set; }
        [DataMember]
        public DateTime? fecha_proximo_pago { get; set; }
        [DataMember]
        public int? tipo_gracia { get; set; }
        [DataMember]
        public Int64? cod_atr_gra { get; set; }
        [DataMember]
        public decimal? periodo_gracia { get; set; }
        [DataMember]
        public int? cod_clasifica { get; set; }
        [DataMember]
        public decimal? saldo_capital { get; set; }
        [DataMember]
        public decimal otros_saldos { get; set; }
        [DataMember]
        public Int64? cod_asesor_com { get; set; }
        [DataMember]
        public string tipo_credito { get; set; }
        [DataMember]
        public Int64? num_radic_origen { get; set; }
        [DataMember]
        public decimal? vrreestructurado { get; set; }
        [DataMember]
        public int? cod_empresa { get; set; }
        [DataMember]
        public int? cod_pagaduria { get; set; }
        [DataMember]
        public decimal gradiente { get; set; }
        [DataMember]
        public DateTime? fecha_inicio { get; set; }
        [DataMember]
        public int? dias_ajuste { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public DateTime fechacreacion { get; set; }
        [DataMember]
        public string usuariocreacion { get; set; }
        [DataMember]
        public DateTime? fecultmod { get; set; }
        [DataMember]
        public string usuultmod { get; set; }
        [DataMember]
        public decimal? tir { get; set; }
        [DataMember]
        public int? pago_especial { get; set; }
        [DataMember]
        public string universidad { get; set; }
        [DataMember]
        public string semestre { get; set; }
        [DataMember]
        public DateTime? fecreestructurado { get; set; }
        [DataMember]
        public int? reestructurado { get; set; }
    }
}
