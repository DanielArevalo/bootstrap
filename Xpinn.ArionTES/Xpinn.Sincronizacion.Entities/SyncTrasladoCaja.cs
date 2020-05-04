using System;

namespace Xpinn.Sincronizacion.Entities
{
    public class SyncTrasladoCaja
    {
        public Int64 cod_traslado { get; set; }
        public Int64? cod_ope_traslado { get; set; }
        public Int64? cod_ope_recepcion { get; set; }
        public decimal tipo_traslado { get; set; }
        public DateTime fecha_traslado { get; set; }
        public DateTime? fecha_recibe { get; set; }
        public Int64 cod_caja_ori { get; set; }
        public Int64? cod_cajero_ori { get; set; }
        public Int64? cod_caja_des { get; set; }
        public Int64? cod_cajero_des { get; set; }
        public int cod_moneda { get; set; }
        public decimal valor { get; set; }
        public int estado { get; set; }
        public Int64 cod_ope { get; set; }
    }
}
