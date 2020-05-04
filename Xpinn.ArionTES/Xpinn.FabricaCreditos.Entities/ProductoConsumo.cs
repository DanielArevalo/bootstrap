using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad Familiares
    /// </summary>
    [DataContract]
    [Serializable]
    public class ProductoConsumo
    {
        [DataMember]
        public Int32 id_producto { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public Int32 estado { get; set; }
        [DataMember]
        public Int32 id_cat_producto { get; set; }
    }
}