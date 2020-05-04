using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Caja.Entities
{
    [DataContract]
    [Serializable]
    public class Caja
    {
        [DataMember]
        public Int64 IdCaja { get; set; }
        [DataMember]
        public string cod_caja { get; set; }
        [DataMember]
        public string desc_cuenta_contable { get; set; }
        [DataMember]
        public string cod_cuenta_contable { get; set; }
        [DataMember]
        public Int64 cod_cajero { get; set; }
        [DataMember]
        public Int64 cod_caja_principal { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public Int64 estado { get; set; }
        [DataMember]
        public string state { get; set; }
        [DataMember]
        public Int64 esprincipal { get; set; }
        [DataMember]
        public DateTime fecha_creacion { get; set; }
        [DataMember]
        public decimal valor_minimo { get; set; }
        [DataMember]
        public decimal valor_maximo { get; set; }
        [DataMember]
        public Int64 escajaprincip { get; set; }
        [DataMember]
        public string nombre_ofi { get; set; }
        [DataMember]
        public string cod_datafono { get; set; }
    }
}
