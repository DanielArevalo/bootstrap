using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class EmpresaEstructuraCarga
    {
        [DataMember]
        public Int64? codemparchivo { get; set; }
        [DataMember]
        public Int64? cod_empresa { get; set; }
        [DataMember]
        public Int64? cod_estructura_carga { get; set; }
        [DataMember]
        public int? tipo { get; set; }
    }
}