using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xpinn.Comun.Entities
{
    [DataContract]
    [Serializable]
    public class ListasFijas
    {
        [DataMember]
        public string codigo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
    }
}
