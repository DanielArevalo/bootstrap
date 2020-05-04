using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class Par_Cue_Nomina
    {
        [DataMember]
        public Int64 idparametro { get; set; }
        [DataMember]
        public string cod_concepto { get; set; }
        [DataMember]
        public int? cod_est_det { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public int? tipo_mov { get; set; }
        [DataMember]
        public Int64? tipo_tran { get; set; }
        [DataMember]
        public string cod_cuenta_niif { get; set; }
        [DataMember]
        public Int64? cod_tercero { get; set; }
        [DataMember]
        public int? tipo_tercero { get; set; }
        [DataMember]
        public string cod_cuenta_contra { get; set; }
        [DataMember]
        public string nom_cue_contra { get; set; }
        [DataMember]
        public string nom_tipo_mov { get; set; }
        [DataMember]
        public string nom_tipo_tran { get; set; }
        [DataMember]
        public string nom_cue_local { get; set; }
        [DataMember]
        public string nom_cue_niff { get; set; }
        [DataMember]
        public string nom_est_det { get; set; }
        [DataMember]
        public string nom_concepto { get; set; }
        [DataMember]
        public string identificacion_tercero { get; set; }
        [DataMember]
        public string nom_tercero { get; set; }
        [DataMember]
        public string razon_social { get; set; }
    }
}