using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class Giro
    {
        [DataMember]
        public Int64 idgiro { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public Int64? forma_pago { get; set; }
        [DataMember]
        public string nom_forma_pago { get; set; }
        [DataMember]
        public Int64? tipo_acto { get; set; }
        [DataMember]
        public DateTime? fec_reg { get; set; }
        [DataMember]
        public DateTime? fec_giro { get; set; }
        [DataMember]
        public Int64? numero_radicacion { get; set; }
        [DataMember]
        public Int64? cod_ope { get; set; }
        [DataMember]
        public Int64? num_comp { get; set; }
        [DataMember]
        public int? tipo_comp { get; set; }
        [DataMember]
        public string nom_tipo_comp { get; set; }
        [DataMember]
        public string usu_gen { get; set; }
        [DataMember]
        public string usu_apli { get; set; }
        [DataMember]
        public Int64? estado { get; set; }
        [DataMember]
        public string nom_estado { get; set; }
        [DataMember]
        public string usu_apro { get; set; } 
        [DataMember]
        public Int64 idctabancaria { get; set; }
        [DataMember]
        public string num_referencia { get; set; }
        [DataMember]
        public Int64? cod_banco { get; set; }
        [DataMember]
        public string nom_banco { get; set; }
        [DataMember]
        public Int64 tipo_cuenta { get; set; }
        [DataMember]
        public string num_referencia1 { get; set; }
        [DataMember]
        public Int64? cod_banco1 { get; set; }
        [DataMember]
        public string nom_banco1 { get; set; }
        [DataMember]
        public DateTime? fec_apro { get; set; }
        [DataMember]
        public DateTime fec_apro_giro { get; set; }
        [DataMember]
        public int estadogi { get; set; }
        [DataMember]
        public Int64? cob_comision { get; set; }
        [DataMember]
        public decimal? valor { get; set; }
        [DataMember]
        public string num_cuenta { get; set; }
        [DataMember]
        public string nom_generado { get; set; }
        [DataMember]
        public int distribuir { get; set; }
        [DataMember]
        public string identif_bene { get; set; }
        [DataMember]
        public string nombre_bene { get; set; }
        [DataMember]
        public Int64 cod_persona_deta { get; set; }
        [DataMember]
        public bool activar { get; set; }        
        [DataMember]
        public List<Giro> lstGiro { get; set; }
        [DataMember]
        public string nom_tipo_cuenta { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string detalle { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public decimal? valor_comprobante { get; set; }
        [DataMember]
        public Int64? cod_ope_realiza { get; set; }
        [DataMember]
        public DateTime? fec_realiza { get; set; }
        [DataMember]
        public string usu_realiza { get; set; }
        [DataMember]
        public string cod_cuenta_realiza { get; set; }
        [DataMember]
        public decimal? valor_realiza { get; set; }
        [DataMember]
        public string identificacion_beneficiario { get; set; }
        [DataMember]
        public string  nombre_beneficiario { get; set; }
        [DataMember]
        public int codpagofac { get; set; }

        [DataMember]
        public string primer_nombre { get; set; }
        [DataMember]
        public string segundo_nombre { get; set; }

        [DataMember]
        public string primer_apellido { get; set; }

        [DataMember]
         public string segundo_apellido { get; set; }

        [DataMember]
        public long? numero_cuenta_ahorro_vista { get; set; }

        [DataMember]
        public string formadesembolso { get; set; }
    }
}