using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Auxilios.Entities
{
    [DataContract]
    [Serializable]
    public class DesembolsoAuxilio
    {
        [DataMember]
        public Int64 numero_transaccion { get; set; }
        [DataMember]
        public Int64 numero_auxilio { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public Int64 cod_cliente { get; set; }
        [DataMember]
        public string cod_linea_auxilio { get; set; }
        [DataMember]
        public Int64 tipo_tran { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public int estado { get; set; }
        [DataMember]
        public Int64 num_tran_anula { get; set; }
        [DataMember]
        public long? numero_cuenta_ahorro_vista { get; set; }

        
    }
}
