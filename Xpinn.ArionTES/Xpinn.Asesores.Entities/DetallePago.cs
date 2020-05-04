using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Xpinn.Asesores.Entities.Common;

namespace Xpinn.Asesores.Entities
{
    [DataContract]
    [Serializable]
    public class DetallePago
    {
        [DataMember]
        public Int64 NumeroRadicacion { get; set; }
        [DataMember]
        public Int64 NumCuota { get; set; }
        [DataMember]
        public DateTime FechaCuota { get; set; }
        [DataMember]      
        public Decimal Capital { get; set; }
        [DataMember]
        public Decimal IntCte { get; set; }
        [DataMember]
        public Decimal IntMora { get; set; }
        [DataMember]
        public Decimal LeyMiPyme { get; set; }
        [DataMember]
        public Decimal ivaLeyMiPyme { get; set; }

        [DataMember]
        public Decimal Otros { get; set; }
        [DataMember]
        public Decimal Poliza { get; set; }
        [DataMember]
        public Decimal Total { get; set; }
        [DataMember]    
        public Decimal Saldo { get; set; }
        [DataMember]
        public Decimal Cobranzas { get; set; }
        [DataMember]
        public Decimal Garantias_Comunitarias { get; set; }

        [DataMember]
        public Int64 idavance { get; set; }
        [DataMember]
        public int CuotaExtra { get; set; }

    }
}
