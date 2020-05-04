using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Servicios.Entities
{
    [DataContract]
    [Serializable]
    public class Destinacion
    {
        [DataMember]
        public int cod_destino { get; set; }
        [DataMember]
        public string descripcion { get; set; }

    }

    [DataContract]
    [Serializable]
    public class LineaServ_Destinacion
    {
        [DataMember]
        public int cod_linea_servicio { get; set; }
        [DataMember]
        public int cod_destino { get; set; }
        [DataMember]
        public string descripcion { get; set; }
    }


}