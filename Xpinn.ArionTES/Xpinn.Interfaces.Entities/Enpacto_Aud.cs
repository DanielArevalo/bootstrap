using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Interfaces.Entities
{
    [DataContract]
    [Serializable]
    public class Enpacto_Aud
    {
        [DataMember]
        public int? consecutivo { get; set; }
        [DataMember]
        public int? exitoso { get; set; }
        [DataMember]
        public int? tipooperacion { get; set; }
        [DataMember]
        public string jsonentidadpeticion { get; set; }
        [DataMember]
        public string jsonentidadrespuesta { get; set; }
    }
}
