using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;

namespace Xpinn.FabricaCreditos.Entities
{   
    [DataContract]
    [Serializable]
    public class CreditoEducativoEntit
    {
        [DataMember]
        public DateTime PFECHA_SOLICITUD { get; set; }
        [DataMember]
        public Int64 PCOD_DEUDOR { get; set; }
        [DataMember]
        public decimal PVALOR_MATRICULA { get; set; }
        [DataMember]
        public decimal PMONTO_SOLICITADO { get; set; }
        [DataMember]
        public Int32 PNUMERO_CUOTAS { get; set; }
        [DataMember]
        public string PCOD_LINEA_CREDITO { get; set; }
        [DataMember]
        public Int32 PCOD_PERIODICIDAD { get; set; }
        [DataMember]
        public Int32 PCOD_OFICINA { get; set; }
        [DataMember]
        public string PFORMA_PAGO { get; set; }
        [DataMember]
        public DateTime PFECHA_PRIMERPAGO { get; set; }
        [DataMember]
        public Int64 PCOD_ASESOR { get; set; }
        [DataMember]
        public Int64 PUSUARIO { get; set; }
        [DataMember]
        public Int64 PCOD_EMPRESA { get; set; }
        [DataMember]
        public string universidad { get; set; }
        [DataMember]
        public string semestre { get; set; }
    }
}
