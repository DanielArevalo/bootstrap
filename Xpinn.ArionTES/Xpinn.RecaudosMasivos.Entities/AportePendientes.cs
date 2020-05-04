using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class AportePendientes
    {
        [DataMember]
        public Int64 numero_transaccion { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public Int64 numero_aporte { get; set; }
        [DataMember]
        public int? cod_atr { get; set; }
        [DataMember]
        public int? cod_det_list { get; set; }
        [DataMember]
        public int? tipo_tran { get; set; }
        [DataMember]
        public decimal? valor { get; set; }
        [DataMember]
        public int? estado { get; set; }
        [DataMember]
        public Int64? num_tran_anula { get; set; }
        [DataMember]
        public Int64? numero_recaudo { get; set; }
    }

}



