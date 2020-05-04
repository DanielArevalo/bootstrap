using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    /// <summary>
    /// Datos de los conceptos
    /// </summary>
    [DataContract]
    [Serializable]
    public class Concepto
    {
        [DataMember]
        public Int64 concepto { get; set; }
        [DataMember]
        public string descripcion { get; set; }

    }
}
