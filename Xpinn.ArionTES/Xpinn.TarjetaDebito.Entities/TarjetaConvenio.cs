using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Xpinn.TarjetaDebito.Entities
{
    [DataContract]
    [Serializable]
    public class TarjetaConvenio
    {
        [DataMember]
        public string cod_convenio { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string codigo_bin { get; set; }
        [DataMember]
        public string ip_switch { get; set; }
        [DataMember]
        public string puerto_switch { get; set; }
        [DataMember]
        public DateTime? fecha_convenio { get; set; }
        [DataMember]
        public string encargado { get; set; }
        [DataMember]
        public string e_mail { get; set; }
        [DataMember]
        public string e_cc_1 { get; set; }
        [DataMember]
        public string e_cc_2 { get; set; }
        [DataMember]
        public decimal? comision { get; set; }
        [DataMember]
        public decimal? cuota_manejo { get; set; }
        [DataMember]
        public int? tipo_procesamiento { get; set; }
        [DataMember]
        public decimal? cupo_cajero { get; set; }
        [DataMember]
        public int? transacciones_cajero { get; set; }
        [DataMember]
        public decimal? cupo_datafono { get; set; }
        [DataMember]
        public int? transacciones_datafono { get; set; }
        [DataMember]
        public decimal? valor_cancela_tarjeta { get; set; }
        [DataMember]
        public int? cobro_cancela_tarjeta { get; set; }
        [DataMember]
        public int? cobra_tarjeta_bloqueada { get; set; }
        [DataMember]
        public string ip_appliance { get; set; }
        [DataMember]
        public string usuario_appliance { get; set; }
        [DataMember]
        public string clave_appliance { get; set; }
        [DataMember]
        public int? tipo_convenio { get; set; }
    }
    public class Email
    {
        [DataMember]
        public string email { get; set; }

        [DataMember]
        public int proveedor { get; set; }

        [DataMember]
        public string Clave { get; set; }

        [DataMember]
        public int tipo_email { get; set; }

        [DataMember]
        public int estado { get; set; }
    }
}
