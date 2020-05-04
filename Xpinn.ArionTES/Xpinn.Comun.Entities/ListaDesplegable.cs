using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Comun.Entities
{
    [DataContract]
    [Serializable]
    public class ListaDesplegable
    {
        [DataMember]
        public string idconsecutivo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
    }   

}
