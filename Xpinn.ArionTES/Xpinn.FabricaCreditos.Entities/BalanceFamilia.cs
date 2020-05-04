using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad BalanceFamilia
    /// </summary>
    [DataContract]
    [Serializable]
    public class BalanceFamilia
    {
        [DataMember] 
        public Int64 cod_balance { get; set; }
        [DataMember] 
        public Int64 cod_persona { get; set; }
        [DataMember] 
        public Int64 terrenosyedificios { get; set; }
        [DataMember] 
        public Int64 otros { get; set; }
        [DataMember] 
        public Int64 totalactivo { get; set; }
        [DataMember] 
        public Int64 corriente { get; set; }
        [DataMember] 
        public Int64 largoplazo { get; set; }
        [DataMember] 
        public Int64 totalpasivo { get; set; }
        [DataMember] 
        public Int64 patrimonio { get; set; }
        [DataMember] 
        public Int64 totalpasivoypatrimonio { get; set; }        
        [DataMember]
        public Int64 cod_inffin { get; set; }
    }
}