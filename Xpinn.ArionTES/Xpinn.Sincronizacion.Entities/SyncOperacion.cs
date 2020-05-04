using System;

namespace Xpinn.Sincronizacion.Entities
{
    public class SyncOperacion
    {
        public Int64 cod_ope { get; set; }
        public Int64 tipo_ope { get; set; }
        public Int64 cod_usuario { get; set; }
        public Int64 cod_oficina { get; set; }
        public Int64? cod_caja { get; set; }
        public Int64? cod_cajero { get; set; }
        public int? num_recibo { get; set; }
        public DateTime fecha_real { get; set; }
        public DateTime hora { get; set; }
        public DateTime? fecha_oper { get; set; }
        public DateTime fecha_calc { get; set; }
        public Int64? num_comp { get; set; }
        public int? tipo_comp { get; set; }
        public int? estado { get; set; }
        public int? num_lista { get; set; }
        public string observacion { get; set; }
        public Int64? cod_proceso { get; set; }
        public string ip { get; set; }

        public string strMovimientos { get; set; }
        public string strTransacciones { get; set; }
        public string strConsignacion { get; set; }
        public string strConsignacionCheque { get; set; }
        public string strTraslado { get; set; }
        public Int64 cod_ope_principal { get; set; }
    }
}
