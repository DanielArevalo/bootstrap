using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Obligaciones.Entities
{
    /// <summary>
    /// Entidad Componente Adicional
    /// </summary>
    [DataContract]
    [Serializable]
    public class ComponenteAdicional
    {
        [DataMember]
        public Int64 CODOBLIGACION { get; set; }
        [DataMember]
        public Int64 CODCOMPONENTE { get; set; }
        [DataMember]
        public String NOMCOMPONENTE { get; set; }
        [DataMember]
        public Int64 FORMULA { get; set; }
        [DataMember]
        public String NOMFORMULA { get; set; }
        [DataMember]
        public decimal VALOR{ get; set; }
        [DataMember]
        public Int64 FINANCIADO { get; set; }
        [DataMember]
        public decimal VALOR_CALCULADO{ get; set; }
        [DataMember]
        public string DESCRIPCION { get; set; }
    }
}
