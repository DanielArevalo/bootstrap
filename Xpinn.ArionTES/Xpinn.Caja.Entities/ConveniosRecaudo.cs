using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Caja.Entities
{
    [DataContract]
    [Serializable]
    public class ConveniosRecaudo
    {
        [DataMember]
        public Int64 cod_convenio { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public DateTime? fecha_convenio { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public int tipo_producto { get; set; }
        [DataMember]
        public string numero_producto { get; set; }
        [DataMember]
        public int? tipo_tran { get; set; }
        [DataMember]
        public int tipo_identificacion { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre_persona { get; set; }
        [DataMember]
        public string nombre_produc { get; set; }
        [DataMember]
        public string nombre_tran { get; set; }
    }
}
