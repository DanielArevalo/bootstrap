using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Caja.Entities
{
    [DataContract]
    [Serializable]
    public class Bancos
    {
        [DataMember]
        public Int64 IdBanco { get; set; }
        [DataMember]
        public Int64 cod_banco { get; set; }
        [DataMember]
        public string num_cuenta { get; set; }
        [DataMember]
        public string nombrebanco { get; set; }
        [DataMember]
        public Int64 cobra_comision { get; set; }
        
        //AGREGADO
        [DataMember]
        public int ctabancaria { get; set; }


    }
}
