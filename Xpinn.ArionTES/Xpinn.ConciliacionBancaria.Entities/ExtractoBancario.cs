using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.ConciliacionBancaria.Entities
{
    [DataContract]
    [Serializable]
    public class ExtractoBancario
    {
        [DataMember]
        public Int64 idextracto { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public int cod_banco { get; set; }
        [DataMember]
        public string numero_cuenta { get; set; }
        [DataMember]
        public decimal saldo_anterior { get; set; }
        [DataMember]
        public decimal? debitos { get; set; }
        [DataMember]
        public decimal? creditos { get; set; }
        [DataMember]
        public string periodo { get; set; }
        [DataMember]
        public int mes { get; set; }
        [DataMember]
        public int? dia { get; set; }
        [DataMember]
        public int estado { get; set; }
        [DataMember]
        public int codusuariocreacion { get; set; }
        [DataMember]
        public DateTime fechacreacion { get; set; }
        [DataMember]
        public List<DetExtractoBancario> lstDetalle { get; set; }


        //AGREGADO
        [DataMember]
        public string nomestado { get; set; }
        [DataMember]
        public string nombanco { get; set; }
    }


}
