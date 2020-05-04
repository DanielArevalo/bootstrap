using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Riesgo.Entities
{
    [DataContract]
    [Serializable]
    public class Identificacion
    {
        [DataMember]
        public Int64 cod_proceso { get; set; }
        [DataMember]
        public Int64 cod_subproceso { get; set; }
        [DataMember]
        public Int64 cod_area { get; set; }
        [DataMember]
        public Int64 cod_cargo { get; set; }
        [DataMember]
        public Int64 cod_factor { get; set; }
        [DataMember]
        public Int64 cod_riesgo { get; set; }
        [DataMember]
        public Int64 cod_causa { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string nom_proceso { get; set; }
        [DataMember]
        public string factor_riesgo { get; set; }
        [DataMember]
        public string nom_area { get; set; }
        [DataMember]
        public string nom_cargo { get; set; }
        [DataMember]
        public string nom_subproceso { get; set; }
        [DataMember]
        public string nom_factor { get; set; }
        [DataMember]
        public string sigla { get; set; }
        [DataMember]
        public string abreviatura { get; set; }
    }    
}
