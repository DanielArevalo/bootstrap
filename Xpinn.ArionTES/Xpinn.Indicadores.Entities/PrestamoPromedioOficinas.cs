using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Indicadores.Entities
{
    [DataContract]
    [Serializable]
    public class PrestamoPromedioOficinas
    {
        [DataMember]
        public string fecha_corte { get; set; }
        [DataMember]
        public string cod_oficina { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public decimal valor_prestamo_promedio { get; set; }
        [DataMember]
        public decimal valor_prestamo_promedio_c { get; set; }
    }
}
