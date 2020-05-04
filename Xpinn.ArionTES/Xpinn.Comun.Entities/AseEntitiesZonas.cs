using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Comun.Entities
{
    [DataContract]
    [Serializable]
    public class AseEntitiesZonas
    {
        [DataMember]
        public Int64 IdPrograma { get; set; }
        [DataMember]
        public string CodigoCiudad { get; set; }
        [DataMember]
        public string NombreZona { get; set; }

    }
}
