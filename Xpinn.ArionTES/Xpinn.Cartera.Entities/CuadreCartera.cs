using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Xpinn.Util;

namespace Xpinn.Cartera.Entities
{
    /// <summary>
    /// Entidad CuadreCartera
    /// </summary>
    [DataContract]
    [Serializable]
    public class CuadreCartera
    {
        [DataMember]
        public DateTime fecha_inicial { get; set; }
        [DataMember]
        public DateTime fecha_final { get; set; }
        [DataMember]
        public Int64 cod_atr { get; set; }
        [DataMember]
        public string nom_atr { get; set; }
        [DataMember]
        public Int64 num_comp { get; set; }
        [DataMember]
        public Int64 tipo_comp { get; set; }
        [DataMember]
        public string nom_tipo_comp { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public Decimal valor_contable { get; set; }
        [DataMember]
        public Decimal valor_operativo { get; set; }
        [DataMember]
        public Decimal diferencia { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public Int64 tipo_producto { get; set; }
        [DataMember]
        public long cod_ope { get; set; }
        [DataMember]
        public string nom_linea { get; set; }
        [DataMember]
        public string nom_tipo_tran { get; set; }
        [DataMember]
        public string tipo_mov { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string detalle { get; set; }
        [DataMember]
        public int? cod_centro_costo { get; set; }
        [DataMember]
        public int? cod_centro_gestion { get; set; }
        [DataMember]
        public string nom_moneda { get; set; }
        [DataMember]
        public int? cod_tipo_producto { get; set; }
        [DataMember]
        public string numero_transaccion { get; set; }
        [DataMember]
        public int conciliado { get; set; }
        [DataMember]
        public TipoDeProducto tipo_producto_enum { get; set; }
        [DataMember]
        public string tipo_mov_operativo { get; set; }
        
    }
    
}