using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Obligaciones.Entities
{
    /// <summary>
    /// Entidad Tipo Liquidacion
    /// </summary>
    [DataContract]
    [Serializable]
    public class TipoTasa
    {
        [DataMember]
        public Int64 COD_TIPO_TASA { get; set; }
        [DataMember]
        public string NOMBRE { get; set; }   
    }
}
