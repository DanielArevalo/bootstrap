using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Comun.Entities
{

    [DataContract]
    [Serializable]
    public class AseEntitiesActividades
    {
        [DataMember]
        public Int64 IdPrograma { get; set; }
        [DataMember]
        public string CodigoActividad { get; set; }
        [DataMember]
        public string NombreActividad { get; set; }

    }
}
