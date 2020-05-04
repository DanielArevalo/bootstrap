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
    public class SeguimientoScoring
    {
        [DataMember]
        public DateTime fecha { get; set; }   //Parametro util para modificacion. Permite seleccionar que campos se desean actualizar en la tabla
        [DataMember]
        public String estado { get; set; }
        [DataMember]
        public String usuario { get; set; }
        [DataMember]
        public String motivo { get; set; }
        [DataMember]
        public String observaciones { get; set; }
      


    }
}