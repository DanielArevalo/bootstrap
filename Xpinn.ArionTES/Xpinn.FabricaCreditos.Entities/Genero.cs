using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class Genero
    {
        [DataMember]
        public String sexo { get; set; }
      
       
    }
}
