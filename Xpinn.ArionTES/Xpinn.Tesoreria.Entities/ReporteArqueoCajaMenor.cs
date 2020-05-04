using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    public class ArqueoCajaMenor
    {
        [DataMember]
        public Int64 idarea { get; set; }
        [DataMember]
        public string persona { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public Int64? valor_minimo { get; set; }
        [DataMember]
        public Int64 denominacion { get; set; }
        [DataMember]
        public Int64 cantidad { get; set; }
        [DataMember]
        public Int64 valor { get; set; }
        [DataMember]
        public string tipo_efectivo { get; set; }
        [DataMember]
        public Int64 id_soporte { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public List<ArqueoCajaMenor> lista_extracto_legalizados { get; set; }
        [DataMember]
        public List<ArqueoCajaMenor> lista_extracto_no_legalizados { get; set; }
        [DataMember]
        public List<ArqueoCajaMenor> lista_extracto_efectivo { get; set; }
        [DataMember]
        public ArqueoCajaMenor infocaja { get; set; }
        [DataMember]
        public ArqueoCajaMenor resumenArqueo { get; set; }
        [DataMember]
        public Int64 total_legalizados { get; set; }
        [DataMember]
        public Int64 total_no_legalizados { get; set; }
        [DataMember]
        public Int64 total_gastos { get; set; }
        [DataMember]
        public Int64 total_efectivo { get; set; }
        [DataMember]
        public Int64 diferencia { get; set; }
    }
}