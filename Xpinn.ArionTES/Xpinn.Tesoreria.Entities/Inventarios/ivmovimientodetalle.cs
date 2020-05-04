using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class ivmovimientodetalle
    {
        [DataMember]
        public Int64 id_det_mov { get; set; }
        [DataMember]
        public Int64 id_movimiento { get; set; }
        [DataMember]
        public Int64 id_tipo_movimiento { get; set; }
        [DataMember]
        public string id_producto { get; set; }
        [DataMember]
        public double cantidad { get; set; }
        [DataMember]
        public decimal precio_unitario { get; set; }
        [DataMember]
        public decimal precio_total{ get; set; }
        [DataMember]
        public Int64? num_comp { get; set; }
        [DataMember]
        public Int64? tipo_comp { get; set; }
        [DataMember]
        public Int64 codigo { get; set; }
        [DataMember]
        public Int64 id_impuesto { get; set; }
        [DataMember]
        public decimal impuesto { get; set; }
        [DataMember]
        public decimal valor_impuesto { get; set; }
        [DataMember]
        public Int64? id_categoria { get; set; }
        [DataMember]
        public Int64? tercero { get; set; }
        [DataMember]
        public decimal costo_promedio { get; set; }
        [DataMember]
        public string factura { get; set; }
    }
}
