using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Indicadores.Entities
{
    [DataContract]
    [Serializable]
    public class EvolucionDesembolsos
    {
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string total_desembolsos { get; set; }
        [DataMember]
        public string fecha_historico { get; set; }
        [DataMember]
        public string numero_desembolsos { get; set; }
        [DataMember]
        public string total_desembolsos_vivienda { get; set; }
        [DataMember]
        public string fecha_historico_vivienda { get; set; }
        [DataMember]
        public string numero_desembolsos_vivienda { get; set; }
        [DataMember]
        public string total_desembolsos_consumo { get; set; }
        [DataMember]
        public string fecha_historico_consumo { get; set; }
        [DataMember]
        public string numero_desembolsos_consumo { get; set; }
        [DataMember]
        public string total_desembolsos_microcredito { get; set; }
        [DataMember]
        public string numero_desembolsos_comercial { get; set; }
        [DataMember]
        public string total_desembolsos_comercial { get; set; }

        [DataMember]
        public string fecha_historico_microcredito { get; set; }
        [DataMember]
        public string numero_desembolsos_microcredito { get; set; }
    }
}


