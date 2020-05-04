using System;

namespace Xpinn.Sincronizacion.Entities
{
    public class SyncAcceso
    {
        public Int64 codacceso { get; set; }
        public Int64 codperfil { get; set; }
        public Int64 cod_opcion { get; set; }
        public int insertar { get; set; }
        public int modificar { get; set; }
        public int borrar { get; set; }
        public int consultar { get; set; }
    }
}
