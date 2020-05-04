using System;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class Periodicidad
    {
        [DataMember]
        public Int32 Codigo { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public double numero_dias { get; set; }
        [DataMember]
        public double numero_meses { get; set; }
        [DataMember]
        public double periodos_anuales { get; set; }
        [DataMember]
        public double tipo_calendario { get; set; }
        [DataMember]
        public string calendario { get; set; }

    }
}
