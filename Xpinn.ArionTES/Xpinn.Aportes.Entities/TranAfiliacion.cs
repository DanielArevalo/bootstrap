using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class TranAfiliacion
    {
        [DataMember]
        public Int64 numero_transaccion { get; set; }
        [DataMember]
        public Int64? cod_ope { get; set; }
        [DataMember]
        public DateTime? fecha_oper { get; set; }
        [DataMember]
        public int tipo_ope { get; set; }
        [DataMember]
        public Int64? num_comp { get; set; }
        [DataMember]
        public int tipo_comp { get; set; }
        [DataMember]
        public string nomtipo_comp { get; set; }
        [DataMember]
        public string nomtipo_ope { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public Int64? idafiliacion { get; set; }
        [DataMember]
        public int? cod_atr { get; set; }
        [DataMember]
        public Int64? cod_det_lis { get; set; }
        [DataMember]
        public int tipo_tran { get; set; }
        [DataMember]
        public string nomtipo_tran { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public int estado { get; set; }
    }
}