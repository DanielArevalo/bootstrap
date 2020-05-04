using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Scoring.Entities
{
    /// <summary>
    /// Entidad DefinirVariables
    /// </summary>
    [DataContract]
    [Serializable]
    public class Variables
    {
        //Tabla scvariable
        [DataMember]
        public Int64 idvariable { get; set; } 
        [DataMember]
        public String variable { get; set; }       
        [DataMember]
        public String nombre { get; set; }
        [DataMember]
        public Int64 tipo { get; set; }
        [DataMember]
        public String sentencia { get; set; }
       
    }
}