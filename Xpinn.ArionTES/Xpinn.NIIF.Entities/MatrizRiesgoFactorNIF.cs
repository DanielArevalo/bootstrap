using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.NIIF.Entities
{
    [DataContract]
    [Serializable]
    public class MatrizRiesgoFactorNIF
    {
        [DataMember]
        public Int64 idfactorpondera { get; set; }
        [DataMember]
        public Int64 idmatriz { get; set; }
        [DataMember]
        public Int64 idparametro { get; set; }
        [DataMember]
        public string descripcionparametro { get; set; }
        [DataMember]
        public string minimo { get; set; }
        [DataMember]
        public string maximo { get; set; }
        [DataMember]
        public decimal factor { get; set; }
        [DataMember]
        public string variable { get; set; }
        [DataMember]
        public decimal calificacion { get; set; }
    }
}