using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad Credito
    /// </summary>
    [DataContract]
    [Serializable]
    public class Acta
    {
        [DataMember]
        public Int64 acta { get; set; }
        [DataMember]
        public String numero_obligacion { get; set; }       
        
    }
}