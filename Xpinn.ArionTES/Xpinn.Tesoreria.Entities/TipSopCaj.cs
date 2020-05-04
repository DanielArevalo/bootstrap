using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class TipSopCaj
    {
        [DataMember]
        public int idtiposop { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string descripcion { get; set; }
    }
}