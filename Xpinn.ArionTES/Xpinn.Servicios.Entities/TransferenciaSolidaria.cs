using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Xpinn.Servicios.Entities
{
    [DataContract]
    [Serializable]
    public class TransferenciaSolidaria
    {
        [DataMember]
        public int num_transferencia { get; set; }
        [DataMember]
        public int cod_persona { get; set; }
        [DataMember]
        public string num_producto { get; set; }
        [DataMember]
        public int cod_linea_producto { get; set; }
        [DataMember]
        public int tipo_producto { get; set; }
        [DataMember]
        public int? cod_destinacion { get; set; }
        [DataMember]
        public DateTime fecha_transferencia { get; set; }
        [DataMember]
        public decimal monto { get; set; }
        [DataMember]
        public decimal valor_compra { get; set; }
        [DataMember]
        public decimal beneficio { get; set; }
        [DataMember]
        public decimal valor_mercado { get; set; }
        [DataMember]
        public Int64? cod_ope { get; set; }
        [DataMember]
        public int? cod_tipo_producto { get; set; }
        [DataMember]
        public int es_bono_contribucion { get; set; }

    }
}
