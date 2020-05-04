using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.NIIF.Entities
{
    [DataContract]
    [Serializable]
    public class DetalleAmortizacionNIIF
    {
        [DataMember]
        public Int64 NumCuota { get; set; }
        [DataMember]
        public DateTime FechaCuota { get; set; }
        [DataMember]
        public decimal Capital { get; set; }
        [DataMember]
        public decimal IntCte { get; set; }
        [DataMember]
        public decimal IntMora { get; set; }
        [DataMember]
        public decimal LeyMiPyme { get; set; }
        [DataMember]
        public decimal IvaLeyMiPyme { get; set; }
        [DataMember]
        public decimal Otros { get; set; }
        [DataMember]
        public decimal Total { get; set; }
    }
}
