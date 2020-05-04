using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad Credito
    /// </summary>
    [DataContract]
    [Serializable]
    public class TDCredito
    {        
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public String numero_obligacion { get; set; }
        [DataMember]
        public int estados { get; set; }
        [DataMember]
        public String nombre_mod { get; set; }
        [DataMember]
        public Int64 cod_deudor { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string tipo_identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string cod_nomina { get; set; }
        [DataMember]
        public int edad { get; set; } // edad deudor
        [DataMember]
        public string oficina { get; set; }
        [DataMember]
        public string cod_linea_credito { get; set; }
        [DataMember]
        public string nom_linea_credito { get; set; }
        [DataMember]
        public string linea_credito { get; set; }
        [DataMember]
        public string linea_credito_num { get; set; }
        [DataMember]
        public decimal monto { get; set; }
        [DataMember]
        public decimal? monto_desembolsado { get; set; }
        [DataMember]
        public Int64 plazo { get; set; }
        [DataMember]
        public string periodicidad { get; set; }
        [DataMember]
        public decimal valor_cuota { get; set; }
        [DataMember]
        public string forma_pago { get; set; }
        [DataMember]
        public string nomforma_pago { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string nomestado { get; set; }
        [DataMember]
        public Int64 codigo_oficina { get; set; }
        [DataMember]
        public Int64 codigo_cliente { get; set; }
        [DataMember]
        public DateTime? fecha_solicitud { get; set; }
        [DataMember]
        public string fecha_solicitud_string { get; set; }
        [DataMember]
        public DateTime fecha_vencimiento { get; set; }
        [DataMember]
        public DateTime fecha_ultimo_pago { get; set; }
        [DataMember]
        public DateTime fecha_desembolso { get; set; }
        [DataMember]
        public DateTime? fecha_desembolso_nullable { get; set; }
        [DataMember]
        public decimal monto_solicitado { get; set; }
        [DataMember]
        public decimal monto_aprobado { get; set; }
        [DataMember]
        public decimal saldo_capital { get; set; }
        [DataMember]
        public Int64 otros_saldos { get; set; }
        [DataMember]
        public Int64 calificacion_promedio { get; set; }
        [DataMember]
        public Int64 calificacion_cliente { get; set; }
        [DataMember]
        public Int64 numero_cuotas { get; set; }
        [DataMember]
        public Int64 cuotas_pagadas { get; set; }
        [DataMember]
        public DateTime fecha_prox_pago { get; set;}
        [DataMember]
        public DateTime? fecha_prim_pago { get; set; } 
        [DataMember]
        public DateTime fecha_inicio { get; set; }
        [DataMember]
        public string fecha_prox_pago_string { get; set; }
        [DataMember]
        public Int64 porc_renovacion_cuotas { get; set; }
        [DataMember]
        public Int64 porc_renovacion_montos { get; set; }
        [DataMember]
        public Int64 ult_valor_pagado { get; set; }
        [DataMember]
        public decimal valor_a_pagar { get; set; }
        [DataMember]
        public decimal valor_CE { get; set; }
        [DataMember]
        public decimal valor_a_pagar_CE { get; set; }
        [DataMember]
        public decimal total_a_pagar { get; set; }
        [DataMember]
        public Int64 idinforme { get; set; }
        [DataMember]
        public Int64 dias_mora { get; set; }
        [DataMember]
        public Int64 saldo_mora { get; set; }
        [DataMember]
        public Int64 saldo_atributos_mora { get; set; }
        [DataMember]
        public string estado_juridico { get; set; }
        [DataMember]
        public DateTime fecha_corte { get; set; }
        [DataMember]
        public string fecha_corte_string { get; set; }
        [DataMember]
        public DateTime? fecha_aprobacion { get; set; }
        [DataMember]
        public Int64 zona { get; set; }
        [DataMember]
        public Int64 dias_mora_habeas { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public string observacion { get; set; }
        [DataMember]
        public string descrpcion { get; set; }
        [DataMember]
        public Int64 tipo_movimiento { get; set; }
        [DataMember]
        public Int64 tipo_tran { get; set; }
        [DataMember]
        public string moneda { get; set; }
        [DataMember]
        public int cod_moneda { get; set; }
        [DataMember]
        public string calculo_atr { get; set; }
        [DataMember]
        public decimal tasa { get; set; }
        [DataMember]
        public string desc_tasa { get; set; }
        [DataMember]
        public Int64 tipo_tasa { get; set; }
        [DataMember]
        public Int64 tipo_historico { get; set; }
        [DataMember]
        public decimal desviacion { get; set; }
        [DataMember]
        public Int64 cobra_mora { get; set; }
        [DataMember]
        public string fechaacta { get; set; }
        [DataMember]
        public Int64 acta { get; set; }
        [DataMember]
        public Int64 aprobador { get; set; }
        [DataMember]
        public DateTime fecha_aproba { get; set; }
        [DataMember]
        public string Codeudor { get; set; }
        [DataMember]
        public string NombreCodeudor { get; set; }
        [DataMember]
        public int? cod_clasifica { get; set; }
        [DataMember]
        public int? num_comp { get; set; }
        [DataMember]
        public int? tipo_comp { get; set; }
        [DataMember]
        public Int64? cod_proveedor { get; set; }
        [DataMember]
        public string idenprov { get; set; }
        [DataMember]
        public string nomprov { get; set; }
        [DataMember]
        public Int64? num_preimpreso { get; set; }
        [DataMember]
        public string autorizacion { get; set; }
        [DataMember]
        public string universidad { get; set; }
        [DataMember]
        public string semestre { get; set; }
        [DataMember]
        public Int64 dias_ajuste { get; set; }

        //agregados para reporte
        [DataMember]
        public string num_pagare { get; set; }
        [DataMember]
        public string clasificacion { get; set; }
        [DataMember]
        public string modalidad_pag_intereses { get; set; }
        [DataMember]
        public string tipo_garantia { get; set; }
        [DataMember]
        public string categoria { get; set; }
        [DataMember]
        public decimal? por_provision { get; set; }
        [DataMember]
        public decimal? provision_capital { get; set; }
        [DataMember]
        public decimal? tasa_int_corr { get; set; }
        [DataMember]
        public string formato_tasa_int { get; set; }
        [DataMember]
        public Int32 dias_causados { get; set; }
        [DataMember]
        public decimal? int_cte_causado { get; set; }
        [DataMember]
        public decimal? provisio_int_cte { get; set; }
        [DataMember]
        public decimal? int_orden { get; set; }
        [DataMember]
        public decimal? tasa_int_mora { get; set; }
        [DataMember]
        public string formato_tasa_int_mor { get; set; }
        [DataMember]
        public Int32? cod_cli { get; set; }
        [DataMember]
        public decimal? valorCap { get; set; }
        [DataMember]
        public decimal? otros_cobros { get; set; }
        [DataMember]
        public decimal? provision_otros { get; set; }
        [DataMember]
        public string telefonos { get; set; }
        //AGREGADO
        [DataMember]
        public int idcontrol { get; set; }        
        [DataMember]
        public string codtipoproceso { get; set; }
        [DataMember]
        public DateTime? fechaproceso { get; set; }       
        [DataMember]
        public int? cod_motivo { get; set; }
        [DataMember]
        public string observaciones { get; set; }
        [DataMember]
        public string anexos { get; set; }
        [DataMember]
        public int? nivel { get; set; }
        [DataMember]
        public DateTime? fecha_consulta_dat { get; set; }
        /// <summary>
        /// AGREGADO
        /// </summary>
        [DataMember]
        public Int64 numero_radicacion2 { get; set; }
        [DataMember]
        public long numero_solicitud { get; set; }
        [DataMember]
        public long numero_credito { get; set; }
        [DataMember]
        public long numero_avance { get; set; }
        [DataMember]
        public long cuota_solicitada { get; set; }
        [DataMember]
        public string empresa { get; set; }

        [DataMember]
        public string estadocierre { get; set; }

        [DataMember]
        public DateTime fecha_cierre { get; set; }

    }
 }