using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Package.Entities
{
    [DataContract]
    [Serializable]
    public class atributoslinea
    {
        [DataMember]
        public string cod_linea_credito { get; set; }
        [DataMember]
        public int cod_rango_atr { get; set; }
        [DataMember]
        public int cod_atr { get; set; }
        [DataMember]
        public string calculo_atr { get; set; }
        [DataMember]
        public int? tipo_historico { get; set; }
        [DataMember]
        public decimal? desviacion { get; set; }
        [DataMember]
        public int? tipo_tasa { get; set; }
        [DataMember]
        public decimal? tasa { get; set; }
        [DataMember]
        public int? cobra_mora { get; set; }
    }
}