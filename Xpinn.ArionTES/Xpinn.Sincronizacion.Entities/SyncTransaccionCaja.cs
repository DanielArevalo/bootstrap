using System;

namespace Xpinn.Sincronizacion.Entities
{
    public class SyncTransaccionCaja
    {
        public Int64 num_trancaja { get; set; }
        public Int64 cod_ope { get; set; }
        public Int64 cod_caja { get; set; }
        public Int64? cod_cajero { get; set; }
        public DateTime? fecha_movimiento { get; set; }
        public DateTime fecha_aplica { get; set; }
        public string tipo_movimiento { get; set; }
        public Int64 cod_persona { get; set; }
        public string num_producto { get; set; }
        public string tipo_pago { get; set; }
        public int cod_moneda { get; set; }
        public decimal valor_pago { get; set; }
        public int? estado { get; set; }
        public Int64? tipo_tran { get; set; }
        public decimal? tasa_cambio { get; set; }
        public Int64? codcontable { get; set; }
        public Int64 cod_usuario { get; set; }
        public string referencia { get; set; }
        public Int64 tipo_motivo { get; set; }
    }
}
