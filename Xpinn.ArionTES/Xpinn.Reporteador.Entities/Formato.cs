using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data;

namespace Xpinn.Reporteador.Entities
{
    /// <summary>
    /// Entidad Formato
    /// </summary>
    [DataContract]
    [Serializable]
    public class Formato
    {
        [DataMember]
        public Int64 idformato { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string formato { get; set; }
        [DataMember]
        public int? tipo_dato { get; set; }
    }
}
