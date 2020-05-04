using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class TipoInversiones
    {
        [DataMember]
        public int cod_tipo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
    }
}
