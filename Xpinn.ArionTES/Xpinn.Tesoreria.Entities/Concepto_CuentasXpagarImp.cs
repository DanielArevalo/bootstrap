using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class Concepto_CuentasXpagarImp
    {
      

           [DataMember]
        public int idconceptoimp { get; set; }
        [DataMember]
        public int? cod_concepto_fac { get; set; }
        [DataMember]
        public int? cod_tipo_impuesto { get; set; }
        [DataMember]
        public decimal? porcentaje_impuesto { get; set; }
        [DataMember]
        public decimal? base_minima { get; set; }
        [DataMember]
        public string cod_cuenta_imp { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string naturaleza { get; set; }
        
        //AGREGADO
        [DataMember]
        public string nom_tipo_impuesto { get; set; }
        [DataMember]
        public int coddetalleimp { get; set; }
        [DataMember]
        public int idimpuesto { get; set; }
        [DataMember]
        public int asumido { get; set; }
        [DataMember]
        public string cod_cuenta_asumido { get; set; }
        [DataMember]
        public List<Concepto_CuentasXpagarImp> lstPorcentaje { get; set; }

        [DataMember]
        public int cod_factura { get; set; }
        [DataMember]
        public int CodDetalleFac { get; set; }
    }
}