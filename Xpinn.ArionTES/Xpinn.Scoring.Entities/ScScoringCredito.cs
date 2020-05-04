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
    public class ScScoringCredito
    {
        [DataMember]
        public Int64 idscorecre { get; set; }   //Parametro util para modificacion. Permite seleccionar que campos se desean actualizar en la tabla
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public Int64 cod_cliente { get; set; }
        [DataMember]
        public Int64 idscore { get; set; }
        [DataMember]
        public Int64 modelo { get; set; }
        [DataMember]
        public DateTime fecha_scoring { get; set; }
        [DataMember]
        public Int64 clase_scoring { get; set; }
        [DataMember]
        public double resultado { get; set; }
        [DataMember]
        public double calificacion_cliente { get; set; }
        [DataMember]
        public string calificacion { get; set; }
        [DataMember]
        public Int64 tipo { get; set; }
        [DataMember]
        public string observacion { get; set; }
        [DataMember]
        public Int64 estado { get; set; }
        [DataMember]
        public string error { get; set; }
        [DataMember]
        public DateTime fechacreacion { get; set; }
        [DataMember]
        public String usuariocreacion { get; set; }
        [DataMember]
        public DateTime fecultmod { get; set; }
        [DataMember]
        public String usuultmod { get; set; }

    }
}