using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.NIIF.Entities
{
    [DataContract]
    [Serializable]
    public class CarteraNIF
    {

        [DataMember]
        public int consecutivo { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public decimal monto { get; set; }
        [DataMember]
        public decimal saldo { get; set; }
        [DataMember]
        public int plazo { get; set; }
        [DataMember]
        public int cod_periodicidad { get; set; }
        [DataMember]
        public decimal cuota { get; set; }
        [DataMember]
        public DateTime fecha_proximo_pago { get; set; }
        [DataMember]
        public int dias_mora { get; set; }
        [DataMember]
        public decimal valor_total { get; set; }
        [DataMember]
        public decimal valor_exp { get; set; }
        [DataMember]
        public decimal probabilidad_incump { get; set; }
        [DataMember]
        public decimal perdida_esperada { get; set; }
        [DataMember]
        public string garantia { get; set; }
        [DataMember]
        public decimal porcentaje_pdi { get; set; }
        [DataMember]
        public decimal total_ajuste { get; set; }


    }
}