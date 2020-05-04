using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class TipoCotizante
    {
      

        [DataMember]
        public Int64 consecutivo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public decimal porcentaje_salud { get; set; }
        [DataMember]
        public decimal porcentaje_pension { get; set; }

        [DataMember]
        public decimal paga_subsidio { get; set; }
    }
}