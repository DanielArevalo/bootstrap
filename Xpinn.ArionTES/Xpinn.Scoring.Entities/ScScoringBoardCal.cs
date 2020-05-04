using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Scoring.Entities
{
    /// <summary>
    /// Entidad ScScoringBoardCal
    /// </summary>
    [DataContract]
    [Serializable]
    public class ScScoringBoardCal
    {
        [DataMember]
        public Int64 idscorecal { get; set; } 
        [DataMember]
        public Int64 idscore { get; set; }       
        [DataMember]
        public Decimal cal_minimo { get; set; }
        [DataMember]
        public Decimal cal_maximo { get; set; }
        [DataMember]
        public String calificacion { get; set; }
        [DataMember]
        public Int64 tipo { get; set; }
        [DataMember]
        public String observacion { get; set; }
        [DataMember]
        public String descripcionTipo { get; set; } 
    }
}