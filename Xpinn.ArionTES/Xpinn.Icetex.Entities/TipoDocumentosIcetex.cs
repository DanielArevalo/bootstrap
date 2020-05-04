using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Icetex.Entities
{
    [DataContract]
    [Serializable]
    public class TipoDocumentosIcetex
    {
        [DataMember]
        public int cod_tipo_doc { get; set; }
        [DataMember]
        public string descripcion { get; set; }
    }
}
