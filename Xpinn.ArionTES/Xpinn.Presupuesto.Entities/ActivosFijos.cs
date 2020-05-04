using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Presupuesto.Entities
{
    /// <summary>
    /// Entidad ActivosFijos
    /// </summary>
    [DataContract]
    [Serializable]
    public class ActivosFijos
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
        public decimal vrcompra { get; set; }
        [DataMember]
        public DateTime fecha_compra { get; set; }
        [DataMember]
        public Int64 tipo_activo { get; set; }
    }
}
