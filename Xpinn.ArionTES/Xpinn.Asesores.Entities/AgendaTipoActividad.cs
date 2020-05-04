using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities
{
    /// <summary>
    /// Entidad AgendaTipoActividad
    /// </summary>
    [DataContract]
    [Serializable]
    public class AgendaTipoActividad
    {
        [DataMember]
        public Int64 idtipo { get; set; }
        [DataMember]
        public string descripcion { get; set; }

    }
}