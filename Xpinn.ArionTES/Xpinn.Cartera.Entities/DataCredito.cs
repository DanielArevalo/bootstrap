using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Cartera.Entities
{
    [DataContract]
    [Serializable]
    public class DataCredito
    {
        [DataMember]
        public string descripcion { get; set; }        
    }
}
