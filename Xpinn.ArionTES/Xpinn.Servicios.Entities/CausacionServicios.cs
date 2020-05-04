using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Servicios.Entities
{
    [DataContract]
    [Serializable]
    public class CausacionServicios
    {
        [DataMember]
        public Int64 idcausacion { get; set; }
        [DataMember]
        public DateTime fecha_causacion { get; set; }
        [DataMember]
        public int numero_servicio { get; set; }
        [DataMember]
        public decimal valor_causado { get; set; }
        [DataMember]
        public int? codusuario { get; set; }
        [DataMember]
        public Int64? cod_ope { get; set; }
    }

}
