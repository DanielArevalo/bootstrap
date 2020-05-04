using System;
using System.Runtime.Serialization;

namespace Xpinn.Servicios.Entities
{
    [DataContract]
    [Serializable]
    public class SolicitudCredServRecogidos
    {
        [DataMember]
        public long consecutivo { get; set; }
        [DataMember]
        public long numeroradicacion { get; set; }
        [DataMember]
        public Int64 numeroservicio { get; set; }
        [DataMember]
        public decimal valorrecoger { get; set; }
        [DataMember]
        public DateTime fecharecoger { get; set; }
        [DataMember]
        public decimal saldoservicio { get; set; }
        [DataMember]
        public decimal interessevicio { get; set; }
        [DataMember]
        public string nom_periodicidad { get; set; }
        [DataMember]
        public int numero_cuotas { get; set; }
        [DataMember]
        public decimal valor_cuota { get; set; }
        [DataMember]
        public DateTime fecha_solicitud { get; set; }
        [DataMember]
        public string forma_pago { get; set; }
        public string nom_linea { get; set; }
    }
}
