using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Comun.Entities
{
    [DataContract]
    [Serializable]
    public class FormaPago
    {
        [DataMember]
        public Int64 codigo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
      
    }
}
