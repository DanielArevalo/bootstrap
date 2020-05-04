using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class CuentasXpagarImpuesto
    {
        [DataMember]
        public int coddetalleimp { get; set; }
        [DataMember]
        public int coddetallefac { get; set; }
        [DataMember]
        public int codigo_factura { get; set; }
        [DataMember]
        public int cod_tipo_impuesto { get; set; }
        [DataMember]
        public decimal? porcentaje { get; set; }
        [DataMember]
        public decimal? base_vr { get; set; }
        [DataMember]
        public decimal? valor { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }

        [DataMember]
        public string cod_cuenta_imp { get; set; }

        [DataMember]
        public string naturaleza { get; set; }
        
        //ADICIONADO
        [DataMember]
        public string nom_tipo_impuesto { get; set; }
        [DataMember]
        public decimal? porcentaje_impuesto { get; set; }

        [DataMember]
        public int idcuentadetalleimp { get; set; }
    }
}