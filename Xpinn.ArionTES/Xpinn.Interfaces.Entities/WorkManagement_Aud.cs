using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Xpinn.Interfaces.Entities
{
    [DataContract]
    [Serializable]
    public class WorkManagement_Aud
    {
        public int? consecutivo { get; set; }
        public int? exitoso { get; set; }
        public int? tipooperacion { get; set; }
        public string jsonEntidadPeticion { get; set; }
        public string jsonEntidadRespuesta { get; set; }
    }

}
