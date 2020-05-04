using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities
{
    [DataContract]
    [Serializable]
    public class ComprobantePagoExtracto
    {

        [DataMember]
        public Int64 NumeroDeCredito { get; set; }
        [DataMember]
        public DateTime fecha_corte { get; set; }
        [DataMember]
        public DateTime FechaLimiteDePago { get; set; }
        [DataMember]
        public double PagoMinimo { get; set; }
        [DataMember]
        public double PagoTotal { get; set; }
        [DataMember]
        public string CadenaCodigoDeBarras { get; set; }

    }
}