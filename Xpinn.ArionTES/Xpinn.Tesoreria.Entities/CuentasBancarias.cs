using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class CuentasBancarias
    {
        [DataMember]
        public int idctabancaria { get; set; }
        [DataMember]
        public int cod_banco { get; set; }
        [DataMember]
        public string num_cuenta { get; set; }
        [DataMember]
        public int tipo_cuenta { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public string nombrecuenta { get; set; }
        [DataMember]
        public string nombrebanco { get; set; }
    }

}


