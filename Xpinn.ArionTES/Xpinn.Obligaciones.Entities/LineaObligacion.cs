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
    public class LineaObligacion
    {
        [DataMember]
        public Int64 CODLINEAOBLIGACION { get; set; }
        [DataMember]
        public string NOMBRELINEA { get; set; }
        [DataMember]
        public Int64 TIPOMONEDA { get; set; }
        [DataMember]
        public string NOMTIPOMONEDA { get; set; }
        [DataMember]
        public Int64 TIPOLIQUIDACION { get; set; }
        [DataMember]
        public string NOMTIPOLIQUIDACION { get; set; }
        [DataMember]
        public Int64 conteo { get; set; }
    }
}
