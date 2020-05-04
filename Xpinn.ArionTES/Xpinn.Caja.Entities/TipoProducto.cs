using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Caja.Entities
{
    /// <summary>
    /// Entidad Tipo de Producto
    /// </summary>
    [DataContract]
    [Serializable]
    public class TipoProducto
    {
        [DataMember]
        public Int64 cod_tipo_producto { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public Int64 cod_caja { get; set; }
    }
}