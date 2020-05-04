using System;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    public class TipoGarantias
    {
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public Int64 Codigo { get; set; }
      
    }
}
