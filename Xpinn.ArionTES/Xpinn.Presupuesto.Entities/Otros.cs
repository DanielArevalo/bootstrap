using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Presupuesto.Entities
{
    /// <summary>
    /// Entidad Otros
    /// </summary>
    [DataContract]
    [Serializable]
    public class Otros
    {
        [DataMember]
        public Int64 codigo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public decimal arriendo_ant { get; set; }
        [DataMember]
        public decimal servicios_ant { get; set; }
        [DataMember]
        public decimal vigilancia_ant { get; set; }
        [DataMember]
        public decimal incremento_arriendo { get; set; }
        [DataMember]
        public decimal incremento_servicios { get; set; }
        [DataMember]
        public decimal incremento_vigilancia { get; set; }
        [DataMember]
        public decimal arriendo { get; set; }
        [DataMember]
        public decimal servicios { get; set; }
        [DataMember]
        public decimal vigilancia { get; set; }
    }
}
