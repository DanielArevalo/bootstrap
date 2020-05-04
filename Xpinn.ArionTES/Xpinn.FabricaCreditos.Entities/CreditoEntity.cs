using System;

namespace Xpinn.FabricaCreditos.Entities
{
    public class CreditoEntity
    {
        public bool esCorrecto { get; set; }
        public string mensaje { get; set; }
        public Int64 numero_radicacion { get; set; }
        public Int64 cod_ope { get; set; }
        public Int64 num_comp { get; set; }
        public Int64 tipo_comp { get; set; }
    }
}
