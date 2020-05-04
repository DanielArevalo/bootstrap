using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.ActivosFijos.Entities
{
    [DataContract]
    [Serializable]
    public class EstadoActivo
    {
        [DataMember]
        public int estado { get; set; }
        [DataMember]
        public string nombre { get; set; }
    }
}