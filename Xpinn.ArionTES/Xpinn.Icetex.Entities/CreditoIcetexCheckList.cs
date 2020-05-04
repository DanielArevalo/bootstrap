using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Icetex.Entities
{
    [DataContract]
    [Serializable]
    public class CreditoIcetexCheckList
    {
        [DataMember]
        public int idcheck { get; set; }
        [DataMember]
        public int idaprobacion { get; set; }
        [DataMember]
        public Int64 numero_credito { get; set; }
        [DataMember]
        public int? tipo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string valor { get; set; }
        [DataMember]
        public int? resultado { get; set; }
        [DataMember]
        public string observacion { get; set; }
        [DataMember]
        public string obs_resultado { get; set; }
    }
}
