using System;
using System.Runtime.Serialization;

namespace Xpinn.Sincronizacion.Entities
{
    public class SyncConsignacion
    {
        public Int64 cod_consignacion { get; set; }
        public Int64? cod_ope { get; set; }
        public Int64? cod_caja { get; set; }
        public Int64? cod_cajero { get; set; }
        public DateTime fecha_consignacion { get; set; }
        public Int64 cod_banco { get; set; }
        public int? cod_moneda { get; set; }
        public decimal? valor_efectivo { get; set; }
        public decimal? valor_cheque { get; set; }
        public string observaciones { get; set; }
        public DateTime? fecha_salida { get; set; }
        public string num_cuenta { get; set; }
        public int estado { get; set; }
    }
}
