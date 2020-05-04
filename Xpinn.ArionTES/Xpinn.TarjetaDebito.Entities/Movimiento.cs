using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.TarjetaDebito.Entities
{
    [DataContract]
    [Serializable]
    public class Movimiento
    {        
        [DataMember]
        public string fecha { get; set; }
        [DataMember]
        public string hora { get; set; }
        [DataMember]
        public string documento { get; set; }
        [DataMember]
        public string nrocuenta { get; set; }
        [DataMember]
        public string tarjeta { get; set; }
        [DataMember]
        public string tipotransaccion { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public decimal monto { get; set; }
        [DataMember]
        public decimal comision { get; set; }
        [DataMember]
        public string lugar { get; set; }
        [DataMember]
        public string operacion { get; set; }
        [DataMember]
        public string red { get; set; }
        [DataMember]
        public Int64? cod_cliente { get; set; }
        [DataMember]
        public string tipocuenta { get; set; }
        [DataMember]
        public Int32? tipo_tran { get; set; }
        [DataMember]
        public Int32? num_tran { get; set; }
        [DataMember]
        public Int64? idtarjeta { get; set; }
        [DataMember]
        public Int64? cod_ope { get; set; }
        [DataMember]
        public decimal? saldo_total { get; set; }
        [DataMember]
        public string numero_tarjeta { get; set; }
        [DataMember]
        public bool esdatafono { get; set; }
        [DataMember]
        public string secuencia { get; set; }
        [DataMember]
        public string cuenta { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public string error { get; set; }
        [DataMember]
        public string subcausal { get; set; }
        [DataMember]
        public Int64? num_tran_tarjeta { get; set; }
        [DataMember]
        public Int64? num_tran_apl { get; set; }
        [DataMember]
        public Int64? cod_ope_apl { get; set; }
        [DataMember]
        public decimal? valor_apl { get; set; }
        [DataMember]
        public decimal? comision_apl { get; set; }
        [DataMember]
        public Int64? num_comp_apl { get; set; }
        [DataMember]
        public Int64? tipo_comp_apl { get; set; }
        [DataMember]
        public Int64? num_tran_verifica { get; set; }
        [DataMember]
        public DateTime? fecha_movimiento { get; set; }
        [DataMember]
        public decimal diferencia { get; set; }
        [DataMember]
        public int? cod_ofi { get; set; }
        [DataMember]
        public decimal cuenta_porcobrar { get; set; }
        [DataMember]
        public decimal monto_ath { get; set; }
        [DataMember]
        public decimal monto_pos { get; set; }
        [DataMember]
        public decimal monto_plastico { get; set; }
        [DataMember]
        public string fecha_corte { get; set; }
    }

}
