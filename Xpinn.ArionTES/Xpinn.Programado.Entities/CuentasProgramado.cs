using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.Programado.Entities
{
    [DataContract]
    [Serializable]
    public class CuentasProgramado
    {
        [DataMember]
        public decimal? saldo_total { get; set; }
        [DataMember]
        public decimal? saldo_canje { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string numero_cuenta { get; set; }
        [DataMember]
        public string cod_linea_ahorro { get; set; }
        [DataMember]
        public string nom_linea { get; set; }
        [DataMember]
        public decimal tasa { get; set; }
        [DataMember]
        public int? tipo_tasa { get; set; }
        [DataMember]
        public int? forma_tasa { get; set; }
        [DataMember]
        public int? cod_forma_pago { get; set; }
        [DataMember]
        public decimal? saldo_intereses { get; set; }
        [DataMember]
        public int? retencion { get; set; }
        [DataMember]
        public string observaciones { get; set; }
        [DataMember]
        public string numero_programado { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public Int64 modalidad { get; set; }
        [DataMember]
        public Int64 cod_destino { get; set; }
        [DataMember]
        public string nom_oficina { get; set; }
        [DataMember]
        public string cod_linea_programado { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public DateTime fecha_apertura { get; set; }
        [DataMember]
        public DateTime fecha_primera_cuota { get; set; }
        [DataMember]
        public DateTime fecha_cierre { get; set; }
        [DataMember]
        public decimal valor_cuota { get; set; }
        [DataMember]
        public int plazo { get; set; }
        [DataMember]
        public Int64 cod_periodicidad { get; set; }
        [DataMember]
        public DateTime fecha_ultimo_pago { get; set; }
        [DataMember]
        public DateTime fecha_proximo_pago { get; set; }
        [DataMember]
        public decimal saldo { get; set; }
        [DataMember]
        public int forma_pago { get; set; }
        [DataMember]
        public int cod_empresa { get; set; }
        [DataMember]
        public int cuotas_pagadas { get; set; }
        [DataMember]
        public int calculo_tasa { get; set; }
        [DataMember]
        public decimal tasa_interes { get; set; }
        [DataMember]
        public int cod_tipo_tasa { get; set; }
        [DataMember]
        public int tipo_historico { get; set; }
        [DataMember]
        public decimal desviacion { get; set; }
        [DataMember]
        public DateTime fecha_interes { get; set; }
        [DataMember]
        public decimal total_interes { get; set; }
        [DataMember]
        public decimal total_retencion { get; set; }
        [DataMember]
        public int cod_motivo_apertura { get; set; }
        [DataMember]
        public int estado { get; set; }
        [DataMember]
        public int codopcion { get; set; }
        [DataMember]
        public List<Beneficiario> lstBeneficiarios { get; set; }


        [DataMember]
        public List<ProgramadoCuotasExtras> lstcuotasExtras { get; set; }


        //AGREGADO
        [DataMember]
        public string nomlinea { get; set; }
        [DataMember]
        public string nomoficina { get; set; }
        [DataMember]
        public string nommotivo_progra { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string cod_nomina { get; set; }
        [DataMember]
        public string nomforma_pago { get; set; }
        [DataMember]
        public string nom_estado { get; set; }
        [DataMember]
        public string nom_periodicidad { get; set; }

        //agregado
        [DataMember]
        public decimal? V_Traslado { get; set; }
        [DataMember]
        public decimal? valor_gmf { get; set; }
        //agregado
        [DataMember]
        public int codigo_inicial { get; set; }
        [DataMember]
        public int codigo_final { get; set; }
        [DataMember]
        public string identificacion_inicial { get; set; }
        [DataMember]
        public string identificacion_final { get; set; }
        [DataMember]
        public string empresa { get; set; }
        [DataMember]
        public string CiudadResidencia { get; set; }
        [DataMember]
        public string numero_cuenta_final { get; set; }
        [DataMember]
        public string apellidos { get; set; }
        [DataMember]
        public DateTime fec_realiza { get; set; }

        //agregado
        [DataMember]
        public int numero_radicacion { get; set; }
        [DataMember]
        public string nombre_titular { get; set; }
        [DataMember]
        public int monto { get; set; }
        [DataMember]
        public int cuota { get; set; }
        [DataMember]
        public int valor_mora { get; set; }

        //agregado para numeracion cuentas
        [DataMember]
        public int posicion { get; set; }
        [DataMember]
        public string valor { get; set; }
        [DataMember]
        public int longitud { get; set; }
        [DataMember]
        public string alinear { get; set; }
        [DataMember]
        public string caracter_llenado { get; set; }
        [DataMember]
        public int tipo_campo { get; set; }
        [DataMember]
        public int tipo_producto { get; set; }
        [DataMember]
        public decimal valornum { get; set; }

        [DataMember]
        public int opcion { get; set; }
        [DataMember]
        public DateTime fecha_vencimiento { get; set; }

        //agregado para reporte 
        [DataMember]
        public string ciudad { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public int principal { get; set; }

        //Agregado para extracto
        [DataMember]
        public decimal pendiente { get; set; }
        [DataMember]
        public DateTime fecha_cuota { get; set; }
        [DataMember]
        public decimal valor_total { get; set; }
        [DataMember]
        public long? cod_asesor { get; set; }

        [DataMember]
        public decimal valor_acumulado { get; set; }
        [DataMember]
        public decimal valor_total_acumu { get; set; }

        //Agregado novedad
        [DataMember]
        public DateTime fecha_empieza_cambio { get; set; }
        [DataMember]
        public decimal valor_cuota_anterior { get; set; }
        [DataMember]
        public long cod_operacion { get; set; }
        [DataMember]
        public DateTime fecha_pago { get; set; }
        [DataMember]
        public decimal valor_pago { get; set; }

        [DataMember]
        public Int16 numero_dias { get; set; }

        [DataMember]
        public Int16 tipo_calendario { get; set; }

        [DataMember]
        public string estadocierre { get; set; }

        //AtencionWeb
        [DataMember]
        public string nom_formapago { get; set; }
        [DataMember]
        public string estado_modificacion { get; set; }
        [DataMember]
        public string estado_Linea { get; set; }

        [DataMember]
        public decimal nuevo_valor_cuota { get; set; }
        [DataMember]
        public long id_novedad_cambio { get; set; }
        [DataMember]
        public DateTime? fecha_novedad_cambio { get; set; }
        [DataMember]
        public string nuevo_valor_cuota1 { get; set; }
        [DataMember]
        public decimal cuota1 { get; set; }
        [DataMember]
        public int? estado_modificacion1 { get; set; }
        [DataMember]
        public int cod_tipo_producto { get; set; }

    }


    [DataContract]
    [Serializable]
    public class MotivoProgramadoE
    {
        [DataMember]
        public Int64 Codigo { get; set; }
        [DataMember]
        public String Descripcion { get; set; }
    }


}
