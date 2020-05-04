using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class DescuentosLinea
    {
      
        [DataMember]
        public int? cod_atr { get; set; }
        [DataMember]
        public string nom_atr { get; set; }
        [DataMember]
        public decimal? val_atr { get; set; }
        [DataMember]
        public int? tipo_atr { get; set; }                
        [DataMember]
        public int? tipo_descuento { get; set; }
        [DataMember]
        public int? tipo_liquidacion { get; set; }
        [DataMember]
        public int cobra_mora { get; set; }
        [DataMember]
        public decimal? numero_cuotas { get; set; }
        [DataMember]
        public int forma_descuento { get; set; }
        [DataMember]
        public int tipo_impuesto { get; set; }
        [DataMember]
        public int cod_linea { get; set; }
    }
}