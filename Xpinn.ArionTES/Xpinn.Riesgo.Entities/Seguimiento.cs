using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Riesgo.Entities
{
    public class Seguimiento
    {
        [DataMember]
        public Int64 cod_control { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public Int64 clase { get; set; }
        [DataMember]
        public Int64 cod_area { get; set; }
        [DataMember]
        public Int64 cod_cargo { get; set; }
        [DataMember]
        public int grado_aceptacion { get; set; }
        [DataMember]
        public Int64 cod_monitoreo { get; set; }
        [DataMember]
        public int cod_alerta { get; set; }
        [DataMember]
        public int periodicidad { get; set; }
        [DataMember]
        public string nom_area { get; set; }
        [DataMember]
        public string nom_cargo { get; set; }
        [DataMember]
        public string nom_clase { get; set; }
        [DataMember]
        public string nom_alerta { get; set; }
        [DataMember]
        public string nom_periodicidad { get; set; }
    }
}
