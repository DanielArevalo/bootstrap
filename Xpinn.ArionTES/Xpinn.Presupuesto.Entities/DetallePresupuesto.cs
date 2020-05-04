using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Presupuesto.Entities
{
    /// <summary>
    /// Entidad presupuesto
    /// </summary>
    [DataContract]
    [Serializable]
    public class DetallePresupuesto
    {
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string nombre { get; set; }
    }
}
