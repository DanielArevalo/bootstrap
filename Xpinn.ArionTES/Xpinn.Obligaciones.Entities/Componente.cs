using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Obligaciones.Entities
{
    /// <summary>
    /// Entidad Componente
    /// </summary>
    [DataContract]
    [Serializable]
    public class Componente
    {
        [DataMember]
        public Int64 CODCOMPONENTE { get; set; }
        [DataMember]
        public string NOMBRE { get; set; }
        [DataMember]
        public Int64 CAUSA { get; set; }
    }
}
