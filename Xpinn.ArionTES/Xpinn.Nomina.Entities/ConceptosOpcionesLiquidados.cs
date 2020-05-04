using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class ConceptosOpcionesLiquidados
    {
        [DataMember]
        public long consecutivoOpcion { get; set; }
        [DataMember]
        public long tipoOpcion { get; set; }
        [DataMember]
        public decimal valor { get; set; }

        [DataMember]
        public DateTime fecha { get; set; }
    }
}
