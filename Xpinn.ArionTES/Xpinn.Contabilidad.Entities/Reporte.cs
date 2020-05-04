using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class Reporte
    {
       
       
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string Nombres { get; set; }
        [DataMember]
        public string Apellidos { get; set; }
        [DataMember]
        public Int64 valor { get; set; }
        [DataMember]
        public string Direccion { get; set; }
    }
}

