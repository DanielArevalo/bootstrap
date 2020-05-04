using System;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class TipoTasa
    {
        [DataMember]
        public Int64 cod_tipo_tasa { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string efectiva_nomina { get; set; }
        [DataMember]
        public Int64 cod_periodicidad { get; set; }
        [DataMember]
        public string modalidad { get; set; }
        [DataMember]
        public Int64 cod_periodicidad_cap { get; set; }

    }
}
