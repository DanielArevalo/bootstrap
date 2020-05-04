using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.Ahorros.Entities
{
    [DataContract]
    [Serializable]
    public class AhorroVista
    {
        [DataMember]
        public String numero_cuenta { get; set; }     
        [DataMember]
        public string cod_linea_ahorro { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string apellidos { get; set; }
        [DataMember]
        public string cod_nomina { get; set; }
        [DataMember]
        public int? cod_oficina { get; set; }
        [DataMember]
        public int? cod_destino { get; set; }
        [DataMember]
        public string observaciones { get; set; }
        [DataMember]
        public int? modalidad { get; set; }
        [DataMember]
        public int? estado { get; set; }
        [DataMember]
        public string estadopersona { get; set; }
        [DataMember]
        public DateTime? fecha_apertura { get; set; }
        [DataMember]
        public DateTime? fecha_cierre { get; set; }
        [DataMember]
        public decimal saldo_total { get; set; }
        [DataMember]
        public decimal? saldo_canje { get; set; }
        [DataMember]
        public int? forma_tasa { get; set; }
        [DataMember]
        public int? tipo_historico { get; set; }
        [DataMember]
        public decimal? desviacion { get; set; }
        [DataMember]
        public int? tipo_tasa { get; set; }
        [DataMember]
        public string nom_tipo_tasa { get; set; }
        [DataMember]
        public decimal? tasa { get; set; }
        [DataMember]
        public DateTime? fecha_interes { get; set; }
        [DataMember]
        public decimal? saldo_intereses { get; set; }
        [DataMember]
        public Int64? retencion { get; set; }
        [DataMember]
        public int? cod_forma_pago { get; set; }
        [DataMember]
        public DateTime? fecha_proximo_pago { get; set; }
        [DataMember]
        public DateTime? fecha_ultimo_pago { get; set; }
        [DataMember]
        public decimal? valor_cuota_anterior { get; set; }
        [DataMember]
        public decimal? valor_cuota { get; set; }

        [DataMember]
        public decimal? valor_pagar { get; set; }

        [DataMember]
        public int? cod_periodicidad { get; set; }
        [DataMember]
        public int? dias_liq { get; set; }
        [DataMember]
        public DateTime? fecha_liq { get; set; }
        [DataMember]
        public decimal? saldo_base { get; set; }
        [DataMember]
        public decimal? interes { get; set; }
        [DataMember]
        public decimal? retencion_interes { get; set; }
        [DataMember]
        public decimal? valor_gmf { get; set; }
        [DataMember]
        public decimal? valor_base { get; set; }
        [DataMember]
        public decimal? valor_a_aplicar { get; set; }
        [DataMember]
        public decimal? descuento { get; set; }
        [DataMember]
        public int? dias { get; set; }
        [DataMember]
        public List<CuentaHabientes> LstCuentaHabientes { get; set; }
        [DataMember]
        public string nom_linea { get; set; }
        [DataMember]
        public string nom_oficina { get; set; }
        [DataMember]
        public string nom_estado { get; set; }
        [DataMember]
        public string nom_formapago { get; set; }
        [DataMember]
        public string nom_periodicidad { get; set; }
        [DataMember]
        public string moneda { get; set; }
        //agregado
        [DataMember]
        public Decimal valor_acumulado { get; set; }
        [DataMember]
        public Decimal valor_total_acumu { get; set; }
        [DataMember]
        public string Num_Tarjeta { get; set; }
        //agregado
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public string estados { get; set; }
        [DataMember]
        public string motivos { get; set; }
        [DataMember]
        public Int64 codusuario { get; set; }
        [DataMember]
        public string numero { get; set; }
        [DataMember]
        public decimal? V_Traslado { get; set; }
        [DataMember]
        public string tipo_identificacion { get; set; }
        [DataMember]
        public int tipo_identifi { get; set; }
        //agregado
        [DataMember]
        public int idgirorealizado { get; set; }
        [DataMember]
        public Int64 idgiro { get; set; }
        [DataMember]
        public DateTime fec_realiza { get; set; }
        [DataMember]
        public string usu_realiza { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public string archivo { get; set; }
        [DataMember]
        public DateTime? fec_apro { get; set; }

        [DataMember]
        public DateTime? fec_apro_giro { get; set; }
        //agregado 
        [DataMember]
        public Int64 saldo_inicial { get; set; }
        [DataMember]
        public Int64 saldo_final { get; set; }
        [DataMember]
        public Int64 deposito { get; set; }
        [DataMember]
        public Int64 retiro { get; set; }

        //agregado
        [DataMember]
        public int fecha_operacion { get; set; }
        [DataMember]
        public Int64 numero_operacion { get; set; }
        [DataMember]
        public int numero_transaccion { get; set; }
        [DataMember]
        public Int64 tipo_producto { get; set; }

        [DataMember]
        public String nombre_producto { get; set; }
        [DataMember]
        public  String tipo_transaccion { get; set; }
        [DataMember]
        public int Mes { get; set; }
        [DataMember]
        public Decimal Anio { get; set; }
        [DataMember]
        public int saldo_promedio { get; set; }
        [DataMember]
        public int valor_fondo { get; set; }
        [DataMember]
        public String Fondo { get; set; }
        [DataMember]
        public long idfondoliq { get; set; }
        [DataMember]
        public long pdias { get; set; }

        [DataMember]
        public string estado_modificacion { get; set; }

        [DataMember]
        public string estado_Linea { get; set; }

        //agregado
        [DataMember]
        public String cod_rbTipoArchivo { get; set; }
        [DataMember]
        public Int64 codigo_inicial { get; set; }
        [DataMember]
        public Int64 codigo_final { get; set; }
        [DataMember]
        public String identificacion_inicial { get; set; }
        [DataMember]
        public String identificacion_final { get; set; }
        [DataMember]
        public string empresa { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string CiudadResidencia { get; set; }
        [DataMember]
        public string numero_cuenta_final { get; set; }
        [DataMember]
        public string Tarjeta { get; set; }

        // tarjeta de firmas
        [DataMember]
        public Int64? idimagen { get; set; }
        [DataMember]
        public byte[] foto { get; set; }

        // Exentas Gmf
        [DataMember]
        public string exenta { get; set; }
        [DataMember]
        public DateTime fecha_exenta { get; set; }
        [DataMember]
        public DateTime fechaNoExcenta { get; set; }

        // libreta de ahorros
        [DataMember]
        public Int64 volante { get; set; }

        [DataMember]
        public string estadocierre { get; set; }

        // Datos de los beneficiarios
        [DataMember]
        public List<Beneficiario> lstBeneficiarios { get; set; }
        [DataMember]
        public long idbeneficiario { get; set; }
        [DataMember]
        public string identificacion_ben { get; set; }
        [DataMember]
        public int tipo_identificacion_ben { get; set; }
        [DataMember]
        public int parentesco_ben { get; set; }
        [DataMember]
        public string nombres_ben { get; set; }
        [DataMember]
        public DateTime fecha_nacimiento_ben { get; set; }
        [DataMember]
        public long edad { get; set; }
        [DataMember]
        public long? cod_asesor { get; set; }
        [DataMember]
        public int? cod_empresa_reca { get; set; }
        [DataMember]
        public string tipo_registro { get; set; }
        [DataMember]
        public string direccion_oficina { get; set; }
        [DataMember]
        public string telefono_oficina { get; set; }
        [DataMember]
        public string nombre_oficina { get; set; }
        [DataMember]
        public string direccion_persona { get; set; }
        [DataMember]
        public int? forma_giro { get; set; }
        [DataMember]
        public int aplicada { get; set; }

        //Cambio Novedad
    
        [DataMember]
        public DateTime? fecha_empieza_cambio { get; set; }
        [DataMember]
        public decimal nuevo_valor_cuota { get; set; }
        [DataMember]
        public long id_novedad_cambio { get; set; }
        [DataMember]
        public DateTime? fecha_novedad_cambio { get; set; }
        [DataMember]
        public decimal cuota { get; set; }

        [DataMember]
        public string nuevo_valor_cuota1 { get; set; }
        [DataMember]
        public string cuota1 { get; set; }
        [DataMember]
        public int? estado_modificacion1 { get; set; }

        //Datos agragados para retiro en cuenta de ahorros
        [DataMember]
        public int cod_banco { get; set; }
        [DataMember]
        public int tipo_cuenta { get; set; }
        [DataMember]
        public int id_solicitud { get; set; }
        [DataMember]
        public string nom_giro { get; set; }
        [DataMember]
        public string nom_banco { get; set; }
        [DataMember]
        public string nom_cuenta { get; set; }

        //Datos agregador para solicitud de productos
        [DataMember]
        public int plazo { get; set; }
        [DataMember]
        public int num_cuotas { get; set; }
        [DataMember]
        public string descripcion { get; set; }

        //AGRAGADO PARA CONFIRMACIÓN DE SOLICITUD WEB 
        [DataMember]
        public String aprobador { get; set; }

        //agregado para control de envío masivo
        [DataMember]
        public DateTime fecha_consulta { get; set; }
        [DataMember]
        public DateTime fecha_envio { get; set; }
        [DataMember]
        public long IdPayment { get; set; }
        [DataMember]
        public string PagoRealizadoPor { get; set; }

        [DataMember]
        public DateTime fecha_aprobacion { get; set; }

        [DataMember]
        public DateTime Fecha_estado { get; set; }

        [DataMember]
        public long? numero_cuenta_ahorro_vista { get; set; }

        [DataMember]
        public string usu_apro { get; set; }



        [DataMember]
        public Int64? forma_pago { get; set; }
   
    }


    [DataContract]
    [Serializable]
    public class Imagenes
    {
        [DataMember]
        public Int64 idimagen { get; set; }
        [DataMember]
        public Int64 Numero_cuenta { get; set; }
        [DataMember]
        public Int64 tipo_imagen { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public byte[] imagen { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public byte[] foto { get; set; }
    }

    [DataContract]
    [Serializable]
    public class Detalle_AhorroVista
    {
        [DataMember]
        public Int64 cod_usuario_ahorro { get; set; }
        [DataMember]
        public String numero_cuenta { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public int? principal { get; set; }
        [DataMember]
        public string conjuncion { get; set; }
        //Detalle de la persona
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string apellidos { get; set; }
        [DataMember]
        public Int64 codciudadresidencia { get; set; }
        [DataMember]
        public string ciudad { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
       
    }

    [DataContract]
    [Serializable]
    public class ELiquidacionInteres
    {
        [DataMember]
        public List<ELiquidacionInteres> lstLista { get; set; }
        [DataMember]
        public String NumeroCuenta { get; set; }
        [DataMember]
        public String Linea { get; set; }
        [DataMember]
        public String Identificacion { get; set; }
        [DataMember]
        public Int64 Cod_Usuario { get; set; }
        [DataMember]
        public String Nombre { get; set; }
        [DataMember]
        public String Oficina { get; set; }
        [DataMember]
        public DateTime Fecha_Apertura { get; set; }
        [DataMember]
        public Decimal Saldo { get; set; }
        [DataMember]
        public Decimal Saldo_Base { get; set; }
        [DataMember]
        public Decimal Tasa_interes { get; set; }
        [DataMember]
        public int dias { get; set; }
        [DataMember]
        public Decimal Interes { get; set; }
        [DataMember]
        public Decimal Interescausado { get; set; }
        [DataMember]
        public Decimal interes_capitalizado { get; set; }
        [DataMember]
        public Decimal retencion_causado { get; set; }
        [DataMember]
        public Decimal Retefuente { get; set; }
        [DataMember]
        public Decimal? valor_Neto { get; set; }
        [DataMember]
        public DateTime fecha_liquidacion { get; set; }
        [DataMember]
        public string numero_cuenta { get; set; }
        [DataMember]
        public Decimal valor_gmf { get; set; }
        [DataMember]
        public Decimal valor_pagar { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public string forma_pago { get; set; }
        [DataMember]
        public string cta_ahorros { get; set; }
        [DataMember]
        public int cod_interes { get; set; }
        [DataMember]
        public DateTime fecha_int { get; set; }

        [DataMember]
        public String error { get; set; }        

    }

}
