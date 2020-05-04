using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class LiquidacionVacacionesDetalleEmpleado
    {
        [DataMember]
        public Int64 consecutivo { get; set; }
        [DataMember]
        public Int64 codigoliquidacionvacacionesemp { get; set; }
        [DataMember]
        public Int64 codigoConcepto { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public string desc_concepto { get; set; }
        [DataMember]
        public int tipoCalculo { get; set; }
        [DataMember]
        public int esConceptoNomina { get; set; }
    }
}