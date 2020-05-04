using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class RangosFondoSolidaridad
    {


        [DataMember]
        public Int64 consecutivo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public Int64 minimo { get; set; }
        [DataMember]
        public Int64 maximo { get; set; }


        [DataMember]
        public decimal? porcentaje { get; set;
        }
    }
}