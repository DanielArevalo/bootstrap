using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad BienesRaices
    /// </summary>
    [DataContract]
    [Serializable]
    public class BienesRaices
    {
        [DataMember] 
        public Int64 cod_bien { get; set; }
        [DataMember] 
        public Int64 cod_persona { get; set; }
        [DataMember] 
        public string tipo { get; set; }
        [DataMember] 
        public string matricula { get; set; }
        [DataMember] 
        public Int64 valorcomercial { get; set; }
        [DataMember] 
        public Int64 valorhipoteca { get; set; }

    }
}