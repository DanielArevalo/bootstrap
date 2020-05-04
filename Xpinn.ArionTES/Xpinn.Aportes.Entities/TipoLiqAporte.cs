using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class TipoLiqAporte
    {
        [DataMember]
        public int tipo_liquidacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public int cod_periodicidad { get; set; }
        [DataMember]
        public int tipo_saldo_base { get; set; }
        [DataMember]
        public int lineaaporte_base { get; set; }
        [DataMember]
        public int lineaaporte_afecta { get; set; }
    }
}