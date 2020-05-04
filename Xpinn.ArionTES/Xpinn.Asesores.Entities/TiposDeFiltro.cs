using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities
{
    /// <summary>
    /// Entidad TiposDeFiltro, para búsqueda de clientes
    /// </summary>
    [DataContract]
    [Serializable]
    public class TiposDeFiltro
    {
        [DataMember]
        public string SQLWhere { get; set; }
        [DataMember]
        public string Nombre { get; set; }

    }
}