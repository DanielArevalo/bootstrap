using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Caja.Entities
{
    [DataContract]
    [Serializable]
    public class GiroMoneda
    {
        [DataMember]
        public Int64 idgiromoneda { get; set; }
        [DataMember]
        public Int64? cod_ope { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public int? cod_moneda { get; set; }
        [DataMember]
        public int? cod_oficina_recibe { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string observaciones { get; set; }
        [DataMember]
        public int? estado { get; set; }
        [DataMember]
        public int? cod_usuario { get; set; }
        [DataMember]
        public int? cod_usuario_entrega { get; set; }
        [DataMember]
        public string nom_moneda { get; set; }
    }
}
