using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.CDATS.Entities
{
    [DataContract]
    [Serializable]
    public class RepVencimientoCDAT
    {
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
        public Int64? cod_destinacion { get; set; }
        [DataMember]
        public DateTime fecha_apertura { get; set; }
        [DataMember]
        public string modalidad { get; set; }
        [DataMember]
        public int? codforma_captacion { get; set; }
        [DataMember]
        public int plazo { get; set; }
        [DataMember]
        public String identificacion { get; set; }
        [DataMember]
        public String nombres { get; set; }
        [DataMember]
        public String apellidos { get; set; }
        [DataMember]
        public String direccion { get; set; }
        [DataMember]
        public String telefono { get; set; }
        [DataMember]
        public String nom_modalidad { get; set; }
        [DataMember]
        public String nom_periodo { get; set; }
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
        public int? cod_asesor_com { get; set; }
        [DataMember]
        public string tipo_interes { get; set; }
        [DataMember]
        public decimal? tasa_interes { get; set; }
        [DataMember]
        public int? cod_tipo_tasa { get; set; }
        [DataMember]
        public int? tipo_historico { get; set; }
        [DataMember]
        public decimal? desviacion { get; set; }
        [DataMember]
        public int? cod_periodicidad_int { get; set; }
        [DataMember]
        public int? modalidad_int { get; set; }
        [DataMember]
        public int? capitalizar_int { get; set; }
        [DataMember]
        public int? cobra_retencion { get; set; }
        [DataMember]
        public decimal? tasa_nominal { get; set; }
        [DataMember]
        public decimal? tasa_efectiva { get; set; }
        [DataMember]
        public decimal? interes_causado { get; set; }
        [DataMember]
        public decimal? interes_mes { get; set; }
        [DataMember]
        public decimal? interes_retencion { get; set; }
        [DataMember]
        public decimal? intereses_cap { get; set; }
        [DataMember]
        public decimal? retencion_cap { get; set; }
        [DataMember]
        public DateTime? fecha_intereses { get; set; }
        [DataMember]
        public int? estado { get; set; }
        [DataMember]
        public int? desmaterializado { get; set; }
        [DataMember]
        public string observacion { get; set; }
    }
}


