using System;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    public class CuentasContaGarantias
    {
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public Int64 consecutivo { get; set; }
        [DataMember]
        public string Tipo { get; set; }
        [DataMember]
        public Int64 CtaDebito { get; set; }
        [DataMember]
        public Int64 CtaCredito { get; set; }
    }
}
