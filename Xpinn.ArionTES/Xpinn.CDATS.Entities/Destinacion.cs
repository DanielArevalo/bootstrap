using System;
using System.Runtime.Serialization;

namespace Xpinn.CDATS.Entities
{
    [DataContract]
    [Serializable]
    public class Destinacion
    {
        [DataMember]
        public int? cod_destino { get; set; }
        [DataMember]
        public string descripcion { get; set; }
    }
}
