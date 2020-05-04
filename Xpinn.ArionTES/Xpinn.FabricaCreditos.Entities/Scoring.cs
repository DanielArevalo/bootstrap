using System;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    public class Scoring
    {
        [DataMember]
        public long idpreanalisis { get; set; }
        [DataMember]
        public decimal ahorro_voluntario { get; set; }
        [DataMember]
        public long cod_afiliacion { get; set; }
        [DataMember]
        public string cod_nomina { get; set; }
        [DataMember]
        public long cod_persona { get; set; }
        [DataMember]
        public string descripcion_cargo { get; set; }
        [DataMember]
        public string direccionempresa { get; set; }
        [DataMember]
        public string empresa { get; set; }
        [DataMember]
        public DateTime fecha_afiliacion { get; set; }
        [DataMember]
        public DateTime? fecha_score { get; set; }
        [DataMember]
        public DateTime fecha_ingresoempresa { get; set; }
        [DataMember]
        public decimal honorarios { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public decimal otros_ingresos { get; set; }
        [DataMember]
        public string primer_apellido { get; set; }
        [DataMember]
        public string primer_nombre { get; set; }
        [DataMember]
        public string nombre_completo { get; set; }
        [DataMember]
        public decimal monto { get; set; }
        [DataMember]
        public int plazo { get; set; }
        [DataMember]
        public decimal salario { get; set; }
        [DataMember]
        public decimal saldo_aporte { get; set; }
        [DataMember]
        public string segundo_apellido { get; set; }
        [DataMember]
        public string segundo_nombre { get; set; }
        [DataMember]
        public string tipo_identificacion { get; set; }
        [DataMember]
        public decimal valor_cuota_aporte { get; set; }
        [DataMember]
        public decimal factor_intercepto { get; set; }
        [DataMember]
        public decimal factor_pendiente { get; set; }
        [DataMember]
        public long fecha_meses_antiguedad { get; set; }
        [DataMember]
        public string cod_tipo_identificacion { get; set; }
        //Agregado para valor de servicios
        [DataMember]
        public decimal valor_servicios { get; set; }
    }
}
