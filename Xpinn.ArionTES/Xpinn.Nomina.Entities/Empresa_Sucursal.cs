using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class Empresa_Sucursal
    {
        [DataMember]
        public Int64 consecutivo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public Int64? ciudad { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string Email { get; set; }


    }
}
