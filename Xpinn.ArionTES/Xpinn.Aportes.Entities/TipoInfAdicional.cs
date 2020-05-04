using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{

    [DataContract]
    [Serializable]
    public class TipoInfAdicional
    {
        [DataMember]
        public Int64 COD_INFADICIONAL { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Valor { get; set; }
    }
}
