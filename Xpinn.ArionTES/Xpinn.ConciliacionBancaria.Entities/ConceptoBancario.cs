using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.ConciliacionBancaria.Entities
{
    [DataContract]
    [Serializable]
    public class ConceptoBancario
    {
        [DataMember]
        public string cod_concepto { get; set; }
        [DataMember]
        public int cod_banco { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string tipo_concepto { get; set; }
        [DataMember]
        public int id_conceptobancario { get; set; }
    }
}