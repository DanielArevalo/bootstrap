using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Riesgo.Entities
{

    [DataContract]
    [Serializable()]

    public class ActividadEco
    {
        [DataMember]
        public Int64 Id_actividad { get; set; }
        [DataMember]
        public string Cod_actividad { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string valoracion { get; set; }
        [DataMember]
        public Int64 cod_usua { get; set; }

    }
}
