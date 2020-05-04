using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Indicadores.Entities
{
    [DataContract]
    [Serializable]
    public class DistribucionCarteraOficinas
    {
        [DataMember]
        public string fecha_historico { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public decimal porcentaje30 { get; set; }
        [DataMember]
        public decimal valor30 { get; set; }
        [DataMember]
        public decimal porcentaje60 { get; set; }
        [DataMember]
        public decimal valor60 { get; set; }
        [DataMember]
        public decimal porcentaje90 { get; set; }
        [DataMember]
        public decimal valor90 { get; set; }
        [DataMember]
        public decimal porcentaje120 { get; set; }
        [DataMember]
        public decimal valor120 { get; set; }
        [DataMember]
        public decimal porcentaje150 { get; set; }
        [DataMember]
        public decimal valor150 { get; set; }
        [DataMember]
        public decimal porcentaje180 { get; set; }
        [DataMember]
        public decimal valor180 { get; set; }
        [DataMember]
        public decimal porcentajeult { get; set; }
        [DataMember]
        public decimal valorult { get; set; }
    }
}
