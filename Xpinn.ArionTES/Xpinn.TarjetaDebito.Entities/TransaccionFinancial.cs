using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.TarjetaDebito.Entities
{
    [DataContract]
    [Serializable]
    public class TransaccionFinancial
    {
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public string tipoIdentificacion { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string tipoProducto { get; set; }
        [DataMember]
        public string numeroproducto { get; set; }
        [DataMember]
        public string tipoMov { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public Int64? codOpe { get; set; }
        [DataMember]
        public string observaciones { get; set; }
        [DataMember]
        public DateTime? fecha_oper { get; set; }
        [DataMember]
        public string numero_tarjeta { get; set; }
    }
}
