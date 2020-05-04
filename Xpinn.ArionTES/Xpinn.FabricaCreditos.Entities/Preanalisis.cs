using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad Persona1
    /// </summary>
    [DataContract]
    [Serializable]
    public class Preanalisis
    {
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public String identificacion { get; set; }
        [DataMember]
        public decimal disponible { get; set; }
        [DataMember]
        public decimal cuotaCreditoCancelado { get; set; }
        [DataMember]
        public decimal cuotaServicios { get; set; }
    }

}