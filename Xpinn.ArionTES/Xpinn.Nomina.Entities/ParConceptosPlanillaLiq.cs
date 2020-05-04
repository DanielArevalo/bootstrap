using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class ParConceptosPlanillaLiq
    {
        [DataMember]
        public long codigocolumna { get; set; }
        [DataMember]
        public long codigoconcepto { get; set; }
    }
}
