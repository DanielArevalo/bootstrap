using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class TipoImpuesto
    {
        [DataMember]
        public int? cod_tipo_impuesto { get; set; }
        [DataMember]
        public string nombre_impuesto { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int principal { get; set; }
        [DataMember]
        public int depende_de{ get; set; }
    }
}