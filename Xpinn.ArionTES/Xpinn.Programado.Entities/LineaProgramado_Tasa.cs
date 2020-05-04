using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Xpinn.Programado.Entities
{
   public class LineaProgramado_Tasa
    {
        [DataMember]
        public int idtasa { get; set; }
        [DataMember]
        public int idrango { get; set; }
        [DataMember]
        public string cod_linea_programado { get; set; }
        [DataMember]
        public int tipo_interes { get; set; }
        [DataMember]
        public decimal? tasa { get; set; }
        [DataMember]
        public int? cod_tipo_tasa { get; set; }
        [DataMember]
        public int? tipo_historico { get; set; }
        [DataMember]
        public decimal? desviación { get; set; }
    }
}
