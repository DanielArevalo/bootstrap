using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class Oficina_ciudad
    {
        [DataMember]
        public Int64 idoficiudad { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public Int64 codciudad { get; set; }

        // agregados 

        public String Nombre_Ciudad { get; set; }
        public String Nombre_Oficina { get; set; }
    }
}
