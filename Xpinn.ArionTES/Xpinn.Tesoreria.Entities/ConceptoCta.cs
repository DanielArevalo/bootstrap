using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class ConceptoCta
    {
        [DataMember]
        public int? cod_concepto_fac { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string cod_cuenta_niif { get; set; }

        [DataMember]
        public string cod_cuenta_anticipos { get; set; }
        [DataMember]
        public int? tipo_mov { get; set; }
        [DataMember]
        public string nom_tipo_mov { get; set; }
        [DataMember]
        public List<Concepto_CuentasXpagarImp> lstImpuesto { get; set; }
        [DataMember]
        public string cod_cuenta_desc { get; set; }
    }
}