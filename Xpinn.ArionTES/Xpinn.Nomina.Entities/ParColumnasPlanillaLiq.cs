using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class ParColumnasPlanillaLiq
    {
        [DataMember]
        public long codigocolumna { get; set; }
        [DataMember]
        public string nombrecolumna { get; set; }
        [DataMember]
        public long esvisible { get; set; }
    }
}
