using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Cartera.Entities
{
    [DataContract]
    [Serializable]
    public class Clasificacion
    {
        [DataMember]
        public int cod_clasifica { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int? tipo_historico { get; set; } 
        [DataMember]
        public string nom_tipo_historico { get; set; }
        [DataMember]
        public int? estado { get; set; }
        [DataMember]
        public int? aportes_garantia { get; set; }
        [DataMember]
        public int? aportes_gar_clasificacion { get; set; }
    }
}