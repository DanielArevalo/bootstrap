using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class EntregaCheques
    {
        [DataMember]
        public Int64 identrega { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public Int64? num_comp { get; set; }
        [DataMember]
        public int? tipo_comp { get; set; }
        [DataMember]
        public Int64? idgiro { get; set; }
        [DataMember]
        public string num_cheque { get; set; }
        [DataMember]
        public string n_documento { get; set; }
        [DataMember]
        public int entidad { get; set; }
        [DataMember]
        public string nombrebanco { get; set; }
        [DataMember]
        public Int64? cod_benef { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public int concepto { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public Int64? idautorizacion { get; set; }
        [DataMember]
        public DateTime? fecha_entrega { get; set; }
        [DataMember]
        public Int64? cod_usuario { get; set; }
        [DataMember]
        public int? estado_cheque { get; set; }
    }
}