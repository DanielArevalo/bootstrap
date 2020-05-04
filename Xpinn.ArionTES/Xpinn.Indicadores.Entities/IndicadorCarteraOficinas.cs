using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Indicadores.Entities
{
    [DataContract]
    [Serializable]
    public class IndicadorCarteraOficinas
    {
        [DataMember]
        public string fecha_historico { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public decimal porcentaje_total { get; set; }
        [DataMember]
        public decimal porcentaje_30dias { get; set; }
        [DataMember]
        public decimal valor_total { get; set; }
        [DataMember]
        public decimal valor_30dias { get; set; }
        [DataMember]
        public decimal porcentaje_total_c { get; set; }
        [DataMember]
        public decimal porcentaje_30dias_c { get; set; }
        [DataMember]
        public decimal valor_total_c { get; set; }
        [DataMember]
        public decimal valor_30dias_c { get; set; }
    }
}
