using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Programado.Entities
{
    [DataContract]
    [Serializable]
    public class LineasProgramado
    {
        [DataMember]
        public string cod_linea_programado { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public int tipo_liquidacion { get; set; }
        [DataMember]
        public decimal cuota_minima { get; set; }
        [DataMember]
        public int plazo_minimo { get; set; }
        [DataMember]
        public  decimal saldo_mínimo { get; set; }
        [DataMember]
        public int? calculo_tasa { get; set; }
        [DataMember]
        public decimal? tasa_interes { get; set; }
        [DataMember]
        public int? cod_tipo_tasa { get; set; }
        [DataMember]
        public int? tipo_historico { get; set; }
        [DataMember]
        public decimal? desviacion { get; set; }
        [DataMember]
        public int? retiro_parcial { get; set; }
        [DataMember]
        public decimal? por_retiro_maximo { get; set; }
        [DataMember]
        public decimal? saldo_minimo { get; set; }
        [DataMember]
        public int maneja_cuota_extra { get; set; }

        [DataMember]
        public decimal? cuota_extra_min { get; set; }

        [DataMember]
        public decimal? cuota_extra_max { get; set; }

        [DataMember]
        public int? cruza { get; set; }
        [DataMember]
        public decimal? porcentaje_cruce { get; set; }
        [DataMember]
        public decimal? retencion { get; set; }
        [DataMember]
        public decimal? cuota_nomina { get; set; }
        [DataMember]
        public int? por_retiro_plazo { get; set; }
        [DataMember]
        public string opcion_saldo { get; set; }
        [DataMember]
        public decimal? por_retiro_minimo { get; set; }
        [DataMember]
        public decimal? valor_maximo_retiro { get; set; }
        [DataMember]
        public decimal? por_int_dism { get; set; }
        [DataMember]
        public int? porpla_ret_t { get; set; }
        [DataMember]
        public int? pormont_ret_t { get; set; }
        [DataMember]
        public int? dias_gracia { get; set; }
        [DataMember]
        public int? prioridad { get; set; }
        [DataMember]
        public int estado { get; set; }
        [DataMember]
        public int? aplica_retencion { get; set; }

        //AGREGADO

        [DataMember]
        public string nomestado { get; set; }
        [DataMember]
        public string nomTipoLiquidacion { get; set; }

        [DataMember]
        public int? interes_por_cuenta { get; set; }


        [DataMember]
        public int? tipo_saldo_int { get; set; }
        [DataMember]
        public int? periodicidad_int { get; set; }

        [DataMember]
        public int cod_moneda { get; set; }

        [DataMember]
        public int plazo_maximo { get; set; }


        //tASA RENOVACION
        [DataMember]
        public int interes_renovacion { get; set; }

        public int? calculo_tasa_ren { get; set; }
        [DataMember]
        public decimal? tasa_interes_ren { get; set; }
        [DataMember]
        public int? cod_tipo_tasa_ren { get; set; }
        [DataMember]
        public int? tipo_historico_ren { get; set; }
        [DataMember]
        public decimal? desviacion_ren { get; set; }

    }
}
