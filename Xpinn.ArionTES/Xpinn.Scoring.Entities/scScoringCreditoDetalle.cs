using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Scoring.Entities
{
    /// <summary>
    /// Entidad ScScoringBoard
    /// </summary>
    [DataContract]
    [Serializable]
    public class ScScoringCreditoDetalle
    {
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public Int64 idparametro { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string valor { get; set; }
        [DataMember]
        public double puntaje { get; set; }

    }
}