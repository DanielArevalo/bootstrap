using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.NIIF.Entities
{
    [DataContract]
    [Serializable]
    public class TasaMercadoNIF
    {

        [DataMember]
        public int idvariable { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public int cod_tasa_mercado { get; set; }
        [DataMember]
        public DateTime fecha_inicial { get; set; }
        [DataMember]
        public DateTime fecha_final { get; set; }
        [DataMember]
        public decimal tasa { get; set; }
        [DataMember]
        public int tipo_tasa { get; set; }
        [DataMember]
        public string observaciones { get; set; }
        [DataMember]
        public List<Xpinn.NIIF.Entities.TasaMercadoCondicionNIF> lstTasaCondi { get; set; }
        [DataMember]
        public string variable { get; set; }
    }
}