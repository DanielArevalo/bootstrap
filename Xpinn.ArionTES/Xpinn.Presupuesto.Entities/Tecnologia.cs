using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Presupuesto.Entities
{
    /// <summary>
    /// Entidad Tecnologia
    /// </summary>
    [DataContract]
    [Serializable]
    public class Tecnologia
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
        public DateTime fecha_compra { get; set; }
        [DataMember]
        public Int64 tipo_concepto { get; set; }
    }
}
