using System;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class Atributos
    {
        [DataMember]
        public Int64 cod_atr { get; set; }
        [DataMember]
        public string nom_atr { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public Int64 calculo_atr { get; set; }
        [DataMember]
        public Int64? tipo_historico { get; set; }
        [DataMember]
        public Double desviacion { get; set; }
        [DataMember]
        public Int64? tipo_tasa { get; set; }
        [DataMember]
        public Double tasa { get; set; }
        [DataMember]
        public Int64 cobra_mora { get; set; }
        [DataMember]
        public Int64? tipotasa { get; set; }
        [DataMember]
        public int? causa { get; set; }

        #region ValoresAdeudados
        [DataMember]
        public Double? valor { get; set; }
        [DataMember]
        public Double? causado { get; set; }
        [DataMember]
        public Boolean aplica { get; set; }
        #endregion
    }
}
