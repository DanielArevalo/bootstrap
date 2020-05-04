using System;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class HistoricoTasa
    {
        [DataMember]
        public Int64 IDHISTORICO { get; set; }
        [DataMember]
        public Int64 TIPO_HISTORICO { get; set; }
        [DataMember]
        public DateTime FECHA_INICIAL { get; set; }
        [DataMember]
        public DateTime FECHA_FINAL { get; set; }
        [DataMember]
        public decimal VALOR { get; set; }
        [DataMember]
        public Int64 TIPODEHISTORICO { get; set; }
        [DataMember]
        public string DESCRIPCION { get; set; }
    }
}
