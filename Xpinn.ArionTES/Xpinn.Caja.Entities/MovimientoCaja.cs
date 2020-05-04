using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Caja.Entities
{
    /// <summary>
    /// Entidad MovimientoCaja
    /// </summary>
    [DataContract]
    [Serializable]
    public class MovimientoCaja
    {
        [DataMember]
        public Int64 cod_movimiento { get; set; }
        [DataMember]
        public Int64 Cod_Consignacion { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public DateTime fec_ope { get; set; }
        [DataMember]
        public Int64 cod_caja { get; set; }
        [DataMember]
        public Int64 cod_cajero { get; set; }
        [DataMember]
        public string tipo_mov { get; set; }
        [DataMember]
        public Int64 cod_tipo_pago { get; set; }
        [DataMember]
        public Int64 cod_banco { get; set; }
        [DataMember]
        public string nom_banco { get; set; }
        [DataMember]
        public string num_documento { get; set; }
        [DataMember]
        public string tipo_documento { get; set; }
        [DataMember]
        public Int64 cod_moneda { get; set; }
        [DataMember]
        public string nom_moneda { get; set; }
        [DataMember]
        public Int64 valor { get; set; }
        [DataMember]
        public Int64 estado { get; set; }
        [DataMember]
        public DateTime fechaCierre { get; set; }
        [DataMember]
        public String fechacierre { get; set; }
        [DataMember]
        public Int64 orden { get; set; }
        [DataMember]
        public string concepto { get; set; }
        [DataMember]
        public decimal efectivo { get; set; }
        [DataMember]
        public decimal cheque { get; set; }
        [DataMember]
        public decimal consignacion { get; set; }
        [DataMember]
        public decimal datafono { get; set; }
        [DataMember]
        public decimal total { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public Int64 cod_usuario { get; set; }
        [DataMember]
        public string titular { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string estado_cheque { get; set; }
        /// <summary>
        /// agregado para poder modificar el arqueo
        /// </summary>
        [DataMember]
        public long id_arqueo { get; set; }

        /// <summary>
        /// agregado para poder modificar la Consignacion del Cheque
        /// </summary>
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string numcuenta { get; set; }
        [DataMember]
        public string filtro { get; set; }
    }
}