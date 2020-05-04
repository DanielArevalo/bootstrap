using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;


namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class ConvenioRecaudo
    {
        [DataMember]
        public Int64? cod_convenio { get; set; }
        [DataMember]
        public string nombre_convenio { get; set; }
        [DataMember]
        public DateTime? fecha_convenio { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public Int64? tipo_producto { get; set; }
        [DataMember]
        public string nom_tipo_producto { get; set; }
        [DataMember]
        public string num_producto { get; set; }
        [DataMember]
        public Int64? tipo_tran { get; set; }
        [DataMember]
        public int cuenta_propia { get; set; }
        [DataMember]
        public int contrato_firmado { get; set; }
        [DataMember]
        public int naturaleza_convenio { get; set; }

        [DataMember]
        public String EAN { get; set; }
    }
}
