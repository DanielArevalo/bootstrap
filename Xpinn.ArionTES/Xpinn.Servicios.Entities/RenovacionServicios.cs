using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Servicios.Entities
{
    [DataContract]
    [Serializable]
    public class RenovacionServicios
    {
        [DataMember]
        public int idrenovacion { get; set; }
        [DataMember]
        public Int64 numero_servicio { get; set; }
        [DataMember]
        public DateTime? fecha_renovacion { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public DateTime? fecha_inicial_vigencia { get; set; }
        [DataMember]
        public DateTime? fecha_final_vigencia { get; set; }
        [DataMember]
        public decimal? valor_total { get; set; }
        [DataMember]
        public decimal? valor_cuota { get; set; }
        [DataMember]
        public int? plazo { get; set; }
        [DataMember]
        public int? cod_usuario { get; set; }
        [DataMember]
        public int tipo { get; set; }        
    }

}
