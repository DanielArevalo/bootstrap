using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class LiquidacionPrimaDetalle
    {
        [DataMember]
        public long consecutivo { get; set; }
        [DataMember]
        public long codigoliquidacionprima { get; set; }
        [DataMember]
        public long codigoempleado { get; set; }
        [DataMember]
        public decimal valortotalpagar { get; set; }
        [DataMember]
        public long diasliquidar { get; set; }
        [DataMember]
        public DateTime fechainicio { get; set; }
        [DataMember]
        public DateTime fechaterminacion { get; set; }

        [DataMember]
        public DateTime fechaingreso { get; set; }
        [DataMember]


        public string identificacion_empleado { get; set; }
        [DataMember]
        public string nombre_empleado { get; set; }
        [DataMember]
        public decimal salario { get; set; }
        [DataMember]
        public long codigo_cargo { get; set; }
        [DataMember]
        public string desc_cargo { get; set; }

        [DataMember]
        public Int64 cod_ope { get; set; }
    }
}