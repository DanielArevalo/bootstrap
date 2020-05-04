using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class Par_Cue_LinCred
    {
        [DataMember]
        public Int64 idparametro { get; set; }
        [DataMember]
        public string cod_linea_credito { get; set; }
        [DataMember]
        public int cod_atr { get; set; }
        [DataMember]
        public string nom_atr { get; set; }
        [DataMember]
        public int tipo_cuenta { get; set; }
        [DataMember]
        public string cod_categoria { get; set; }
        [DataMember]
        public int? libranza { get; set; }
        [DataMember]
        public string nom_libranza { get; set; }
        [DataMember]
        public int? garantia { get; set; }
        [DataMember]
        public string nom_garantia { get; set; }
        [DataMember]
        public int? cod_est_det { get; set; }
        [DataMember]
        public string nom_est_det { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public int tipo_mov { get; set; }
        [DataMember]
        public int? tipo { get; set; }
        [DataMember]
        public Int64? tipo_tran { get; set; }
        [DataMember]
        public string nom_tipo_tran { get; set; }
        [DataMember]
        public string cod_cuenta_niif { get; set; }
        [DataMember]
        public string nom_linea_credito { get; set; }
    }
}