using System;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    public class PlanCuentas
    {
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public Int64 Codigo { get; set; }
    }
}
