using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data;

namespace Xpinn.Reporteador.Entities
{    
    /// <summary>
    /// Entidad Lista
    /// </summary>
    [DataContract]
    [Serializable]
    public class Lista
    {
        [DataMember]
        public Int64 idlista { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string textfield { get; set; }
        [DataMember]
        public string valuefield { get; set; }
        [DataMember]
        public string sentencia { get; set; }
    }
}
