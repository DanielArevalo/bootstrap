using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Xpinn.Package.Entities
{
    [DataContract]
    [Serializable]
    public class DetalleMoraCredito
    {
        public DateTime f_fecha_cuota { get; set; }
        public int n_cod_atr { get; set; }
        public decimal n_valor { get; set; }
        public decimal n_saldo { get; set; }
        public DateTime f_fecha_ini { get; set; }
        public DateTime f_fecha_fin { get; set; }
        public string s_estado { get; set; }
        public int dias_mora { get; set; }
    }
}
