using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Cartera.Entities
{
    [DataContract]
    [Serializable]
    public class Reliquidacion
    {
        [DataMember]
        public Int64 idreliquida { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public Int64 cod_deudor { get; set; }
        [DataMember]
        public DateTime fecha_reliquida { get; set; }
        [DataMember]
        public int plazo_rel { get; set; }
        [DataMember]
        public DateTime fecha_prox_pago_rel { get; set; }
        [DataMember]
        public Decimal cuota_rel { get; set; }
        [DataMember]
        public Int64 cod_periodicidad_rel { get; set; }
        [DataMember]
        public Int64 cod_usuario { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public List<Xpinn.FabricaCreditos.Entities.CuotasExtras> lstCuotasExtras { get; set; }
    }

}