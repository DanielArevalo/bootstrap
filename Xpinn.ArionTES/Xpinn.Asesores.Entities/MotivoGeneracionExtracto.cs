using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities
{
    /// <summary>
    /// Entidad MotivoGeneracionExtracto, contiene los motivos de generación de extracto para despues ser usado en un registro de generación de extractos.
    /// </summary>
    [DataContract]
    [Serializable]
    public class MotivoGeneracionExtracto
    {
        [DataMember]
        public Int64 Codigo { get; set; }
        [DataMember]
        public string Nombre { get; set; }
    }
}
