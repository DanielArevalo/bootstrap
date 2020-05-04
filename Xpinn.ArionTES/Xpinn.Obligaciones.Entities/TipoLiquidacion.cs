using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Obligaciones.Entities
{
    /// <summary>
    /// Entidad Tipo Liquidacion
    /// </summary>
    [DataContract]
    [Serializable]
    public class TipoLiquidacion
    {
        [DataMember]
        public Int64 codtipoliquidacion { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public Int64 tipocuota { get; set; }
        [DataMember]
        public string nomtipocuota { get; set; }
        [DataMember]
        public Int64 tipoamortizacion { get; set; }
        [DataMember]
        public Int64 tipointeres { get; set; }
        [DataMember]
        public Int64 tipopago { get; set; }
        [DataMember]
        public string nomtipopago { get; set; }
        [DataMember]
        public Int64 cobrointeresajuste { get; set; }
        [DataMember]
        public Int64 tipocuotasextras { get; set; }
        [DataMember]
        public Int64 tipointeresextras { get; set; }
        [DataMember]
        public Int64 tipopagosextras { get; set; }
        [DataMember]
        public Int64 conteo { get; set; } 
        
    }
}
