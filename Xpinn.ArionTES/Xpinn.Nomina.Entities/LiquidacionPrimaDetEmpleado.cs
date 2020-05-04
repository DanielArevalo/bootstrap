using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class LiquidacionPrimaDetEmpleado
    {
        [DataMember]
        public long consecutivo { get; set; }
        [DataMember]
        public long codigoliquidacionprimadetalle { get; set; }
        [DataMember]
        public long codigoempleado { get; set; }
        [DataMember]
        public long codigotiponovedad { get; set; }
        [DataMember]
        public int esnovedadcreadamanual { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public string descripcionNovedad { get; set; }
        [DataMember]
        public int tipoCalculoNovedad { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
    }
}