using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities
{
    /// <summary>
    /// Entidad Actividad
    /// </summary>
    [DataContract]
    [Serializable]
    public class Actividad
    {
        [DataMember]
        public Int64 codactividad { get; set; }
        [DataMember]
        public string descripcion { get; set; }

    }
}