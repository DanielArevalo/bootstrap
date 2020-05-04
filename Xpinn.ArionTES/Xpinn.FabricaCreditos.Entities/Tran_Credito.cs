using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{

    [DataContract]
    [Serializable]
    public class TRAN_CRED
    {
        [DataMember]
        public int num_tran { get; set; }
        [DataMember]
        public int cod_ope { get; set; }
        [DataMember]
        public int numero_radicacion { get; set; }
        [DataMember]
        public int cod_cliente { get; set; }
        [DataMember]
        public string cod_linea_credito { get; set; }
        [DataMember]
        public int tipo_tran { get; set; }
        [DataMember]
        public int cod_det_lis { get; set; }
        [DataMember]
        public int cod_atr { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public decimal valor_mes { get; set; }
        [DataMember]
        public decimal valor_causa { get; set; }
        [DataMember]
        public decimal valor_orden { get; set; }
        [DataMember]
        public int estado { get; set; }
        [DataMember]
        public int num_tran_anula { get; set; }
    }

}