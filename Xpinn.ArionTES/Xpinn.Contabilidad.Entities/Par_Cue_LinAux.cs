using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class Par_Cue_LinAux
    {
        [DataMember]
        public Int64 idparametro { get; set; }
        [DataMember]
        public string cod_linea_auxilio { get; set; }
        [DataMember]
        public string nom_linea_auxilio { get; set; }
        [DataMember]
        public int cod_atr { get; set; }
        [DataMember]
        public string nom_atr { get; set; }
        [DataMember]
        public int? cod_est_det { get; set; }
        [DataMember]
        public string nomestructura { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string nom_cuenta { get; set; }
        [DataMember]
        public int? tipo_mov { get; set; }
        [DataMember]
        public string nom_tipo_mov { get; set; }
        [DataMember]
        public int? tipo { get; set; }
        [DataMember]
        public int? tipo_tran { get; set; }
        [DataMember]
        public string nom_tipo_tran { get; set; }
    }
}