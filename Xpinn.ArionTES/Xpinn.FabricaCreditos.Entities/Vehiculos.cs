using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad Vehiculos
    /// </summary>
    [DataContract]
    [Serializable]
    public class Vehiculos
    {
        [DataMember] 
        public Int64 cod_vehiculo { get; set; }
        [DataMember] 
        public Int64 cod_persona { get; set; }
        [DataMember] 
        public string marca { get; set; }
        [DataMember] 
        public string placa { get; set; }
        [DataMember] 
        public Int64 modelo { get; set; }
        [DataMember] 
        public Int64 valorcomercial { get; set; }
        [DataMember] 
        public Int64 valorprenda { get; set; }

    }
}