using System;

namespace Xpinn.Sincronizacion.Entities
{
    public class SyncSaldoCaja
    {
        public Int64 cod_caja { get; set; }
        public Int64 cod_cajero { get; set; }
        public DateTime fecha { get; set; }
        public int cod_moneda { get; set; }
        public decimal? valor { get; set; }
    }
}
