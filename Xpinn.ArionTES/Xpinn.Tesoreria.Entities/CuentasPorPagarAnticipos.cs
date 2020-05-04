using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class CUENTAXPAGAR_ANTICIPO
    {
        [DataMember]
        public Int64 idanticipo { get; set; }
        [DataMember]
        public int codigo_factura { get; set; }
        [DataMember]
        public DateTime fecha_anticipo { get; set; }
        [DataMember]
        public DateTime? fecha_aprobacion { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public decimal? saldo { get; set; }
        [DataMember]
        public int estado { get; set; }
        [DataMember]
        public DateTime fecha_factura { get; set; }
        [DataMember]
        public int idgiro { get; set; }
        [DataMember]
        public long cod_persona { get; set; }
        [DataMember]
        public int forma_pago { get; set; }
        [DataMember]
        public int tipo_acto { get; set; }
        [DataMember]
        public DateTime fec_reg { get; set; }
        [DataMember]
        public DateTime fec_giro { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public string usu_gen { get; set; }
        [DataMember]
        public int estadogi { get; set; }
        [DataMember]
        public string usu_apli { get; set; }
        [DataMember]
        public string usu_apro { get; set; }
        [DataMember]
        public long idctabancaria { get; set; }
        [DataMember]
        public int cod_banco { get; set; }
        [DataMember]
        public int tipo_cuenta { get; set; }
        [DataMember]
        public string num_cuenta { get; set; }
        [DataMember]
        public DateTime fec_apro { get; set; }
        [DataMember]
        public int cob_comision { get; set; }
        [DataMember]
        public long cod_ope { get; set; }

        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public Int64 cod_persona_deta { get; set; }
    }
}
