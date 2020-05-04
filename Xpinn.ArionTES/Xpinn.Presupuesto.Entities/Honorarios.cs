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
    public class Honorarios
    {
        [DataMember]
        public Int64 codigo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public decimal valor_ant { get; set; }
        [DataMember]
        public decimal incremento { get; set; }
        [DataMember]
        public decimal valor { get; set; }
    }
}