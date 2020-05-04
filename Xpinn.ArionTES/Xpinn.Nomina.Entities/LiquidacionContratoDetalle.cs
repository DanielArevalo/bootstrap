using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class LiquidacionContratoDetalle
    {
        [DataMember]
        public long consecutivo { get; set; }
        [DataMember]
        public long codigoliquidacioncontrato { get; set; }
        [DataMember]
        public long codigoconceptonomina { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public string desc_conceptoNomina { get; set; }
        [DataMember]
        public long tipoCalculo { get; set; }
        [DataMember]
        public decimal valorPago { get; set; }
        [DataMember]
        public decimal valorDescuento { get; set; }
    }
}