using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Xpinn.Asesores.Entities
{
    [DataContract]
    public class ConsultaAvance
    {
        [DataMember]
        public Int64 Numero_Radicacion { get; set; }
        [DataMember]
        public Int64 NumAvance { get; set; }
        [DataMember]
        public DateTime FechaDesembolsi { get; set; }
        [DataMember]
        public DateTime FechaProxPago { get; set; }
        [DataMember]
        public DateTime FechaUltiPago { get; set; }
        [DataMember]
        public Int64 ValDesembolso { get; set; }
        [DataMember]
        public Int64 Plazo { get; set; }
        [DataMember]
        public Int64 CuotasPagadas { get; set; }
        [DataMember]
        public Int64 CuotasPendi { get; set; }
        [DataMember]
        public Int64 SaldoAvance { get; set; }
        [DataMember]
        public Decimal Tasa { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public Int64 ValorCuota { get; set; }
        [DataMember]
        public Int64 ValorTotal { get; set; }
        [DataMember]
        public Int64 Intereses { get; set; }
        [DataMember]
        public DateTime Fecha_Solicitud { get; set; }
        [DataMember]
        public DateTime fecha_Aprobacion { get; set; }


    }
}
