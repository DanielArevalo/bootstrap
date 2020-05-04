using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Riesgo.Entities
{

    [DataContract]
    [Serializable()]

    public class TipoAsociado
    {
        [DataMember]
        public Int64  Cod_tipoasociado { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string valoracion { get; set; }
    }
}

