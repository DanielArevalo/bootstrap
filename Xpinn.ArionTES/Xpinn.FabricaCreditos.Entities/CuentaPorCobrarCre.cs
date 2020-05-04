using System;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class CuentaPorCobrarCre
    {
        [DataMember]
        public Int64 idcuenta { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public Int64 tipo_cta { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public DateTime fecha_cta { get; set; }
        [DataMember]
        public Double total { get; set; }
        [DataMember]
        public Double saldo { get; set; }
        [DataMember]
        public Int64 cod_usuario { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }

    }
}
