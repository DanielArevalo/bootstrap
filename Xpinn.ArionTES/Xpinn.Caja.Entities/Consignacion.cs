using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Caja.Entities
{
    /// <summary>
    /// Entidad Consignacion
    /// </summary>
    [DataContract]
    [Serializable]
    public class Consignacion
    {
        [DataMember] 
        public Int64 cod_consignacion { get; set; }
        [DataMember] 
        public Int64 cod_caja { get; set; }
        [DataMember]
        public Int64 caja_principal { get; set; }
        [DataMember]
        public string nom_caja { get; set; }
        [DataMember] 
        public Int64 cod_cajero { get; set; }
        [DataMember]
        public string nom_cajero { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public string nom_oficina { get; set; }
        [DataMember] 
        public DateTime fecha_consignacion { get; set; }
        [DataMember] 
        public Int64 cod_banco { get; set; }
        [DataMember]
        public String nom_banco { get; set; }
        [DataMember] 
        public Int64 cod_moneda { get; set; }
        [DataMember]
        public String nom_moneda { get; set; }
        [DataMember] 
        public decimal valor_efectivo { get; set; }
        [DataMember] 
        public decimal valor_cheque { get; set; }
        [DataMember]
        public decimal valor_consignacion_total { get; set; }
        [DataMember] 
        public string observaciones { get; set; }
        [DataMember] 
        public DateTime fecha_salida { get; set; }
        [DataMember]
        public Int64 cod_movimiento { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public Int64 ip { get; set; }
        [DataMember]
        public Int64 tipo_ope { get; set; }
        [DataMember]
        public string Cuenta { get; set; }
        [DataMember]
        public string documento { get; set; }

        [DataMember]
        public Int64 estado { get; set; }

        [DataMember]
        public Int64 cod_persona{ get; set; }
    }
}