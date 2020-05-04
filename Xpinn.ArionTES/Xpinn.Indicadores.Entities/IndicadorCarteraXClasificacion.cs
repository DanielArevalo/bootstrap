using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Indicadores.Entities
{
    [DataContract]
    [Serializable]
    public class IndicadorCarteraXClasificacion
    {
        [DataMember]
        public string fecha_historico { get; set; }
        [DataMember]
        public decimal porcentaje_microcredito { get; set; }
        [DataMember]
        public decimal porcentaje_30microcredito { get; set; }
        [DataMember]
        public decimal porcentaje_consumo { get; set; }
        [DataMember]
        public decimal porcentaje_30consumo { get; set; }
        [DataMember]
        public decimal porcentaje_vivienda { get; set; }
        [DataMember]
        public decimal porcentaje_30vivienda { get; set; }
        [DataMember]
        public decimal porcentaje_comercial { get; set; }
        [DataMember]
        public decimal porcentaje_30comercial { get; set; }

    }
}


