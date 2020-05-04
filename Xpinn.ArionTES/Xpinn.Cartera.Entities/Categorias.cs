using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Cartera.Entities
{
    [DataContract]
    [Serializable]
    public class Categorias
    {
        [DataMember]
        public string cod_categoria { get; set; }
        [DataMember]
        public string descripcion { get; set; }
    }
}