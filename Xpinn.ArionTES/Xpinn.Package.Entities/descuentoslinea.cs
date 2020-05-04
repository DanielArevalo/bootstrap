using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Package.Entities
{
    [DataContract]
    [Serializable]
    public class descuentoslinea
    {
        [DataMember]
        public string cod_linea_credito { get; set; }
        [DataMember]
        public int? cod_atr { get; set; }
        [DataMember]
        public int? tipo_liquidacion { get; set; }
        [DataMember]
        public decimal? valor { get; set; }
        [DataMember]
        public int? cobra_mora { get; set; }
        [DataMember]
        public int? numero_cuotas { get; set; }
        [DataMember]
        public string forma_descuento { get; set; }
        [DataMember]
        public int? tipo_impuesto { get; set; }
        [DataMember]
        public int? tipo_descuento { get; set; }
        [DataMember]
        public int? modifica { get; set; }
    }
}