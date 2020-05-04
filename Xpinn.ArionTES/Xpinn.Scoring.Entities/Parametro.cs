using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Scoring.Entities
{
    /// <summary>
    /// Entidad parametro
    /// </summary>
    [DataContract]
    [Serializable]
    public class Parametro
    {

        //Tabla scparametro
        [DataMember]
        public Int64 idparametro { get; set; }
        [DataMember]
        public String tipo { get; set; }
        [DataMember]
        public String nombre { get; set; }
        [DataMember]
        public Int64 idvariable { get; set; }
        [DataMember]
        public String formula { get; set; }
        [DataMember]
        public String sentencia { get; set; }
        [DataMember]
        public String campo { get; set; }
    }
}
