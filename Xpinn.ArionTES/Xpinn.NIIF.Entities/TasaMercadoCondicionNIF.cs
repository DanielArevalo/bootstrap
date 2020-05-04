using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.NIIF.Entities
{
    [DataContract]
    [Serializable]
    public class TasaMercadoCondicionNIF
    {

        [DataMember]
        public int cod_tasa_condicion { get; set; }
        [DataMember]
        public int cod_tasa_mercado { get; set; }
        [DataMember]
        public int? variable { get; set; }
        [DataMember]
        public string operador { get; set; }
        [DataMember]
        public string valor { get; set; }
         [DataMember]
        public List<TasaMercadoCondicionNIF> lstTasaCondi { get; set; }
    }
}