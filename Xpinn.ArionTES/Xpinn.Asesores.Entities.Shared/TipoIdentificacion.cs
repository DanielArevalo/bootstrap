using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities.Common
{
    [DataContract]
    [Serializable]
    public class TipoIdentificacion
    {
        [DataMember]
        public Int64 IdTipoIdentificacion { get; set; } // Corresponde a la columna CODTIPOIDENTIFICACION
        [DataMember]
        public string NombreTipoIdentificacion { get; set; } // Corresponde a la columna DESCRIPCION
    }
}
