using System;

namespace Xpinn.Sincronizacion.Entities
{
    public class SyncHomologaOperacionDeta
    {
        public Int64 id_detalle { get; set; }
        public Int64? cod_ope_principal { get; set; }
        public Int64? cod_ope_local { get; set; }
        public string tabla_detalle { get; set; }
        public string campo_tabla { get; set; }
        public string codigo_principal { get; set; }
        public string codigo_local { get; set; }
    }
}
