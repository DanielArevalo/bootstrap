using System;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    public class Persona
    {
        [DataMember]
        public Int32 CodigoUsuario { get; set; }
        [DataMember]
        public string Nombre { get; set; }
    }
}
