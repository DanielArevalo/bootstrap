using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities
{
    [DataContract]
    [Serializable]
    public class Reporte
    {

        [DataMember]
        public Int64 icodigo { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string Nombres { get; set; }
        [DataMember]
        public string Apellidos { get; set; }
        [DataMember]
        public string Nombrecliente { get; set; }
        [DataMember]
        public Int64 NumRadicacion { get; set; }
        [DataMember]
        public string pagare { get; set; }
        [DataMember]
        public decimal saldo_capital { get; set; }
        [DataMember]
        public string dias_mora { get; set; }
        [DataMember]
        public decimal valor_cuota { get; set; }
        [DataMember]
        public decimal pendite_cuota { get; set; }
        [DataMember]
        public Double garantia_comunitaria { get; set; }
        [DataMember]
        public DateTime Fecha_cuota { get; set; }
        [DataMember]
        public string direccion_oficina { get; set; }
        [DataMember]
        public string direccion_empresa { get; set; }
        [DataMember]
        public string barrio_oficina { get; set; }
        [DataMember]
        public string barrio_empresa { get; set; }
        [DataMember]
        public string telefono_empresa { get; set; }
        [DataMember]
        public string direccion_negocio { get; set; }
        [DataMember]
        public string barrio_negocio { get; set; }
        [DataMember]
        public string telefono_negocio { get; set; }
        [DataMember]
        public string direccion_residencia { get; set; }
        [DataMember]
        public string barrio_residencia { get; set; }
        [DataMember]
        public string telefono_residencia { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string ciudad { get; set; }
        [DataMember]
        public string barrio { get; set; }
        [DataMember]
        public string celular { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public Int64 idpromotor { get; set; }
        [DataMember]
        public decimal valor_a_pagar { get; set; }
        [DataMember]
        public Int64 dias_amortiza { get; set; }
        [DataMember]
        public decimal ValorInteres { get; set; }
        [DataMember]
        public decimal ValorOtros { get; set; }
        [DataMember]
        public decimal ValorMora { get; set; }
        [DataMember]
        public decimal ValorCapital { get; set; }
        [DataMember]
        public int Acuerdo { get; set; }
        //////////repor poliza//////
        [DataMember]
        public decimal porcentaje { get; set; }
        [DataMember]
        public Int64 valor_prima { get; set; }
        [DataMember]
        public string oficina { get; set; }
        [DataMember]
        public string tipoplan { get; set; }
        [DataMember]
        public Int64 monto_pago { get; set; }
        [DataMember]
        public string cedula_asesor { get; set; }
        [DataMember]
        public string nombre_asesor { get; set; }
        [DataMember]
        public string fehca_pago { get; set; }
        [DataMember]
        public string fecha_vigencia { get; set; }
        [DataMember]
        public Int64 codigo { get; set; }
        [DataMember]
        public Int64 cuenta { get; set; }
        [DataMember]
        public Int64 credito { get; set; }
        [DataMember]
        public Int64 saldo { get; set; }
        [DataMember]
        public string linea { get; set; }
        [DataMember]
        public string fecha_desenbolso { get; set; }
        [DataMember]
        public string fecha_proximo_pago { get; set; }
        [DataMember]
        public DateTime fecha_proximo { get; set; }
        [DataMember]
        public DateTime fecha_pago { get; set; }
        [DataMember]
        public decimal monto_aprobado { get; set; }
        [DataMember]
        public Int64 cantidad { get; set; }
        [DataMember]
        public Int64 numero_cuotas { get; set; }
        [DataMember]
        public Int64 cuotas_pagadas { get; set; }
        [DataMember]
        public Int64 codigo_asesor { get; set; }
        [DataMember]
        public string telefono_oficina { get; set; }

        //////cierre oficina        
        [DataMember]
        public DateTime fechacierre { get; set; }
        [DataMember]
        public string numero_credito { get; set; }
        [DataMember]
        public string numero_colocacion_mes { get; set; }
        [DataMember]
        public string monto_colocacion_mes { get; set; }
        [DataMember]
        public string total_mora { get; set; }
        [DataMember]
        public string saldo_mora { get; set; }
        [DataMember]
        public string mora_menor_30 { get; set; }
        [DataMember]
        public string monto_menor_30 { get; set; }
        [DataMember]
        public string mora_mayor_30 { get; set; }
        [DataMember]
        public string monto_mayor_30 { get; set; }
        [DataMember]
        public string saldo_cierre { get; set; }
        [DataMember]
        public string email { get; set; }

        [DataMember]
        public string email_codeudor { get; set; }


        /////////////////////
        [DataMember]
        public string codigo_oficina { get; set; }
        [DataMember]
        public string valor_pagar { get; set; }
        [DataMember]
        public Int64 valor_pago { get; set; }
        [DataMember]
        public string cod_codeudor { get; set; }
        [DataMember]
        public string identificacion_codeudor { get; set; }
        [DataMember]
        public string nombre_codeudor { get; set; }
        [DataMember]
        public string direcion_codeudor { get; set; }
        [DataMember]
        public string telefono_codeudor { get; set; }
        [DataMember]
        public string telefono_empresa_codeudor { get; set; }
        [DataMember]
        public string direcion_corespondecia_codeudor { get; set; }
        [DataMember]
        public string telefono_correspondecia_codeudor { get; set; }
        [DataMember]
        public string direccion_correspondencia { get; set; }
        [DataMember]
        public string telefono_correspondencia { get; set; }
        [DataMember]
        public DateTime Fecha_aprobacion { get; set; }
        [DataMember]
        public string cod_linea { get; set; }
        [DataMember]
        public DateTime Fecha_vencimiento { get; set; }
        [DataMember]
        public DateTime? Fecha_ult_pago { get; set; }
        [DataMember]
        public Int64 plazo { get; set; }
        [DataMember]
        public string periodicidad { get; set; }

        // REPORTE DE MORA
        [DataMember]
        public Int64 valor_acuerdo { get; set; }
        [DataMember]
        public string fecha_acuerdo { get; set; }
        [DataMember]
        public DateTime fecha_acuerdorepo { get; set; }
        [DataMember]
        public string respuesta { get; set; }
        [DataMember]
        public string usuario { get; set; }
        [DataMember]
        public string fecha_acuerdostring { get; set; }
        [DataMember]
        public string observacion { get; set; }
        [DataMember]
        public Int64 dias_mora_u_cierre { get; set; }

        // Reporte pago especial
        [DataMember]
        public long numero_radicacion { get; set; }
        [DataMember]
        public string nombress { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public DateTime fecha_solicitud { get; set; }
        [DataMember]
        public DateTime fecha_aprobacion { get; set; }
        [DataMember]
        public long monto { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string cod_categoria { get; set; }
        [DataMember]
        public string cod_clasificacion { get; set; }
        [DataMember]
        public string descripcion_clasificacion { get; set; }
        [DataMember]
        public string cod_categoria_cli { get; set; }
        [DataMember]
        public decimal tasa_interes { get; set; }
        [DataMember]
        public int cod_atr { get; set; }
        [DataMember]
        public string nom_atr { get; set; }
        [DataMember]
        public decimal valor_causado { get; set; }
        [DataMember]
        public decimal valor_orden { get; set; }
        [DataMember]
        public decimal saldo_causado { get; set; }
        [DataMember]
        public decimal saldo_orden { get; set; }
        [DataMember]
        public int dias_causados { get; set; }
        [DataMember]
        public decimal saldo_causado_ant { get; set; }

        [DataMember]
        public decimal porc_provision { get; set; }
        [DataMember]
        public decimal valor_provision { get; set; }
        [DataMember]
        public decimal base_provision { get; set; }
        [DataMember]
        public decimal aporte_resta { get; set; }
        [DataMember]
        public decimal diferencia_provision { get; set; }
        [DataMember]
        public decimal diferencia_actual { get; set; }
        [DataMember]
        public decimal diferencia_anterior { get; set; }
        [DataMember]
        public decimal valor_garantia { get; set; }
        [DataMember]
        public DateTime fecha_afiliacion { get; set; }
        [DataMember]
        public string razon_social { get; set; }
        [DataMember]
        public DateTime fecha_apertura { get; set; }
        [DataMember]
        public string tipo_producto { get; set; }
        [DataMember]
        public string forma_pago { get; set; }

        [DataMember]
        public decimal monto_desembolsado { get; set; }
        [DataMember]
        public string empresa_recaudo { get; set; }

        [DataMember]
        public string empresa_recaudo_code { get; set; }
        //////////repor cuadre saldos//////
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public int centro_costo { get; set; }
        [DataMember]
        public decimal saldo_operativo { get; set; }
        [DataMember]
        public decimal saldo_contable { get; set; }
        [DataMember]
        public decimal valor_movimiento { get; set; }
        [DataMember]
        public string nom_tipo_garantia { get; set; }
    }
}

