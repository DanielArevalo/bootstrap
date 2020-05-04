using System;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class Aprobador
    {
        [DataMember]
        public Int64 Id { get; set; }
        [DataMember]
        public string CodLineaCredito { get; set; }
        [DataMember]
        public string LineaCredito { get; set; }
        [DataMember]
        public string UsuarioAprobador { get; set; }
        [DataMember]
        public Int32 codusuaAprobador { get; set; }
        [DataMember]
        public Int32 Nivel { get; set; }
        [DataMember]
        public decimal MontoMinimo { get; set; }
        [DataMember]
        public decimal MontoMaximo { get; set; }
        [DataMember]
        public Int32 Aprueba { get; set; }
        [DataMember]
        public String Nombre { get; set; }
    }
}
