using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class AtributosCredito
    {
        [DataMember]
        public Int64 numero_radicacion { get; set; }
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
        [DataMember]
        public decimal? saldo_atributo { get; set; }
        [DataMember]
        public decimal? causado_atributo { get; set; }
        [DataMember]
        public decimal? orden_atributo { get; set; }
    }
}
