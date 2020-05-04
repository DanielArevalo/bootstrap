using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class Parametrizar
    {
        [DataMember]
        public Int64 idp { get; set; }
        [DataMember]
        public Int64 minimo { get; set; }
        [DataMember]
        public Int64 maximo { get; set; }
        [DataMember]
        public string aprueba { get; set; }
        [DataMember]
        public string muestra { get; set; }
        [DataMember]
        public string mensaje { get; set; }
        [DataMember]
        public string UsuarioCrea { get; set; }
        [DataMember]
        public DateTime FechaCrea { get; set; }
        [DataMember]
        public string UsuarioEdita { get; set; }
        [DataMember]
        public DateTime FechaEdita { get; set; }


        [DataMember]
        public Int64 idc { get; set; }
        [DataMember]
        public string central { get; set; }
        [DataMember]
        public Int64 valor { get; set; }
        [DataMember]
        public string cobra { get; set; }
        [DataMember]
        public Int64 porcentaje { get; set; }
        [DataMember]
        public Double valoriva { get; set; }
    }
        
    
}