using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Indicadores.Entities
{
    [DataContract]
    [Serializable]
    public class GestionRiesgo
    {
        [DataMember]
        public string fecha_historico { get; set; }
        [DataMember]
        public string segmento { get; set; }
        [DataMember]
        public string cod_categoria { get; set; }
        [DataMember]
        public decimal numero { get; set; }
        [DataMember]
        public decimal saldo { get; set; }
    }
}


