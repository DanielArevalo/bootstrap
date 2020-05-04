using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Scoring.Entities
{
    [DataContract]
    [Serializable]
    public class ScoringCreditos
    {
        [DataMember]
        public Int64 Numero_radicacion { get; set; }
        [DataMember]
        public Int64 Cod_Cliente { get; set; }
        [DataMember]
        public string Identificacion { get; set; }
        [DataMember]
        public string Nombres { get; set; }
        [DataMember]
        public string LineaCredito { get; set; }
        [DataMember]
        public string Linea { get; set; }
        [DataMember]
        public Int64 Monto { get; set; }
        [DataMember]
        public Int64 Plazo { get; set; }
        [DataMember]
        public Int64 Cuota { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public string Periodicidad { get; set; }
        [DataMember]
        public string FormaPago { get; set; }
        [DataMember]
        public DateTime FechaInicio { get; set; }
        [DataMember]
        public Int64 DiasAjuste { get; set; }
        [DataMember]
        public Int64 TasaInteres { get; set; }
        [DataMember]
        public Int64 LeyMiPyme { get; set; }
        [DataMember]
        public string Moneda { get; set; }

        [DataMember]
        public Int64 NumeroSolicitud { get; set; }
        [DataMember]
        public Int64 Numero_Radicacion { get; set; }
        [DataMember]
        public DateTime FechaSolicitud { get; set; }
        [DataMember]
        public string Direccion { get; set; }
        [DataMember]
        public string Telefono { get; set; }
        [DataMember]
        public string Oficina { get; set; }
        [DataMember]
        public string Ejecutivo { get; set; }
        [DataMember]
        public string DescripcionIdentificacion { get; set; }  
        [DataMember]
        public Int64 saldocapital { get; set; }
        [DataMember]
        public Int64 cuotas_pendientes { get; set; }   
        [DataMember]
        public Int64 valorrecoge { get; set; }
        [DataMember]
        public Int64 puntscoring { get; set; } 
    }

    [DataContract]
    [Serializable()]
    public class MatrizColor
    {
        [DataMember]
        public string calificacion { get; set; }
        [DataMember]
        public string color { get; set; }
    }

}
