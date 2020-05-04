using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Presupuesto.Entities
{
    /// <summary>
    /// Entidad nomina
    /// </summary>
    [DataContract]
    [Serializable]
    public class Nomina
    {
        [DataMember]
        public Int64 codigo { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public DateTime fecha_ingreso { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public string oficina { get; set; }
        [DataMember]
        public decimal salario { get; set; }
        [DataMember]
        public Int64 cargo { get; set; }
        [DataMember]
        public Int64 tipo_salario { get; set; }
        [DataMember]
        public decimal incremento { get; set; }
        [DataMember]
        public decimal salario_nuevo { get; set; }
        [DataMember]
        public decimal aux_trans { get; set; }
        [DataMember]
        public decimal aux_tel { get; set; }
        [DataMember]
        public decimal cumplimiento { get; set; }
        [DataMember]
        public decimal comisiones { get; set; }
        [DataMember]
        public decimal aux_gas { get; set; }
        [DataMember]
        public decimal cesantias { get; set; }
        [DataMember]
        public decimal int_ces { get; set; }
        [DataMember]
        public decimal prima { get; set; }
        [DataMember]
        public decimal vacaciones { get; set; }
        [DataMember]
        public decimal dotacion { get; set; }
        [DataMember]
        public decimal salud { get; set; }
        [DataMember]
        public decimal pension { get; set; }
        [DataMember]
        public decimal arp { get; set; }
        [DataMember]
        public decimal caja_comp { get; set; }
        [DataMember]
        public decimal total { get; set; }
    }
}
