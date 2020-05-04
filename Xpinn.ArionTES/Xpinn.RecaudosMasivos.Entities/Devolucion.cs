using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class Devolucion
    {
        [DataMember]
        public Int64 num_devolucion { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string cod_nomina { get; set; }
        [DataMember]
        public string concepto { get; set; }
        [DataMember]
        public DateTime? fecha_descuento { get; set; }
        [DataMember]
        public DateTime fecha_devolucion { get; set; }
        [DataMember]
        public Int64? num_recaudo { get; set; }
        [DataMember]
        public Int64 iddetalle { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public string origen { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public decimal saldo { get; set; }
        [DataMember]
        public decimal valor_a_aplicar { get; set; }
        //agregado
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public int? numero_recaudo { get; set; }
        [DataMember]
        public string detalle { get; set; }
        [DataMember]
        public Int64? cod_ope { get; set; }
        [DataMember]
        public Int64? numero_transaccion { get; set; }
        [DataMember]
        public Int64? tipo_tran { get; set; }

        //agregado para ajuste de oficina
        [DataMember]
        public string nom_oficina { get; set; }

        [DataMember]
        public Int64 num_comp { get; set; }
        [DataMember]
        public Int64 tipo_comp { get; set; }
        [DataMember]
        public DateTime fecha_oper { get; set; }

    }

}



