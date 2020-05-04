using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class LiquidacionNominaDetalle
    {
        [DataMember]
        public long consecutivo { get; set; }
        [DataMember]
        public long codigoliquidacionnomina { get; set; }
        [DataMember]
        public long codigoempleado { get; set; }
        [DataMember]
        public decimal valortotalapagar { get; set; }
        [DataMember]
        public string identificacion_empleado { get; set; }
        [DataMember]
        public string nombre_empleado { get; set; }
        [DataMember]
        public decimal salario { get; set; }

        [DataMember]
        public decimal subsidio_transporte { get; set; }
        [DataMember]
        public long codigo_cargo { get; set; }
        [DataMember]
        public string desc_cargo { get; set; }
        [DataMember]
        public long dias { get; set; }


        [DataMember]
        public decimal valor_anticipo { get; set; }

        [DataMember]
        public long porcentaje_anticipo { get; set; }


        [DataMember]
        public decimal valor_anticipo_sub { get; set; }

        [DataMember]
        public long porcentaje_anticipo_sub { get; set; }

        [DataMember]
        public long codigo_anticipo { get; set; }

        [DataMember]
        public long centrocosto { get; set; }

        [DataMember]
        public long codigocentrocosto { get; set; }

    }
}