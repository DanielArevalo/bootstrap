using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Cartera.Entities
{
    [DataContract]
    [Serializable]
    public class ProvisionGeneral
    {
        [DataMember]
        public int idprovision { get; set; }
        [DataMember]
        public DateTime? fecha_corte { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public string nombre_oficina { get; set; }
        [DataMember]
        public int forma_pago { get; set; }
        [DataMember]
        public decimal? valor_total { get; set; }
        [DataMember]
        public decimal? provision_acumulada { get; set; }
        [DataMember]
        public decimal? valor_provision { get; set; }
        // Variables para guardar los calculos para mostrar en pantalla
        [DataMember]
        public decimal? valor_sinlibranza { get; set; }
        [DataMember]
        public decimal? total_provision_sinlibranza { get; set; }        
        [DataMember]
        public decimal? provision_sinlibranza { get; set; }
        [DataMember]
        public decimal? valor_conlibranza { get; set; }
        [DataMember]
        public decimal? total_provision_conlibranza { get; set; }
        [DataMember]
        public decimal? provision_conlibranza { get; set; }
    }
}