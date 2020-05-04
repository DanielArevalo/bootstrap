using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Icetex.Entities
{
    [DataContract]
    [Serializable]
    public class CreditoIcetexDocumento
    {
        [DataMember]
        public int cod_credoc { get; set; }
        [DataMember]
        public Int64 numero_credito { get; set; }
        [DataMember]
        public int cod_tipo_doc { get; set; }
        [DataMember]
        public string pregunta { get; set; }
        [DataMember]
        public string respuesta { get; set; }
        [DataMember]
        public byte[] imagen { get; set; }
        [DataMember]
        public string observacion { get; set; }
    }
}
