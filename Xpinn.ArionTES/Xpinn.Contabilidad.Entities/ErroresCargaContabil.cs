using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class ErroresCargaContabil
    {
        [DataMember]
        public string numero_registro { get; set; }
        [DataMember]
        public string datos { get; set; }
        [DataMember]
        public string error { get; set; }
    }

}
