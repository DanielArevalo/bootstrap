using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Package.Entities
{
    [DataContract]
    [Serializable]
    public class lineascredito
    {
        [DataMember]
        public string cod_linea_credito { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public int? tipo_linea { get; set; }
        [DataMember]
        public int tipo_liquidacion { get; set; }
        [DataMember]
        public int? tipo_cupo { get; set; }
        [DataMember]
        public int? recoge_saldos { get; set; }
        [DataMember]
        public int? cobra_mora { get; set; }
        [DataMember]
        public int? tipo_refinancia { get; set; }
        [DataMember]
        public decimal? minimo_refinancia { get; set; }
        [DataMember]
        public decimal? maximo_refinancia { get; set; }
        [DataMember]
        public string maneja_pergracia { get; set; }
        [DataMember]
        public int? periodo_gracia { get; set; }
        [DataMember]
        public string tipo_periodic_gracia { get; set; }
        [DataMember]
        public string modifica_datos { get; set; }
        [DataMember]
        public string modifica_fecha_pago { get; set; }
        [DataMember]
        public string garantia_requerida { get; set; }
        [DataMember]
        public int? tipo_capitalizacion { get; set; }
        [DataMember]
        public int? cuotas_extras { get; set; }
        [DataMember]
        public int? cod_clasifica { get; set; }
        [DataMember]
        public int? numero_codeudores { get; set; }
        [DataMember]
        public int? cod_moneda { get; set; }
        [DataMember]
        public decimal? porc_corto { get; set; }
        [DataMember]
        public int tipo_amortiza { get; set; }
        [DataMember]
        public int? estado { get; set; }
        [DataMember]
        public int? aprobar_avances { get; set; }
        [DataMember]
        public int? desembolsar_a_ahorros { get; set; }
        [DataMember]
        public int? plazo_a_diferir { get; set; }
        [DataMember]
        public int? aplica_tercero { get; set; }
        [DataMember]
        public int? aplica_asociado { get; set; }
        [DataMember]
        public int? aplica_empleado { get; set; }
        [DataMember]
        public string maneja_excepcion { get; set; }
        [DataMember]
        public int? cuotas_intajuste { get; set; }
        [DataMember]
        public int? credito_gerencial { get; set; }
        [DataMember]
        public int? orden_servicio { get; set; }
        [DataMember]
        public int? educativo { get; set; }
        [DataMember]
        public int? credito_x_linea { get; set; }
        [DataMember]
        public int? maneja_auxilio { get; set; }
        [DataMember]
        public int? prioridad { get; set; }
    }
}