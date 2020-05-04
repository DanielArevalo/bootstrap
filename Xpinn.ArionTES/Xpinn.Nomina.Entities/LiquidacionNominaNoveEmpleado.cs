using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class LiquidacionNominaNoveEmpleado
    {
        [DataMember]
        public long consecutivo { get; set; }
        [DataMember]
        public long codigoliquidacionnominadetalle { get; set; }
        [DataMember]
        public long codigoempleado { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int codigoconcepto { get; set; }
        [DataMember]
        public decimal valorconcepto { get; set; }
        [DataMember]
        public long codigocentrocosto { get; set; }
        [DataMember]
        public long codigonomina { get; set; }
        [DataMember]
        public int tipo { get; set; }
        [DataMember]
        public string identificacion { get; set; }

        [DataMember]
        public string nombre { get; set; }

    }
}