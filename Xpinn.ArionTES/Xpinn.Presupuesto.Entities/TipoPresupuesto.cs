using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Presupuesto.Entities
{
    /// <summary>
    /// Entidad Componente
    /// </summary>
    [DataContract]
    [Serializable]
    public class TipoPresupuesto
    {
        [DataMember]
        public Int64 tipo_presupuesto { get; set; }
        [DataMember]
        public string descripcion { get; set; }
    }
}
