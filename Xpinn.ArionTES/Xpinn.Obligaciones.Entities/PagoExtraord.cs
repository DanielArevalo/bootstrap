using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Obligaciones.Entities
{
    /// <summary>
    /// Entidad Linea Obligacion
    /// </summary>
    [DataContract]
    [Serializable]
    public class PagoExtraord
    {
        [DataMember]
        public Int64 IDOBCUOTAEXTRA { get; set; }
        [DataMember]
        public Int64 CODOBLIGACION { get; set; }
        [DataMember]
        public Int64 COD_PERIODICIDAD { get; set; }
        [DataMember]
        public String NOM_PERIODICIDAD { get; set; }
        [DataMember]
        public decimal VALOR { get; set; }  
    }
}
