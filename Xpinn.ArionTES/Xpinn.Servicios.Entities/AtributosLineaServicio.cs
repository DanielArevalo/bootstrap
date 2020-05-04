using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Servicios.Entities
{
    [DataContract]
    [Serializable]
    public class AtributosLineaServicio
    {
        [DataMember]
        public int codatrser { get; set; }
        [DataMember]
        public int codrango { get; set; }
        [DataMember]
        public string cod_linea_servicio { get; set; }
        [DataMember]
        public int cod_atr { get; set; }
        [DataMember]
        public string calculo_atr { get; set; }
        [DataMember]
        public int? tipo_historico { get; set; }
        [DataMember]
        public decimal? desviacion { get; set; }
        [DataMember]
        public int? tipo_tasa { get; set; }
        [DataMember]
        public decimal? tasa { get; set; }
    }
}
