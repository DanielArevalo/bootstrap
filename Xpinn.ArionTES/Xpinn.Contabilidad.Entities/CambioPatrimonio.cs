using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{

    [DataContract]
    [Serializable]
    public class CambioPatrimonio
    {
        [DataMember]
        public String Descripcion_Moviminto { get; set; }
        [DataMember]
        public Decimal aporte_Sociales { get; set; }
        [DataMember]
        public Decimal reservas { get; set; }
        [DataMember]
        public Decimal fondos_Destinacion_especificas { get; set; }
        [DataMember]
        public Decimal valorizacion { get; set; }
        [DataMember]
        public Decimal excedentes_netos { get; set; }
        [DataMember]
        public Decimal totales { get; set; }

    }
}
