using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Caja.Entities
{
    /// <summary>
    /// Entidad TipoPago
    /// </summary>
    [DataContract]
    [Serializable]
    public class TipoPago
    {
        [DataMember] 
        public Int64 cod_tipo_pago { get; set; }
        [DataMember] 
        public string descripcion { get; set; }
        [DataMember]
        public string caja { get; set; }

    }
}