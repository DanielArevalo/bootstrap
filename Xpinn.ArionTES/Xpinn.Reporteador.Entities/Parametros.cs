using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data;

namespace Xpinn.Reporteador.Entities
{
    /// <summary>
    /// Entidad Parametros
    /// </summary>
    [DataContract]
    [Serializable]
    public class Parametros
    {
        [DataMember]
        public Int64 idreporte { get; set; }
        [DataMember]
        public Int64 idparametro { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public Int64 tipo { get; set; }
        [DataMember]
        public string nomtipo { get; set; }
        [DataMember]
        public string valor { get; set; }
        [DataMember]
        public Int64? idlista { get; set; }
    }
}
