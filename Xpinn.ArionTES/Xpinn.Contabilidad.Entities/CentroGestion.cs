using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class CentroGestion
    {
        [DataMember]
        public int centro_gestion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public int nivel { get; set; }
        [DataMember]
        public DateTime fechacreacion { get; set; }
        [DataMember]
        public int depende_de { get; set; }
        [DataMember]
        public string nom_depende { get; set; }
    }
}