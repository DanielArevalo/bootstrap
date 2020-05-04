using System;
using System.Runtime.Serialization;

namespace Xpinn.Sincronizacion.Entities
{
    [DataContract]
    [Serializable]
    public class Producto
    {
        [DataMember]
        public Int64 idproducto { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public int? cod_tipo_producto { get; set; }
        [DataMember]
        public string numero_producto { get; set; }
        [DataMember]
        public string cod_linea_producto { get; set; }
        [DataMember]
        public string nombre_linea { get; set; }
        [DataMember]
        public decimal? monto_aprobado { get; set; }
        [DataMember]
        public DateTime? fecha_aprobacion { get; set; }
        [DataMember]
        public decimal? valor_cuota { get; set; }
        [DataMember]
        public decimal? saldo_capital { get; set; }
        [DataMember]
        public DateTime? fecha_proximo_pago { get; set; }
        [DataMember]
        public int? dias_mora { get; set; }
        [DataMember]
        public decimal? valor_pago { get; set; }
        [DataMember]
        public decimal? total_pago { get; set; }
        [DataMember]
        public int tipo_linea { get; set; }
        [DataMember]
        public string estado { get; set; }
    }
}
