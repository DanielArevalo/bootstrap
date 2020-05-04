using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.NIIF.Entities
{
    [DataContract]
    [Serializable]
    public class TransicionRangosNIF
    {
        [DataMember]
        public int? codrango { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int? dias_minimo { get; set; }
        [DataMember]
        public int? dias_maximo { get; set; }

        [DataMember]
        public int num_modi { get; set; }
        [DataMember]
        public int num_grab { get; set; }


        [DataMember]
        public List<TransicionRangosNIF> lstRangos { get; set; }
    }
}