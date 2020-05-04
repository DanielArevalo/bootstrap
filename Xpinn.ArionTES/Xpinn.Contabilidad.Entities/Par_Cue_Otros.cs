using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class Par_Cue_Otros
    {
        [DataMember]
        public int idparametro { get; set; }
        [DataMember]
        public int tipo_tran { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public int tipo_mov { get; set; }
        [DataMember]
        public int cod_est_det { get; set; }
        [DataMember]
        public int tipo_impuesto { get; set; }

        //agregado 
        [DataMember]
        public string nomestructura { get; set; }
        [DataMember]
        public string nomCuenta { get; set; }
        [DataMember]
        public string nom_tipo_mov { get; set; }
        [DataMember]
        public string nomtipo_tran { get; set; }
        [DataMember]
        public string nomimpuesto { get; set; }
        [DataMember]
        public string cod_cuenta_niif { get; set; }
        [DataMember]
        public string nom_cuenta_niif { get; set; }
    }
}
