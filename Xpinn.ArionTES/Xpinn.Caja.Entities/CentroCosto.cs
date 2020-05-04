using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Caja.Entities
{
    [DataContract]
    [Serializable]
    public class CentroCosto
    {
        [DataMember]
        public Int64 IdCentroCosto { get; set; }
        [DataMember]
        public Int64 centro_costo { get; set; }
        [DataMember]
        public string nom_centro { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public Int64 principal { get; set; }
    }
}
