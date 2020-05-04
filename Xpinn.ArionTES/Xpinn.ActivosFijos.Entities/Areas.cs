using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.ActivosFijos.Entities
{
    [DataContract]
    [Serializable]
    public class Areas
    {
        [DataMember]
        public Int64 IdArea { get; set; }
        [DataMember]
        public String DescripcionArea { get; set; }
        [DataMember]
        public Int64? IdCentroCosto { get; set; }


    }
}

