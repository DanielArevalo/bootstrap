using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{

    [DataContract]
    [Serializable]
    public class EstFlujoEfectivo
    {
        [DataMember]
        public String descripcion { get; set; }

        [DataMember]
        public Decimal valor1 { get; set; }

        [DataMember]
        public Decimal valor2 { get; set; }

        [DataMember]
        public Decimal variacion { get; set; }

        [DataMember]
        public String Descripcion2 { get; set; }
        [DataMember]
        public Decimal caja { get; set; }
        [DataMember]
        public Decimal bancos_Comerciales { get; set; }
        [DataMember]
        public Decimal total { get; set; }

        [DataMember]
        public DateTime fecha { get; set; }
    }
}
