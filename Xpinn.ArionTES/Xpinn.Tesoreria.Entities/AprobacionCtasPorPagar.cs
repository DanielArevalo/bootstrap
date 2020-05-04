using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class AprobacionCtasPorPagar
    {
        [DataMember]
        public int codpagofac { get; set; }
        [DataMember]
        public int codigo_factura { get; set; }

        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public string numero_factura { get; set; }
        [DataMember]
        public DateTime? fecha_ingreso { get; set; }
        [DataMember]
        public DateTime? fecha_vencimiento { get; set; }
        [DataMember]
        public int idtipo_cta_por_pagar { get; set; }       
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public DateTime? fec_fact { get; set; }
        [DataMember]
        public decimal valortotal { get; set; }
        [DataMember]
        public decimal valorneto { get; set; }
        [DataMember]
        public decimal valordescuento { get; set; }
        [DataMember]
        public decimal saldo_actual { get; set; }
        [DataMember]
        public decimal nuevo_saldo { get; set; }

        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public int estado { get; set; }

        [DataMember]
        public int forma_pago { get; set; }
        [DataMember]
        public Int64 cod_banco { get; set; }
        [DataMember]
        public int tipo_cuenta { get; set; }
        [DataMember]
        public string num_cuenta { get; set; }
    }

}