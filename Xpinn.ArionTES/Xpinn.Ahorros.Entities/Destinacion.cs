using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Ahorros.Entities
{
    [DataContract]
    [Serializable]
    public class Destinacion
    {
        [DataMember]
        public int cod_destino { get; set; }
        [DataMember]
        public string descripcion { get; set; }       
    }
}