using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class GiroRealizado
    {
        [DataMember]
        public int idgirorealizado { get; set; }
        [DataMember]
        public Int64 idgiro { get; set; }
        [DataMember]
        public DateTime fec_realiza { get; set; }
        [DataMember]
        public string usu_realiza { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public string archivo { get; set; }
    }
}