using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.CDATS.Entities
{
    [DataContract]
    [Serializable]
    public class Cdat
    {
        [DataMember]
        public Int64 codigo_gmf { get; set; }
        [DataMember]
        public Int64 monto { get; set; }
        [DataMember]
        public Int64 dias_liquidacion { get; set; }
        [DataMember]
        public Int64 codigo_cdat { get; set; }
        [DataMember]
        public string numero_cdat { get; set; }
        [DataMember]
        public string numero_fisico { get; set; }
        [DataMember]
        public int cod_oficina { get; set; }
        [DataMember]
        public string cod_lineacdat { get; set; }
        [DataMember]
        public Int64 cod_destinacion { get; set; }
        [DataMember]
        public DateTime fecha_apertura { get; set; }
        [DataMember]
        public string modalidad { get; set; }
        [DataMember]
        public int codforma_captacion { get; set; }
        [DataMember]
        public int plazo { get; set; }
        [DataMember]
        public int tipo_calendario { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public int cod_moneda { get; set; }
        [DataMember]
        public DateTime fecha_inicio { get; set; }
        [DataMember]
        public DateTime fecha_vencimiento { get; set; }
        [DataMember]
        public int cod_asesor_com { get; set; }
        [DataMember]
        public string tipo_interes { get; set; }
        [DataMember]
        public decimal tasa_interes { get; set; }

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
        public int cod_tipo_tasa { get; set; }
       
        [DataMember]
        public int? cod_periodicidad_int { get; set; }
        [DataMember]
        public int modalidad_int { get; set; }
        [DataMember]
        public int capitalizar_int { get; set; }

        [DataMember]
        public int capitalizar_int_old { get; set; }

        [DataMember]
        public decimal valor_capitalizar { get; set; }
        [DataMember]
        public int cobra_retencion { get; set; }
        [DataMember]
        public decimal tasa_nominal { get; set; }
        [DataMember]
        public decimal tasa_efectiva { get; set; }
        [DataMember]
        public decimal intereses_cap { get; set; }
        [DataMember]
        public decimal retencion_cap { get; set; }
        [DataMember]
        public DateTime fecha_intereses { get; set; }
        [DataMember]
        public int estado { get; set; }
        [DataMember]
        public string nom_estado { get; set; }
        [DataMember]
        public String estado_persona { get; set; }
        [DataMember]
        public int moneda { get; set; }
        [DataMember]
        public int desmaterializado { get; set; }

        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string cod_nomina { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        //Agregado
        [DataMember]
        public List<Detalle_CDAT> lstDetalle { get; set; }

        [DataMember]
        public int origen { get; set; }


        [DataMember]
        public String cdat_renovado { get; set; }

        //lista Usuario
        [DataMember]
        public int codusuario { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string nombres { get; set; }

        [DataMember]
        public string nomoficina { get; set; }
        [DataMember]
        public int numdias { get; set; }
        [DataMember]
        public string nommoneda { get; set; }
        [DataMember]
        public string retencion { get; set; }
        [DataMember]
        public int opcion { get; set; }

        [DataMember]
        public string observacion { get; set; }
        [DataMember]
        public string nomperiodicidad { get; set; }
        [DataMember]
        public string nomlinea { get; set; }


        [DataMember]
        public decimal intereses { get; set; }

        [DataMember]
        public decimal intereses_cau { get; set; }

        [DataMember]
        public decimal valor_acumulado { get; set; }

        [DataMember]
        public decimal valor_total_acumu { get; set; }

        [DataMember]
        public Int64 forma_pago { get; set; }

        [DataMember]
        public string numero_cuenta { get; set; }

        [DataMember]
        public decimal valor_parcial { get; set; }

        [DataMember]
        public int PagoRealizadoPor { get; set; }

        /// <summary>
        /// Reporte 1020 DIAN
        /// </summary>
        [DataMember]
        public string tipo_identificacion { get; set; }
        [DataMember]
        public int? Digito_Verificacion { get; set; }
        [DataMember]
        public string Primer_Apellido { get; set; }
        [DataMember]
        public string Segundo_Apellido { get; set; }
        [DataMember]
        public string Primer_Nombre { get; set; }
        [DataMember]
        public string Segundo_Nombre { get; set; }
        [DataMember]
        public string Razon_Social { get; set; }
        [DataMember]
        public string Direccion { get; set; }
        [DataMember]
        public int? Cod_Dep { get; set; }
        [DataMember]
        public int? Cod_Mun_Ciud { get; set; }
        [DataMember]
        public string Pais { get; set; }
        [DataMember]
        public int? TipoTitulo { get; set; }
        [DataMember]
        public int? TipoMovimiento { get; set; }
        [DataMember]
        public decimal? valorInversion { get; set; }
        [DataMember]
        public long cod_ope { get; set; }
        [DataMember]
        public long? numero_cuenta_ahorro_vista { get; set; }

        [DataMember]
        public int DiasAviso { get; set; }
        [DataMember]
        public string mensajenotifi { get; set; }
        //Agregado Atención Web
        [DataMember]
        public int cod_tipo_producto { get; set; }
        [DataMember]
        public int estado_modificacion1 { get; set; }
        [DataMember]
        public List<Beneficiarios> lstBenef { get; set; }
        [DataMember]
        public byte[] consignacion { get; set; }
        [DataMember]
        public byte[] declaracion { get; set; }
    }
    
    [DataContract]
    [Serializable]
    public class Detalle_CDAT
    {
        [DataMember]
        public Int64 cod_usuario_cdat { get; set; }
        [DataMember]
        public Int64 codigo_cdat { get; set; }
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
        [DataMember]
        public long? numero_cuenta_ahorro_vista { get; set; }
    }


    [DataContract]
    [Serializable]
    public class CDAT_AUDITORIA
    {
        [DataMember]
        public Int64 cod_auditoria_cdat { get; set; }
        [DataMember]
        public Int64 codigo_cdat { get; set; }
        [DataMember]
        public int tipo_registro_aud { get; set; }
        [DataMember]
        public DateTime fecha_aud { get; set; }
        [DataMember]
        public Int64 cod_usuario_aud { get; set; }
        [DataMember]
        public string ip_aud { get; set; }
       

    }


}
