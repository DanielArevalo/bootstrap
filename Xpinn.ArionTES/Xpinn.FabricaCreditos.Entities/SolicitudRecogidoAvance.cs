using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    public class SolicitudRecogidoAvance
    {
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
        public Int64 Radicado { get; set; }


    }
}
