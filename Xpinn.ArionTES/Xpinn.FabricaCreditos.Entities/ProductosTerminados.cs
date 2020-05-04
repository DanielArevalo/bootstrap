using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad ProductosTerminados
    /// </summary>
    [DataContract]
    [Serializable]
    public class ProductosTerminados
    {
        [DataMember] 
        public Int64 cod_prodter { get; set; }
        [DataMember] 
        public Int64 cod_inffin { get; set; }
        [DataMember] 
        public Int64 cantidad { get; set; }
        [DataMember] 
        public string producto { get; set; }
        [DataMember] 
        public Int64 vrunitario { get; set; }
        [DataMember] 
        public Int64 vrtotal { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }

    }
}