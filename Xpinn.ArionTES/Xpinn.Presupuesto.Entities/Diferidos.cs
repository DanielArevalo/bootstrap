using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Presupuesto.Entities
{
    /// <summary>
    /// Entidad Diferidos
    /// </summary>
    [DataContract]
    [Serializable]
    public class Diferidos
    {
        [DataMember]
        public Int64 codigo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public string oficina { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public decimal plazo { get; set; }
        [DataMember]
        public DateTime fecha_diferido { get; set; }
    }
}
