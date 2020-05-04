using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class Producto
    {
        [DataMember]
        public Int64? tipo_producto { get; set; }
        [DataMember]
        public string nom_tipo_producto { get; set; }
        [DataMember]
        public string num_producto { get; set; }
        [DataMember]
        public DateTime fecha_proximo_pago { get; set; }
        [DataMember]
        public decimal saldo { get; set; }
        [DataMember]
        public decimal cuota { get; set; }
        [DataMember]
        public decimal? valor_a_aplicar { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string cod_linea { get; set; }
        [DataMember]
        public decimal valor_a_pagar { get; set; }
        [DataMember]
        public decimal total_a_pagar { get; set; }
        [DataMember]
        public Int64? prioridad { get; set; }
        [DataMember]
        public int? refinanciado { get; set; }

        [DataMember]
        public int? vacaciones { get; set; }
    }
}

