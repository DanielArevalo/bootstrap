using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad CuotasExtras
    /// </summary>
    [DataContract]
    [Serializable]
    public class CuotasExtras
    {
        [DataMember] 
        public Int64 cod_cuota { get; set; }
        [DataMember] 
        public Int64 numero_radicacion { get; set; }
        [DataMember] 
        public DateTime? fecha_pago { get; set; }
        [DataMember] 
        public decimal? valor { get; set; }
        [DataMember]
        public decimal? valor_capital { get; set; }
        [DataMember] 
        public Int64 valor_interes { get; set; }
        [DataMember] 
        public Int64 saldo_capital { get; set; }
        [DataMember] 
        public Int64 saldo_interes { get; set; }
        [DataMember] 
        public string forma_pago { get; set; }
        [DataMember]
        public string des_forma_pago { get; set; }
        [DataMember]
        public Int64 tipo_cuota { get; set; }
        [DataMember]
        public string des_tipo_cuota { get; set; }
    }
}