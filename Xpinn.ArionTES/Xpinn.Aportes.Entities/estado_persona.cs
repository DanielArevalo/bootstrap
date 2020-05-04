using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class Estado_Persona
    {
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string descripcion { get; set; }
    }
}
