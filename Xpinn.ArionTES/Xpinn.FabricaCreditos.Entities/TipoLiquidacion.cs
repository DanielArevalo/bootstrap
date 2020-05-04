using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class TipoLiquidacion
    {
        [DataMember]
        public Int64 tipo_liquidacion { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int tipo_cuota { get; set; }         // 1: Pago Unico, 2: Serie Uniforme, 3: Gradiente
        [DataMember]
        public string nomtipo_cuota { get; set; }
        [DataMember]
        public int tipo_pago { get; set; }          // 1: Anticipado,  2: Vencido
        [DataMember]
        public string nomtipo_pago { get; set; }
        [DataMember]
        public int tipo_interes { get; set; }       // 1: Simple,  2: Compuesto
        [DataMember]
        public string nomtipo_interes { get; set; }
        [DataMember]
        public int tipo_intant { get; set; }
        [DataMember]
        public string nomtipo_intant { get; set; }
        [DataMember]
        public decimal valor_gradiente { get; set; }
        [DataMember]
        public int tip_gra { get; set; }
        [DataMember]
        public string nomtip_gra { get; set; }
        [DataMember]
        public int tip_amo { get; set; }            
        [DataMember]
        public string nomtip_amo { get; set; }
        [DataMember]
        public int cob_intant_des { get; set; }
    }
}