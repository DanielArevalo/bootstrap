using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Servicios.Entities
{
    [DataContract]
    [Serializable]
    public class RangoTasas
    {
        [DataMember]
        public int codrango { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string cod_linea_servicio { get; set; }

        [DataMember]
        public List<RangoTasasTope> lstTopes { get; set; }
    }
}
