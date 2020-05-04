using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities
{
    /// <summary>
    /// Entidad TipoDiligencia
    /// </summary>
    [DataContract]
    [Serializable]
    public class TipoDiligencia
    {
        [DataMember]
        public Int64 tipo_diligencia { get; set; }
        [DataMember]
        public string descripcion { get; set; }

    }
}