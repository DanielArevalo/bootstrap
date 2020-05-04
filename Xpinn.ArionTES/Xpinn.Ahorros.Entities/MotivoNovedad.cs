using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Ahorros.Entities
{
    [DataContract]
    [Serializable]
    public class MotivoNovedad
    {
        [DataMember]
        public int idmotivo_novedad { get; set; }
        [DataMember]
        public string descripcion { get; set; }
    }
}