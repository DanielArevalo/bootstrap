using System;

namespace Xpinn.Sincronizacion.Entities
{
    public class SyncHomologaOperacion
    {
        public Int64 consecutivo { get; set; }
        public DateTime? fecha { get; set; }
        public string tabla { get; set; }
        public string campo_tabla { get; set; }
        public string codigo_principal { get; set; }
        public string codigo_local { get; set; }
        public string proceso { get; set; }
        public Int64 num_comp { get; set; }
        public Int64 tipo_comp { get; set; }
    }
}
