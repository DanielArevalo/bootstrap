using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class ivimpuesto
    {
        [DataMember]
        public Int64 id_impuesto { get; set; }
        [DataMember]
        public string codigo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int? tipo_valor { get; set; }
        [DataMember]
        public decimal? valor { get; set; }
        [DataMember]
        public Int64? idparametro { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string nombre_cuenta { get; set; }
        [DataMember]
        public string cod_cuenta_niif { get; set; }
        [DataMember]
        public string nombre_cuenta_niif { get; set; }
    }
}