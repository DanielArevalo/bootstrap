using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Icetex.Entities
{
    [DataContract]
    [Serializable]
    public class Reporte
    {
        [DataMember]
        public Int64 numero_credito { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public decimal monto { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public Int64 codigo { get; set; }
        [DataMember]
        public string nom_asociado { get; set; }
        [DataMember]
        public string nom_beneficiario { get; set; }
        [DataMember]
        public string programa { get; set; }
        [DataMember]
        public string tipo_beneficiario { get; set; }
        [DataMember]
        public string institucion_univ { get; set; }
        [DataMember]
        public int semestre { get; set; }
        [DataMember]
        public int estrato { get; set; }
        [DataMember]
        public string observacion { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string esconforme { get; set; }
        
    }
}
