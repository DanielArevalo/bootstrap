using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class PlanesSegurosAmparos
    {
        [DataMember]
         public Int64 consecutivo  { get; set; }
        [DataMember]
         public Int64 tipo_plan { get; set; }
        [DataMember]
         public string tipo { get; set; }
        [DataMember]
         public string descripcion { get; set; }
        [DataMember]
        public Int64 valor_cubierto { get; set; }
        [DataMember]
        public Int64 valor_cubierto_conyuge { get; set; }
        [DataMember]
        public Int64 valor_cubierto_hijos { get; set; }
    }
}
