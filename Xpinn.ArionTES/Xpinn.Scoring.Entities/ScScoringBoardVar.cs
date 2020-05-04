using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Scoring.Entities
{
    /// <summary>
    /// Entidad ScScoringBoardVar
    /// </summary>
    [DataContract]
    [Serializable]
    public class ScScoringBoardVar
    {
        [DataMember]
        public Int64 idscorevar { get; set; } 
        [DataMember]
        public Int64 idscore { get; set; }
        [DataMember]
        public Int64 idparametro { get; set; }
        [DataMember]
        public Decimal minimo { get; set; }
        [DataMember]
        public Decimal maximo { get; set; }
        [DataMember]
        public Int64 valor { get; set; }
        [DataMember]
        public decimal beta { get; set; }
        [DataMember]
        public String descripcionParametro { get; set; }
        
    }
}