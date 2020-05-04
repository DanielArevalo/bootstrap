using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.ConciliacionBancaria.Entities
{
    [DataContract]
    [Serializable]
    public class Tipo_Campo_Ext
    {
        [DataMember]
        public int cod_tipo_campo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
    }
}