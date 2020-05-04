using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Cartera.Entities
{
    [DataContract]
    [Serializable]
    public class ParametroCobroPrejuridico
    {
        [DataMember]
        public int idparametro { get; set; }
        [DataMember]
        public int? tipo_cobro { get; set; }
        [DataMember]
        public string minimo { get; set; }
        [DataMember]
        public string maximo { get; set; }
        [DataMember]
        public int forma_cobro { get; set; }
        [DataMember]
        public int? porcentaje { get; set; }
        [DataMember]
        public decimal? valor { get; set; }
    }
}
