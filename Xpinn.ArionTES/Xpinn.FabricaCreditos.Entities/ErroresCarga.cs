using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class ErroresCarga
    {
        [DataMember]
        public string numero_registro { get; set; }
        [DataMember]
        public string datos { get; set; }
        [DataMember]
        public string error { get; set; }
    }

}



