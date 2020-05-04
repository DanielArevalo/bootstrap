using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class Pago_IntPermanente
    {
        [DataMember]
        public int idpagperm { get; set; }
        [DataMember]
        public Int64 numero_transaccion { get; set; }
        [DataMember]
        public Int64 numero_aporte { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public decimal? valor { get; set; }
        [DataMember]
        public int estado { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }

        [DataMember]
        public Int64? identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }

        [DataMember]
        public Int64 cod_atr { get; set; }
        [DataMember]
        public Int64 tipo_tran { get; set; }
    }
}


