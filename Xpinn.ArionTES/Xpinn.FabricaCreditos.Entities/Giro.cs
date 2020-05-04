using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class Giro
    {
        [DataMember]
        public int idgiro { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public int tipo_acto { get; set; }
        [DataMember]
        public int forma_pago { get; set; }
        [DataMember]
        public DateTime fec_reg { get; set; }
        [DataMember]
        public DateTime fec_giro { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public Int64? cod_ope { get; set; }
        [DataMember]
        public int num_comp { get; set; }
        [DataMember]
        public int tipo_comp { get; set; }
        [DataMember]
        public string usu_gen { get; set; }
        [DataMember]
        public string usu_apli { get; set; }
        [DataMember]
        public int estadogi { get; set; }
        [DataMember]
        public string usu_apro { get; set; }
        [DataMember]
        public Int64 idctabancaria { get; set; }
        [DataMember]
        public int cod_banco { get; set; }
        [DataMember]
        public string num_cuenta { get; set; }
        [DataMember]
        public int tipo_cuenta { get; set; }
        [DataMember]
        public DateTime fec_apro { get; set; }
        [DataMember]
        public int cob_comision { get; set; }
        [DataMember]
        public Int64 valor { get; set; }
        [DataMember]
        public int codpagofac { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
    }


}