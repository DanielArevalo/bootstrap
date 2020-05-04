using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class TranAporte
    {
        [DataMember]
        public Int64 numero_transaccion { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public int numero_aporte { get; set; }
        [DataMember]
        public int cod_atr { get; set; }
        [DataMember]
        public int cod_det_lis { get; set; }
        [DataMember]
        public int tipo_tran { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public int estado { get; set; }
    }
}