using System;

namespace Xpinn.Sincronizacion.Entities
{
    public class SyncMovimientoCaja
    {
        public Int64 cod_movimiento { get; set; }
        public Int64 cod_ope { get; set; }
        public DateTime fec_ope { get; set; }
        public Int64? cod_caja { get; set; }
        public Int64? cod_cajero { get; set; }
        public string tipo_mov { get; set; }
        public int cod_tipo_pago { get; set; }
        public Int64? cod_banco { get; set; }
        public string num_documento { get; set; }
        public string tipo_documento { get; set; }
        public int? cod_moneda { get; set; }
        public decimal? valor { get; set; }
        public int? estado { get; set; }
        public Int64? cod_persona { get; set; }
        public int? idctabancaria { get; set; }
    }
}
