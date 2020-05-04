using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Xpinn.Asesores.Entities.Common;

namespace Xpinn.Asesores.Entities
{
    [DataContract]
    [Serializable]
    public class Codeudor
    {
        public Codeudor()
        {
            Persona = new Persona();
        }

        [DataMember]
        public Int64 IdCodeudor { get; set; }
        [DataMember]
        public Int64 NumeroRadicacion { get; set; }
        [DataMember]
        public Persona Persona { get; set; }
        [DataMember]
        public string TipoCodeudor { get; set; }
        [DataMember]
        public int parentesco { get; set; }
        [DataMember]
        public string opinion { get; set; }
        [DataMember]
        public string responsabilidad { get; set; }
        [DataMember]
        public int orden { get; set; }
    }
}