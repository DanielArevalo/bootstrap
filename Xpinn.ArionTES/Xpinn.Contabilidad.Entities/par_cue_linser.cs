using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public  class par_cue_linser
    {
        [DataMember]
        public Int64 idparametro { get; set; }
        [DataMember]
        public string cod_linea_servicio { get; set; }
        [DataMember]
        public int cod_atr { get; set; }
        [DataMember]
        public int cod_est_det { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public int? tipo_mov { get; set; }
        [DataMember]
        public int? tipo { get; set; }
        [DataMember]
        public int? tipo_tran { get; set; }

       ///  agregados////
       ///  
        [DataMember]
        public string NombreLinea { get; set; }
        [DataMember]
        public string NombreCuenta { get; set; }
        [DataMember]
        public string tipo_tranN { get; set; }

    }
}
