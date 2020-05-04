using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Riesgo.Entities
{
    [DataContract]
    public class Perfil
    {
        [DataMember]
        public Int64 cod_perfil { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public string descripcion { get; set; }
    }
}
