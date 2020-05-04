using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Scoring.Entities
{
    [DataContract]
    [Serializable]
    public class AprobacionScoringNegados
    {       
        [DataMember]
        public Int64 NumeroSolicitud { get; set; }
        [DataMember]
        public string Identificacion { get; set; }
        [DataMember]
        public string Nombres { get; set; }
        [DataMember]
        public Int64 Monto { get; set; }
        [DataMember]
        public Int64 Plazo { get; set; }
        [DataMember]
        public Int64 puntscoring { get; set; } 

        //Variables para la tabla "ControlCreditos"
        [DataMember]
        public Int64 idControl { get; set; }
        [DataMember]
        public String codTipoProceso { get; set; }
        [DataMember]
        public DateTime fechaProceso { get; set; }
        //[DataMember]
        //public Int64 codPersona { get; set; }
        [DataMember]
        public Int64 codMotivo { get; set; }
        [DataMember]
        public String observaciones { get; set; }
        [DataMember]
        public String anexos { get; set; }
        [DataMember]
        public Int64 nivel { get; set; } 

        //Listas desplegables:
        [DataMember]
        public string ListaDescripcion { get; set; }
        [DataMember]
        public Int64 ListaId { get; set; }
        [DataMember]
        public string ListaIdStr { get; set; }
    }
}
