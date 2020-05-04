using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Caja.Entities
{
    /// <summary>
    /// Entidad SaldoCaja
    /// </summary>
    [DataContract]
    [Serializable]
    public class SaldoCaja
    {
        [DataMember] 
        public Int64 cod_caja { get; set; }
        [DataMember]
        public Int64 caja_principal { get; set; }
        [DataMember] 
        public Int64 cod_cajero { get; set; }
        [DataMember] 
        public DateTime fecha { get; set; }
        [DataMember] 
        public Int64 tipo_moneda { get; set; }
        [DataMember] 
        public decimal valor { get; set; }

    }
}