using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad CosteoProductos
    /// </summary>
    [DataContract]
    [Serializable]
    public class CosteoProductos
    {
        [DataMember] 
        public Int64 cod_costeo { get; set; }
        [DataMember] 
        public Int64 cod_margen { get; set; }
        [DataMember] 
        public string materiaprima { get; set; }
        [DataMember] 
        public string unidadcompra { get; set; }
        [DataMember] 
        public Int64 costounidad { get; set; }
        [DataMember] 
        public Int64 cantidad { get; set; }
        [DataMember] 
        public Int64 costo { get; set; }

    }
}