using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class GrupoLineaAporte
    {
        [DataMember]
        public Int64 cod_linea_aporte { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public Int64 tipo_aporte { get; set; }
        [DataMember]
        public Int64 tipo_cuota { get; set; }
        [DataMember]
        public Int64 valor_cuota_minima { get; set; }
        [DataMember]
        public Int64 valor_cuota_maximo { get; set; }
        [DataMember]
        public int obligatorio { get; set; }
        [DataMember]
        public Int64 tipo_liquidacion { get; set; }
        [DataMember]
        public Int64 min_valor_pago { get; set; }
        [DataMember]
        public Int64 min_valor_retiro { get; set; }
        [DataMember]
        public Int64 max_valor_retiro { get; set; }
        [DataMember]
        public Int64 saldo_minimo { get; set; }
        [DataMember]
        public Int64 valor_cierre { get; set; }
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
        public String estado_Linea { get; set; }
        [DataMember]
        public int distribuye { get; set; }
        [DataMember]
        public decimal porcentaje_distrib { get; set; }
        [DataMember]
        public Int64 saldo_minimo_Liqui { get; set; }
        [DataMember]
        public Int64 idgrupo { get; set; }
        [DataMember]
        public decimal porcentaje { get; set; }
        [DataMember]
        public int principal { get; set; }
        [DataMember]
        public decimal cap_minimo_irreduptible { get; set; }
        [DataMember]
        public int permite_pagoprod { get; set; }
        [DataMember]
        public int tipo_distribucion { get; set; }
        [DataMember]
        public bool rpta { get; set; }
        //agregados
        [DataMember]
        public Int64 forma_pago { get; set; }
        [DataMember]
        public string periodicidad { get; set; }

        //liquidación de interes

        [DataMember]
        public int? tipo_saldo_int { get; set; }
        [DataMember]
        public int? cod_periodicidad_int { get; set; }
        [DataMember]
        public int? dias_gracia { get; set; }

        [DataMember]
        public int? prioridad { get; set; }
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
        public int? retencion_por_cuenta { get; set; }
        [DataMember]
        public int? saldo_minimo_liq { get; set; }

        //Agregados

        [DataMember]
        public int? Pago_Intereses { get; set; }
        [DataMember]
        public Int64? cod_clasificacion { get; set; }

        //Cargue masivo
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public Int64 numero_aporte { get; set; }
        [DataMember]
        public decimal cuota { get; set; }
        [DataMember]
        public DateTime fechaAct { get; set; }
        [DataMember]
        public Int64 numero_aporteR { get; set; }
        [DataMember]
        public Int64? cod_linea_liqui_rev { get; set; }

        //Agregado para porcentaje máximo de saldo a retirar
        [DataMember]
        public Int64? max_porcentaje_saldo { get; set; }

        //Parametro Acepta beneficiarios
        [DataMember]
        public int? beneficiarios { get; set; }
        [DataMember]
        public int? alerta { get; set; }

    }

}

