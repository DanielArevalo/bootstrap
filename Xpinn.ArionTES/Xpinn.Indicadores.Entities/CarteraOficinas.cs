using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Indicadores.Entities
{
    [DataContract]
    [Serializable]
    public class CarteraOficinas
    {
        [DataMember]
        public string fecha_corte { get; set; }
        [DataMember]
        public string cod_oficina { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public decimal total_cartera { get; set; }
        [DataMember]
        public decimal numero_cartera { get; set; }
        [DataMember]
        public decimal participacion_cartera { get; set; }
        [DataMember]
        public decimal participacion_numero { get; set; }
        [DataMember]
        public decimal total_cartera_c { get; set; }
        [DataMember]
        public decimal numero_cartera_c { get; set; }
        [DataMember]
        public decimal participacion_cartera_c { get; set; }
        [DataMember]
        public decimal participacion_numero_c { get; set; }
        [DataMember]
        public string Cod_linea_credito { get; set; }
    }
}


