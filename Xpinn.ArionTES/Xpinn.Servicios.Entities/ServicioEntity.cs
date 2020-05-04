using System;

namespace Xpinn.Servicios.Entities
{
    public class ServicioEntity
    {
        public bool esCorrecto { get; set; }
        public string mensaje { get; set; }
        public int numero_servicio { get; set; }
        public Int64 cod_ope { get; set; }
        public Int64 num_comp { get; set; }
        public Int64 tipo_comp { get; set; }
    }
}
