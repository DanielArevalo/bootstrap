using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Programado.Entities
{
    /// <summary>
    /// Entidad CuotasExtras
    /// </summary>
    [DataContract]
    [Serializable]
    public class ProgramadoCuotasExtras
    {
        [DataMember] 
        public Int64 cod_cuota { get; set; }
        [DataMember] 
        public String numero_programado { get; set; }
        [DataMember] 
        public DateTime? fecha_pago { get; set; }
        [DataMember] 
        public decimal? valor { get; set; }

        [DataMember]
        public decimal? valor_capital { get; set; }
       
        [DataMember]
        public decimal? saldo_capital { get; set; }
    



    }
}