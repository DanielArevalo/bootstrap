using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class FormatoDocumento
    {
        [DataMember]
        public Int64 cod_documento { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public string texto { get; set; }
        [DataMember]
        public string nombre_pl { get; set; }
        [DataMember]
        public string nomtipo { get; set; }
        [DataMember]
        public byte[] Textos { get; set; }
    }
}
