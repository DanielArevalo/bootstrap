using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Xpinn.Programado.Entities
{
    [DataContract]
    [Serializable]
    public class ReporteCuentasPeriodico
    {
        [DataMember]
        public decimal numeroCuenta { get; set; }

        [DataMember]
        public String linea { get; set; }

        [DataMember]
        public String identificacion { get; set; }

        [DataMember]
        public String nombre { get; set; }

        [DataMember]
        public string oficina { get; set; }

        [DataMember]
        public DateTime fechaApertura { get; set; }

        [DataMember]
        public decimal saldoInicial { get; set; }

        [DataMember]
        public decimal depocito { get; set; }

        [DataMember]
        public decimal retiro { get; set; }

        [DataMember]
        public decimal intereses { get; set; }

        [DataMember]
        public decimal retencion { get; set; }

        [DataMember]
        public decimal saldoFinal { get; set; }

        [DataMember]
        public decimal camponoc { get; set; }

    }
}
