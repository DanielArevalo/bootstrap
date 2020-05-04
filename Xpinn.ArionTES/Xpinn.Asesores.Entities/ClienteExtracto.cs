using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities
{
    [DataContract]
    [Serializable]
    public class ClienteExtracto
    {

        [DataMember]
        public Int64 NumeroRadicacion { get; set; }
        [DataMember]
        public Int64 CodPersona { get; set; }
        [DataMember]
        public Int64 Identificacion { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string CodigoLineaDeCredito { get; set; }
        [DataMember]
        public string Linea { get; set; }
        [DataMember]
        public double SaldoCapital { get; set; }
        [DataMember]
        public string Oficina { get; set; }
  
    }
}
