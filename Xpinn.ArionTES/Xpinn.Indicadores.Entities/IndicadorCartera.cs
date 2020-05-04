using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Indicadores.Entities
{
    [DataContract]
    [Serializable]
    public class IndicadorCartera
    {
        [DataMember]
        public string fecha_historico { get; set; }
        [DataMember]
        public decimal valor_total { get; set; }
        [DataMember]
        public decimal porcentaje_total { get; set; }
        [DataMember]
        public decimal valor_30dias { get; set; }
        [DataMember]
        public decimal porcentaje_30dias { get; set; }
        [DataMember]
        public decimal valor_60dias { get; set; }
        [DataMember]
        public decimal porcentaje_60dias { get; set; }
        [DataMember]
        public decimal valor_90dias { get; set; }
        [DataMember]
        public decimal porcentaje_90dias { get; set; }
        [DataMember]
        public decimal valor_120dias { get; set; }
        [DataMember]
        public decimal porcentaje_120dias { get; set; }
        [DataMember]
        public decimal valor_180dias { get; set; }
        [DataMember]
        public decimal porcentaje_180dias { get; set; }
        [DataMember]
        public decimal valor_360dias { get; set; }
        [DataMember]
        public decimal porcentaje_360dias { get; set; }

        /// <summary>
        /// agregado para la grafica colocacionporoficinas
        /// </summary>
        [DataMember]
        public int numero { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public string nombre { get; set; }

        /// <summary>
        /// agregado para el valor de la cartera en participacion pagadurias
        /// </summary>
        [DataMember]
        public decimal valor_cartera { get; set; }
        [DataMember]
        public decimal valor_mora { get; set; }
        [DataMember]
        public decimal valor_cartera_aldia { get; set; }
        [DataMember]
        public decimal contribucion { get; set; }
        [DataMember]
        public string mes { get; set; }
        [DataMember]
        public int año { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public string cod_categoria { get; set; }
        [DataMember]
        public string nom_categoria { get; set; }

        [DataMember]
        public string cod_linea_credito { get; set; }

        [DataMember]
        public string nom_linea { get; set; }

        [DataMember]
        public decimal valor_aprobado { get; set; }
    }
}


