using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class Par_Cue_LinApo
    {
        [DataMember]
        public Int64 idparametro { get; set; }
        [DataMember]
        public int cod_linea_aporte { get; set; }
        [DataMember]
        public int cod_atr { get; set; }
        [DataMember]
        public int tipo_cuenta { get; set; }
        [DataMember]
        public int cod_est_det { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public int tipo_mov { get; set; }
        [DataMember]
        public int tipo { get; set; }
        [DataMember]
        public int tipo_tran { get; set; }
        [DataMember]
        public string cod_cuenta_niif { get; set; }

        //agregado 
        [DataMember]
        public string nombrelinea { get; set; }
        [DataMember]
        public string nomestructura { get; set; }
        [DataMember]
        public string nomCuenta { get; set; }
        [DataMember]
        public string nomCuentaNiif { get; set; }
        [DataMember]
        public string nom_tipo_mov { get; set; }
        [DataMember]
        public string nomtipo_tran { get; set; }

    }    
}
