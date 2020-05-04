using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class TipoContrato
    {
        [DataMember]
        public Int64 cod_contratacion { get; set; }
        [DataMember]
        public Int64 cod_contrato { get; set; }
        [DataMember]
        public string tipo_contrato { get; set; }
        [DataMember]
        public string dia_habil { get; set; }
        [DataMember]
        public long cod_tipo_contrato { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public long cod_garantia { get; set; }

        [DataMember]
        public Int64 consecutivo { get; set; }
        [DataMember]
        public string descripciontiporetiro { get; set; }
    }
}