using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class NovedadPrima
    {
        [DataMember]
        public long consecutivo { get; set; }
        [DataMember]
        public long codigoempleado { get; set; }
        [DataMember]
        public int semestre { get; set; }
        [DataMember]
        public long codigotiponovedad { get; set; }
        [DataMember]
        public long tipoCalculoNovedad { get; set; }
        [DataMember]
        public string descripcionNovedad { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public int novedadfuepagada { get; set; }
        [DataMember]
        public long anio { get; set; }
        [DataMember]
        public long codigonomina { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string desc_nomina { get; set; }
        [DataMember]
        public string desc_tipoNovedad { get; set; }
        [DataMember]
        public string desc_semestre { get; set; }
        [DataMember]
        public string desc_fuepagada { get; set; }
        [DataMember]
        public string tipo_identificacion { get; set; }
    }
}