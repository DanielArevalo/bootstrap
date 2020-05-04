using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class DatosDeDocumento
    {
        [DataMember]
        public Int32 Id { get; set; }
        [DataMember]
        public string Campo { get; set; }
        [DataMember]
        public string Valor { get; set; }
    }
}

