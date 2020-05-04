using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class LiquidacionPrima
    {
        [DataMember]
        public long consecutivo { get; set; }
        [DataMember]
        public int codigousuariocreacion { get; set; }
        [DataMember]
        public DateTime fechageneracion { get; set; }
        [DataMember]
        public long codigonomina { get; set; }
        [DataMember]
        public long codigocentrocosto { get; set; }
        [DataMember]
        public decimal valortotalpagar { get; set; }
        [DataMember]
        public DateTime fechapago { get; set; }
        [DataMember]
        public Int64 anio { get; set; }
        [DataMember]
        public int semestre { get; set; }
        public string desc_nomina { get; set; }
        public string desc_usuario { get; set; }
        public string desc_centro_costo { get; set; }
        public string desc_semestre { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }

        [DataMember]
        public Int64 codorigen { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }

        [DataMember]
        public Int64 num_comp { get; set; }

        [DataMember]
        public Int64 tipo_comp { get; set; }
    }
}