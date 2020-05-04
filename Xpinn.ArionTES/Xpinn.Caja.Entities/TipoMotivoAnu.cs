using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Caja.Entities
{
    /// <summary>
    /// Entidad TipoMotivoAnu
    /// </summary>
    [DataContract]
    [Serializable]
    public class TipoMotivoAnu
    {
        [DataMember] 
        public Int64 tipo_motivo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
    }
}