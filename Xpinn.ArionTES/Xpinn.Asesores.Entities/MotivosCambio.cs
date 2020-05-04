using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities
{
    /// <summary>
    /// Entidad MotivosCambio
    /// </summary>
    [DataContract]
    [Serializable]
    public class MotivosCambio
    {
        [DataMember]
        public Int64 cod_motivo_cambio { get; set; }
        [DataMember]
        public string descripcion { get; set; }

    }
}