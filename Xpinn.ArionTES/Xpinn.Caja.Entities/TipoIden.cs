using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Caja.Entities
{
    /// <summary>
    /// Entidad TipoIden
    /// </summary>
    [DataContract]
    [Serializable]
    public class TipoIden
    {
        [DataMember] 
        public Int64 codtipoidentificacion { get; set; }
        [DataMember] 
        public string descripcion { get; set; }

    }
}