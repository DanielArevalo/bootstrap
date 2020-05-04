using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Obligaciones.Entities
{
    /// <summary>
    /// Entidad Obligacion Credito
    /// </summary>
    [DataContract]
    [Serializable]
    public class ObligacionCredito
    {
        [DataMember]
        public Int64 codobligacion { get; set; }
        [DataMember]
        public Int64 numeropagare { get; set; }
        [DataMember]
        public Int64 codentidad { get; set; }
        [DataMember]
        public string nomentidad { get; set; }
        [DataMember]
        public string fecha_inicio { get; set; }
        [DataMember]
        public string fecha_final { get; set; }
        [DataMember]
        public string fecha_aprobacion { get; set; }
        [DataMember] 
        public DateTime fecha_solicitud { get; set; }
        [DataMember]
        public DateTime fecha_ultpago { get; set; }
        [DataMember]
        public Int64 codperiodicidad { get; set; }
        [DataMember]
        public string nomperiodicidad { get; set; }
        [DataMember]
        public Int64 montoaprobado { get; set; }
        [DataMember]
        public decimal montosolicitud { get; set; }
        [DataMember]
        public Int64 saldocapital { get; set; }
        [DataMember]
        public Int64 cuota { get; set; }
        [DataMember]
        public string fechaproximopago { get; set; }
        [DataMember]
        public DateTime fechapago { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public Int64 cod_tipo_ope { get; set; }
        [DataMember]
        public string tipo_ope { get; set; }
        [DataMember]
        public string tipo_mov { get; set; }
        [DataMember]
        public string estadoobligacion { get; set; }
        [DataMember]
        public string codfiltroorderuno { get; set; }
        [DataMember]
        public string codfiltroorderdos { get; set; }
        [DataMember]
        public string codfiltroordertres { get; set; }
        [DataMember]
        public string entidad { get; set; }
        [DataMember]
        public Int64 nrocuota { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public DateTime fechacuota { get; set; }
        [DataMember]
        public DateTime fechareal { get; set; }
        [DataMember]
        public decimal amort_cap { get; set; }
        [DataMember]
        public decimal interes_corriente { get; set; }
        [DataMember]
        public decimal interes_mora { get; set; }
        [DataMember]
        public decimal seguro { get; set; }
        [DataMember]
        public decimal cuotaextra { get; set; }
        [DataMember]
        public decimal cuotanormal { get; set; }
        [DataMember]
        public decimal total { get; set; }
        [DataMember]
        public decimal saldo { get; set; }
        [DataMember]
        public decimal cuotatotal { get; set; }
        [DataMember]
        public Int64 num_comp { get; set; }
        [DataMember]
        public Int64 tipo_comp { get; set; }
        [DataMember]
        public Int64 tipo_tran{ get; set; }
        [DataMember]
        public Int64 codlineaobligacion { get; set; }
        [DataMember]
        public string nomlineaobligacion { get; set; }
        [DataMember]
        public decimal tasa { get; set; }
        [DataMember]
        public decimal puntos_adicionales { get; set; }
        [DataMember]
        public string nombre_tasa { get; set; }
        [DataMember]
        public Int64 plazo { get; set; }
        [DataMember]
        public DateTime fecha_corte { get; set; }
        [DataMember]
        public string sfecha_corte { get; set; }
        [DataMember]
        public decimal intereses { get; set; }
        [DataMember]
        public Int64 dias_causados { get; set; }
        [DataMember]
        public string tipo_cierre { get; set; }

    }
}
