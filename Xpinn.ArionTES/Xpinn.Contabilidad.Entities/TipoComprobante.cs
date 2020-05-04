using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    /// <summary>
    /// Entidad de Tipos de Comprobante
    /// </summary>
    [DataContract]
    [Serializable]
    public class TipoComprobante
    {
        [DataMember]
        public Int64? tipo_comprobante { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public Int64? tipo_norma { get; set; }
        [DataMember]
        public string nomtipo_norma { get; set; }
    }
}