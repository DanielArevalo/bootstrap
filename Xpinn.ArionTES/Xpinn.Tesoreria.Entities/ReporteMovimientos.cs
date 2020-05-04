using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class ReporteMovimientos
    {
        [DataMember]
        public string nom_oficina { get; set; }
        [DataMember]
        public string nom_caja { get; set; }
        [DataMember]
        public string nom_moneda { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public int cod_ofi { get; set; }
        [DataMember]
        public int cod_cajero { get; set; }
        [DataMember]
        public int cod_tipoope { get; set; }
        [DataMember]
        public int num_comp { get; set; }
        [DataMember]
        public int tipo_comp { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }        
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }        
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public string usuario { get; set; }
        [DataMember]
        public string iden_usuario { get; set; }
        [DataMember]
        public string nomtipo_comp { get; set; }
        [DataMember]
        public string nomTipo_Ope { get; set; }
        [DataMember]
        public string nomTipo_Pago { get; set; }
        [DataMember]
        public string forma_pago { get; set; }
        [DataMember]
        public Int64 cod_tipo_pago{ get; set; }
        [DataMember]
        public string num_documento { get; set; }
        [DataMember]
        public int estado { get; set; }
        [DataMember]
        public string nom_estado { get; set; }
    }
}
