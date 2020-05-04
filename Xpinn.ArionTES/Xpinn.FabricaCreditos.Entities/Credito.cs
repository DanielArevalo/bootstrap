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
    public class Credito
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
        public DateTime fecha_prox_pago { get; set; }
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

        #region DatosCompletosDeudor
        [DataMember]
        public string primer_nombre { get; set; }
        [DataMember]
        public string segundo_nombre { get; set; }
        [DataMember]
        public string primer_apellido { get; set; }
        [DataMember]
        public string segundo_apellido { get; set; }
        #endregion DatosCompletosDeudor

        #region AsesorComercial
        [DataMember]
        public Int64 CodigoAsesor { get; set; }
        [DataMember]
        public string NombreAsesor { get; set; }
        #endregion AsesorComercial

        #region Actas
        [DataMember]
        public Int64 paramcargo { get; set; }
        [DataMember]
        public String paramrestruct { get; set; }
        #endregion Actas     

        #region Descuentos del Credito a realizar en el desembolso
        [DataMember]
        public List<DescuentosDesembolso> lstDescuentos { get; set; }
        #endregion Descuentos del Credito

        /// <summary>
        /// CALCULO VALORES PARA REFINANCIACION
        /// </summary>
        [DataMember]
        public long minimo_refinancia { get; set; }
        [DataMember]
        public long maximo_refinancia { get; set; }
        [DataMember]
        public int tipo_refinancia { get; set; }
        [DataMember]
        public string nomtipo_refinancia { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public decimal saldo { get; set; }
        [DataMember]
        public decimal cuotas_pagass { get; set; }
        [DataMember]
        public decimal Rango { get; set; }
        [DataMember]
        public decimal valor_para_refinanciar { get; set; }
        [DataMember]
        public int reciprocidad { get; set; }
        [DataMember]
        public int tipo_plan { get; set; }

        /// <summary>
        /// AGREGADO PARA DATOS DE PREANALISIS DE CREDITOS
        /// </summary>
        [DataMember]
        public Int64 idpreanalisis { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public Decimal saldo_disponible { get; set; }
        [DataMember]
        public Decimal cuota_credito { get; set; }
        [DataMember]
        public Decimal cuota_servicios { get; set; }
        [DataMember]
        public Decimal pago_terceros { get; set; }
        [DataMember]
        public Decimal cuota_otros { get; set; }
        [DataMember]
        public Decimal ingresos_adicionales { get; set; }
        [DataMember]
        public Decimal menos_smlmv { get; set; }
        [DataMember]
        public Decimal total_disponible { get; set; }
        [DataMember]
        public Decimal aportes { get; set; }
        [DataMember]
        public Decimal creditos { get; set; }
        [DataMember]
        public Decimal capitalizacion { get; set; }
        [DataMember]
        public Decimal monto_maximo { get; set; }
        [DataMember]
        public int cod_usuario { get; set; }
        [DataMember]
        public int educativo { get; set; }


        /// <summary>
        /// agregado para el reporteplan
        /// </summary>
        [DataMember]
        public decimal otros { get; set; }
        [DataMember]
        public decimal interesmora { get; set; }
        [DataMember]
        public decimal intcoriente { get; set; }
        [DataMember]
        public decimal capital { get; set; }
        [DataMember]
        public int check { get; set; }
        [DataMember]
        public Decimal porcentaje_auxilio { get; set; }
        [DataMember]
        public Decimal valor_auxilio { get; set; }
        [DataMember]
        public int maneja_auxilio { get; set; }

        /// <summary>
        /// agregado para  cambio linea credito educativo
        /// </summary>
        [DataMember]
        public List<codeudores> lstCodeudores { get; set; }
        [DataMember]
        public List<CreditoRecoger> lstCreditoRecoger { get; set; }
        [DataMember]
        public List<Documento> lstDocumentos { get; set; }
        [DataMember]
        public int tipo_documento { get; set; }
        [DataMember]
        public Int64 referencia { get; set; }
        [DataMember]
        public int iddocumento { get; set; }
        [DataMember]
        public String rutapdf { get; set; }
        [DataMember]
        public Imagenes image { get; set; }
        [DataMember]
        public List<DescuentosCredito> lstDescuentosCredito { get; set; }
        [DataMember]
        public List<AmortizaCre> lstAmortizaCre { get; set; }
        [DataMember]
        public long? cuotas_pendientes { get; set; }
        [DataMember]
        public int tipo_liquidacion { get; set; }
        [DataMember]
        public int? tipo_gracia { get; set; }
        [DataMember]
        public long? cod_atr_gracia { get; set; }
        [DataMember]
        public decimal? periodo_gracia { get; set; }
        [DataMember]
        public string tipo_credito { get; set; }
        [DataMember]
        public long? num_radic_origen { get; set; }
        [DataMember]
        public int? cod_empresa { get; set; }
        [DataMember]
        public int? cod_pagaduria { get; set; }
        [DataMember]
        public decimal gradiente { get; set; }
        [DataMember]
        public DateTime? fecha_creacion { get; set; }
        [DataMember]
        public DateTime? fecha_ultima_mod { get; set; }
        [DataMember]
        public string cod_usuario_ultima_mod { get; set; }
        [DataMember]
        public int? pago_especial { get; set; }
        [DataMember]
        public Documento Documento_Garantia { get; set; }
        [DataMember]
        public codeudores Codeudor1 { get; set; }
        [DataMember]
        public codeudores Codeudor2 { get; set; }
        [DataMember]
        public codeudores Codeudor3 { get; set; }
        [DataMember]
        public AtributosCredito AtributosCredito1 { get; set; }
        [DataMember]
        public AtributosCredito AtributosCredito2 { get; set; }
        [DataMember]
        public AtributosCredito AtributosCredito3 { get; set; }
        [DataMember]
        public AtributosCredito AtributosCredito4 { get; set; }
        [DataMember]
        public CuotasExtras CuotasExtras { get; set; }
        [DataMember]
        public byte[] imagen { get; set; }
        [DataMember]
        public long? numero_cuenta_ahorro_vista { get; set; }
        [DataMember]
        public string numero_cuenta { get; set; }
        [DataMember]
        public string cod_banco { get; set; }
        [DataMember]
        public string tipocuenta { get; set; }
        [DataMember]
        public string forma_desembolso { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public int? cod_Departamento { get; set; }
        [DataMember]
        public int? Cod_MunoCiu { get; set; }
        [DataMember]
        public string Pais { get; set; }
        [DataMember]
        public decimal MediaSaldoDiario { get; set; }
        [DataMember]
        public decimal ValorSaldoMaxCt { get; set; }
        [DataMember]
        public decimal ValorSaldoMinCt { get; set; }
        [DataMember]
        public decimal ValorTotalMovNatCred { get; set; }
        [DataMember]
        public int? NumMoviNatCredito { get; set; }
        [DataMember]
        public decimal ValorPromMoviNatCredito { get; set; }
        [DataMember]
        public decimal MedMoviNTCrditoDiarios { get; set; }
        [DataMember]
        public decimal ValorTotalMovNatDebito { get; set; }
        [DataMember]
        public int? NumMoviNatDebito { get; set; }
        [DataMember]
        public decimal ValorPromMovNatDebito { get; set; }
        [DataMember]
        public int? Cod_Destinacion { get; set; }
        [DataMember]
        public decimal monto_giro { get; set; }
        [DataMember]
        public string mensaje { get; set; }
        [DataMember]
        public Int32 Tipo_Linea { get; set; }
        [DataMember]
        public Int32 ReqAfiancol { get; set; }
        [DataMember]
        public string NombreDestinacion { get; set; }
        [DataMember]
        public int Reestructurado { get; set; }
    }
}