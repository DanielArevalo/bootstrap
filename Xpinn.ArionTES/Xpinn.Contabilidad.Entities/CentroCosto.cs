using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class CentroCosto
    {
        [DataMember]
        public Int64 centro_costo { get; set; }
        [DataMember]
        public string nom_centro { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public Int32 principal { get; set; }
    }
}
