using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class Par_Cue_Obligacion
    {
        [DataMember]
        public int idparametro { get; set; }
        [DataMember]
        public int codlineaobligacion { get; set; }
        [DataMember]
        public int codcomponente { get; set; }
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
        public int codentidad { get; set; }

        //agregado 
        [DataMember]
        public string nomestructura { get; set; }
        [DataMember]
        public string nomCuenta { get; set; }
        [DataMember]
        public string nom_tipo_mov { get; set; }
        [DataMember]
        public string nombrelinea { get; set; }
        [DataMember]
        public string nombrebanco { get; set; }
        [DataMember]
        public string nombrecomponente { get; set; }
               
    }    
}
