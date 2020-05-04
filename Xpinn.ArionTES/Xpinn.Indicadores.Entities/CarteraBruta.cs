using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Indicadores.Entities
{
    [DataContract]
    [Serializable]
    public class CarteraBruta
    {
        [DataMember]
        public string fecha_historico { get; set; }
        [DataMember]
        public decimal total_cartera_consumo { get; set; }
        [DataMember]
        public decimal numero_cartera_consumo { get; set; }
        [DataMember]
        public decimal variacion_valor_consumo { get; set; }
        [DataMember]
        public decimal variacion_numero_consumo { get; set; }   
        [DataMember]
        public decimal total_cartera_vivienda { get; set; }
        [DataMember]
        public decimal numero_cartera_vivienda { get; set; }

        [DataMember]
        public decimal total_cartera_comercial { get; set; }
        [DataMember]
        public decimal numero_cartera_comercial { get; set; }
        [DataMember]
        public decimal variacion_valor_vivienda { get; set; }
        [DataMember]
        public decimal variacion_numero_vivienda { get; set; }  
        [DataMember]
        public decimal total_cartera_microcredito { get; set; }
        [DataMember]
        public decimal numero_cartera_microcredito { get; set; }
        [DataMember]
        public decimal variacion_valor_microcredito { get; set; }
        [DataMember]
        public decimal variacion_numero_microcredito { get; set; }  
        [DataMember]
        public decimal total_cartera { get; set; }
        [DataMember]
        public decimal numero_cartera { get; set; }
        [DataMember]
        public decimal variacion_valor { get; set; }
        [DataMember]
        public decimal variacion_numero { get; set; }  
    }
}


