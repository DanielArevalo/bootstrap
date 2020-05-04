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
    public class TransaccionCredito
    {
        public long? num_tran { get; set; }
        public long? numero_radicacion { get; set; }
        public long? cod_cliente { get; set; }
        public string cod_linea_credito { get; set; }
        public int? tipo_tran { get; set; }
        public int? cod_atr { get; set; }
        public decimal? valor { get; set; }
        public long? estado { get; set; }
        public long? num_tran_anula { get; set; }
        public long? tipo_mov { get; set; }
    }
}
