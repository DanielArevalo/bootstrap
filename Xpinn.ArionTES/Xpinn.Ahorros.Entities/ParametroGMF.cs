using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Ahorros.Entities
{
    [DataContract]
    [Serializable]
    public class ParametroGMF
    {
        [DataMember]
        public Int64 idparametrogmf { get; set; }
        [DataMember]
        public int tipo_ope { get; set; }
        [DataMember]
        public int? tipo_tran { get; set; }
        [DataMember]
        public int? afecta_producto { get; set; }
        [DataMember]
        public int? asume { get; set; }
        [DataMember]
        public decimal? porasume_cliente { get; set; }
        [DataMember]
        public int? maneja_exentas { get; set; }
        [DataMember]
        public int? pago_cheque { get; set; }
        [DataMember]
        public int? pago_efectivo { get; set; }
        [DataMember]
        public int? pago_traslado { get; set; }
        [DataMember]
        public int? tipo_producto { get; set; }
        [DataMember]
        public int? numero_cuenta { get; set; }
        [DataMember]
        public string cod_linea { get; set; }
        [DataMember]
        public string descripcion { get; set; }
         [DataMember]
        public string transaccion { get; set; } 
        [DataMember]
        public string operacion { get; set; }

        [DataMember]
        public string nom_linea { get; set; }
    }
}