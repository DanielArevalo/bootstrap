using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Servicios.Entities
{
    [DataContract]
    [Serializable]
    public class DetalleServicio
    {
        [DataMember]
        public Int64 codserbeneficiario { get; set; }
        [DataMember]
        public int numero_servicio { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public int? codparentesco { get; set; }
        [DataMember]
        public decimal? porcentaje { get; set; }
        [DataMember]
        public string nombre_fallecido { get; set; }
        [DataMember]
        public string descripcion { get; set; }
    }
}
