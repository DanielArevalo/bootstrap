using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class ivordenconcepto
    {
        [DataMember]
        public Int64 id_ordcon { get; set; }
        [DataMember]
        public Int64 ordencompra_id { get; set; }
        [DataMember]
        public Int64 id_concepto { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public double base_minima { get; set; }
        [DataMember]
        public double porcentaje_calculo{ get; set; }
        [DataMember]
        public Int32 tipo { get; set; }
        [DataMember]
        public double valor { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string cod_cuenta_niif { get; set; }
    }
}
