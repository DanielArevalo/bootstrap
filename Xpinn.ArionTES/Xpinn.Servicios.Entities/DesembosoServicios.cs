using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Servicios.Entities
{
    [DataContract]
    [Serializable]
    public class DesembosoServicios
    {
        [DataMember]
        public Int64 numero_transaccion { get; set; }
        [DataMember]
        public int numero_servicio { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public Int64 cod_cliente { get; set; }
        [DataMember]
        public string cod_linea_servicio { get; set; }
        [DataMember]
        public int tipo_tran { get; set; }
        [DataMember]
        public Int64 cod_det_lis { get; set; }
        [DataMember]
        public int cod_atr { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public int estado { get; set; }
        [DataMember]
        public Int64 num_tran_anula { get; set; }
        [DataMember]
        public DateTime? fecha_primer_pago { get; set; }
        [DataMember]
        public DateTime? fecha_desembolso { get; set; }
        [DataMember]
        public Int64? numero_preimpreso { get; set; }
        [DataMember]
        public string error { get; set; }
    }

}
