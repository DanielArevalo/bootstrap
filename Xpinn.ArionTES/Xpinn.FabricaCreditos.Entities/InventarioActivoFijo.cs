using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad InventarioActivoFijo
    /// </summary>
    [DataContract]
    [Serializable]
    public class InventarioActivoFijo
    {
        [DataMember] 
        public Int64 cod_activo { get; set; }
        [DataMember] 
        public Int64 cod_inffin { get; set; }
        [DataMember] 
        public string descripcion { get; set; }
        [DataMember] 
        public string marca { get; set; }
        [DataMember] 
        public Int64 valor { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }

    }
}