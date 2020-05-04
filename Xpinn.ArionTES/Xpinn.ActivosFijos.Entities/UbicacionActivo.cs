using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.ActivosFijos.Entities
{
    [DataContract]
    [Serializable]
    public class UbicacionActivo
    {
        [DataMember]
        public int cod_ubica { get; set; }
        [DataMember]
        public string nombre { get; set; }
    }
}