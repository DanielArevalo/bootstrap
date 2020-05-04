using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class LiquidacionContrato
    {
        [DataMember]
        public long consecutivo { get; set; }
        [DataMember]
        public long codigoempleado { get; set; }
        [DataMember]
        public long codigonomina { get; set; }
        [DataMember]
        public DateTime fecharetiro { get; set; }
        [DataMember]
        public long codigotiporetirocontrato { get; set; }
        [DataMember]
        public int codigousuariocreacion { get; set; }
        [DataMember]
        public DateTime fechacreacion { get; set; }
        [DataMember]
        public decimal valortotalpagar { get; set; }
        [DataMember]
        public string identificacion_empleado { get; set; }
        [DataMember]
        public string nombre_empleado { get; set; }
        [DataMember]
        public string desc_nomina { get; set; }
        [DataMember]
        public string desc_usuario { get; set; }
        [DataMember]
        public long codigoingresopersonal { get; set; }
        [DataMember]
        public DateTime fechaingreso { get; set; }
        [DataMember]
        public string tipo_identificacion { get; set; }
        [DataMember]
        public long codigocentrocosto { get; set; }
        [DataMember]
        public decimal salario { get; set; }
        [DataMember]
        public string desc_cargo { get; set; }
        [DataMember]
        public long codigoTipoContrato { get; set; }
        [DataMember]
        public decimal primaCalculo { get; set; }
        [DataMember]
        public long primaDias { get; set; }
        [DataMember]
        public decimal cesantiasCalculo { get; set; }
        [DataMember]
        public long cesantiasDias { get; set; }
        [DataMember]
        public decimal vacacionesCalculo { get; set; }
        [DataMember]
        public long vacacionesDias { get; set; }
        [DataMember]
        public string desc_tipo_contrato { get; set; }
        [DataMember]
        public string desc_motivo_retiro { get; set; }

        [DataMember]
        public Int64 cod_ope { get; set; }

        [DataMember]
        public Int64 cod_concepto { get; set; }

        [DataMember]
        public decimal valor { get; set; }

        [DataMember]
        public long codorigen { get; set; }

        [DataMember]
        public long dias { get; set; }


        [DataMember]
        public long num_comp { get; set; }


        [DataMember]
        public long tipo_comp { get; set; }
    }
}