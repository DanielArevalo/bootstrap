using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities
{
    [DataContract]
    [Serializable]
    public class Acodeudados{
        [DataMember]
        public Int64 CodPersona { get; set; }
        [DataMember]
        public Int64 NumRadicacion { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public string Linea { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string Nombres { get; set; }
        [DataMember]
        public decimal Monto { get; set; }
        [DataMember]
        public decimal Saldo { get; set; }
        [DataMember]
        public decimal Cuota { get; set; }
        [DataMember]
        public DateTime FechaProxPago { get; set; }
        [DataMember]
        public decimal Valor_apagar { get; set; }
        [DataMember]
        public string ciudad { get; set; }
        [DataMember]
        public string Estado_Codeudor { get; set; }
        
    }
}

