using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class ivconceptos
    {
        [DataMember]
        public Int64? id_concepto { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public decimal base_minima { get; set; }
        [DataMember]
        public decimal porcentaje_calculo { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string cod_cuenta_niif { get; set; }
        [DataMember]
        public string nombre_cuenta { get; set; }
    }


}
