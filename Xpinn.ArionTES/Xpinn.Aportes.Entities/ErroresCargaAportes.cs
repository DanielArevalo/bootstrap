using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class ErroresCargaAportes
    {
        [DataMember]
        public string numero_registro { get; set; }
        [DataMember]
        public string datos { get; set; }
        [DataMember]
        public string error { get; set; }
    }

}



