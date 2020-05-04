using System;

namespace Xpinn.Sincronizacion.Entities
{
    public class SyncProcesoOficina
    {
        public Int64 consecutivo { get; set; }
        public Int64? cod_oficina { get; set; }
        public DateTime fecha_proceso { get; set; }
        public int tipo_horario { get; set; }
        public int tipo_proceso { get; set; }
        public Int64? cod_usuario { get; set; }
    }
}
