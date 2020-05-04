using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.CDATS.Entities
{
    [DataContract]
    [Serializable]
    public class ProrrogaCDAT
    {
        [DataMember]
        public Int64 cod_prorroga { get; set; }
        [DataMember]
        public Int64 codigo_cdat { get; set; }
        [DataMember]
        public DateTime fecha_inicio { get; set; }
        [DataMember]
        public DateTime fecha_final { get; set; }
        [DataMember]
        public string tipo_interes { get; set; }
        [DataMember]
        public decimal tasa_interes { get; set; }
        [DataMember]
        public int cod_tipo_tasa { get; set; }
        [DataMember]
        public int tipo_historico { get; set; }
        [DataMember]
        public decimal desviacion { get; set; }
        [DataMember]
        public int cod_periodicidad_int { get; set; }

        [DataMember]
        public Int64 cod_ope { get; set; }

    }
}
