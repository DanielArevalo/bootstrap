using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class CajaDeCompensacion
    {
        [DataMember]
        public Int64 consecutivo { get; set; }
        [DataMember]
        public string nit { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string codigopila { get; set; }
        [DataMember]
        public string nombre { get; set; }

        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string cod_cuenta_contra { get; set; }
        [DataMember]
        public int? tipo_mov { get; set; }
    }
}