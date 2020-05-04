using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Indicadores.Entities
{
    [DataContract]
    [Serializable]
    public class IndicadoresAportes
    {
        [DataMember]
        public string fecha_historico { get; set; }
        [DataMember]
        public decimal total { get; set; }
        [DataMember]
        public decimal numero { get; set; }
        [DataMember]
        public decimal variacion_valor { get; set; }
        [DataMember]
        public decimal variacion_numero { get; set; }

        [DataMember]
        public string fecha_corte { get; set; }
        [DataMember]
        public decimal año { get; set; }
        [DataMember]
        public decimal mes { get; set; }
        [DataMember]
        public String mesgrafica { get; set; }
    }
}


