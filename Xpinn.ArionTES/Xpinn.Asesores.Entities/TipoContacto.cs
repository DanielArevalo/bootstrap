using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities
{
    /// <summary>
    /// Entidad TipoContacto
    /// </summary>
    [DataContract]
    [Serializable]
    public class TipoContacto
    {
        [DataMember]
        public Int64 tipocontacto { get; set; }
        [DataMember]
        public string descripcion { get; set; }

    }
}