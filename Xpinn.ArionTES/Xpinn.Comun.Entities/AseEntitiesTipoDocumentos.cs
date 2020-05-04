using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Comun.Entities
{

    [DataContract]
    [Serializable]
    public class AseEntitiesTipoDocumentos
    {
        [DataMember]
        public Int64 IdPrograma { get; set; }
        [DataMember]
        public string CodigoDocumento { get; set; }
        [DataMember]
        public string NombreDocumento { get; set; }
    }
}
