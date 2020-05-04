using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.NIIF.Entities
{
    [DataContract]
    [Serializable]
    public class ObligacionesNIF
    {
        [DataMember]
        public Int64 codcostoamortizado { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public Int64 codobligacion { get; set; }
        [DataMember]
        public string entidad { get; set; }
        [DataMember]
        public decimal monto { get; set; }
        [DataMember]
        public int plazo { get; set; }
        [DataMember]
        public decimal valor_cuota { get; set; }
        [DataMember]
        public decimal tasa { get; set; }
        [DataMember]
        public decimal saldo { get; set; }
        [DataMember]
        public int plazo_faltante { get; set; }
        [DataMember]
        public decimal tasa_mercado { get; set; }
        [DataMember]
        public decimal tir { get; set; }
        [DataMember]
        public decimal valor_presente { get; set; }
        [DataMember]
        public decimal valor_ajuste { get; set; }


        [DataMember]
        public List<ObligacionesNIF> lstLista { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string nomentidad { get; set; }
    }
}