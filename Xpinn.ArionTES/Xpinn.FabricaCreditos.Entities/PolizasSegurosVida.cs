using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class PolizasSegurosVida
    {
        [DataMember]
        public Int64 consecutivo { get; set; }
        [DataMember]
        public Int64 cod_poliza { get; set; }
        [DataMember]
        public Int64 tipo_plan { get; set; }
        [DataMember]
        public String tipo { get; set; }
        [DataMember]
        public String individual { get; set; }
        [DataMember]
        public Int64 valor_prima { get; set; }
      }

}

