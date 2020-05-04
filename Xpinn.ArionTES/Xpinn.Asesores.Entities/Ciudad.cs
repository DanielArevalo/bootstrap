using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities
{
    //actual1
    /// <summary>
    /// Entidad Ciudad
    /// </summary>
    [DataContract]
    [Serializable]
    public class Ciudad
    {
        [DataMember]
        public Int64 codciudad { get; set; }
        [DataMember]
        public string nomciudad { get; set; }
        [DataMember]
        public Int64 tipo { get; set; }

    }
}