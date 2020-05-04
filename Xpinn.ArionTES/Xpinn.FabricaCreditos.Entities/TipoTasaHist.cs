using System;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class TipoTasaHist
    {
        [DataMember]
        public Int64 tipo_historico { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public Int64 tipo_tasa { get; set; }

    }
}
