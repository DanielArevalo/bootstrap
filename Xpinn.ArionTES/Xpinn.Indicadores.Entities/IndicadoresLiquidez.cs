using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Indicadores.Entities
{
    [DataContract]
    [Serializable]
    public class IndicadoresLiquidez
    {
        [DataMember]
        public string fecha { get; set; }
        [DataMember]
        public decimal total { get; set; }
        [DataMember]
        public decimal cod_cuenta { get; set; }
        [DataMember]
        public String descripcion { get; set; }
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


