using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Seguridad.Entities
{
    
    [DataContract]
    [Serializable]
    public class RespuestaApp
    {
        [DataMember]
        public string Mensaje { get; set; }
        [DataMember]
        public Boolean rpta { get; set; }
        [DataMember]
        public string valorRpta { get; set; }        
    }
}