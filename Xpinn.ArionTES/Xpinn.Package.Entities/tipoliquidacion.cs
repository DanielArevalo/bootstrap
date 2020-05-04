using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Package.Entities
{
    [DataContract]
    [Serializable]
    public class tipoliquidacion
    {
        [DataMember]
        public int tipo_liquidacion { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int? tipo_cuota { get; set; }
        [DataMember]
        public int? tipo_pago { get; set; }
        [DataMember]
        public int tipo_interes { get; set; }
        [DataMember]
        public int? tipo_intant { get; set; }
        [DataMember]
        public decimal? valor_gradiente { get; set; }
        [DataMember]
        public int? tip_gra { get; set; }
        [DataMember]
        public int? tip_amo { get; set; }
    }
}