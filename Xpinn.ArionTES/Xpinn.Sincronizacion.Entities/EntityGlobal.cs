using System;

namespace Xpinn.Sincronizacion.Entities
{
    public class EntityGlobal
    {
        public bool Success { get; set; }
        public string Filter { get; set; }
        public int NroRegisterAffected { get; set; }
        public string Message { get; set; }
        public int NroRegisterAdd { get; set; }
        public int NroRegisterModify { get; set; }
        public string CodigoGenerado { get; set; }
        public DateTime fechaGenerica { get; set; }
        public Int64 num_comp { get; set; }
        public Int64 tipo_comp { get; set; }
    }
}
