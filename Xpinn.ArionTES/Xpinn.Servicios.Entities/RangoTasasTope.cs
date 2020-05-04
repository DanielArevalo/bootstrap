using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Servicios.Entities
{
    [DataContract]
    [Serializable]
    public class RangoTasasTope
    {
        [DataMember]
        public int codtope { get; set; }
        [DataMember]
        public int codrango { get; set; }
        [DataMember]
        public int tipo_tope { get; set; }
        [DataMember]
        public string minimo { get; set; }
        [DataMember]
        public string maximo { get; set; }
        [DataMember]
        public string cod_linea_servicio { get; set; }
    }
}
