using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities.Common
{
    [DataContract]
    [Serializable]
    public class Zona
    {
        [DataMember]
        public Int64 IdZona { get; set; } //Corresponde al campo ICODZONA
        [DataMember]
        public string CodigoCiudad { get; set; } //Corresponde al campo ICODCIUDAD
        [DataMember]
        public string NombreZona { get; set; } // Corresponde al campo SZONA
         
    }
}
