using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Riesgo.Entities
{
    [DataContract]
    [Serializable()]
    public class Matriz
    {
        [DataMember]
        public Int64 cod_matriz { get; set; }
        //[DataMember]
        //public Int64 cod_matriz_control { get; set; }
        [DataMember]
        public Int64 cod_riesgo { get; set; }
        [DataMember]
        public Int64 cod_factor { get; set; }
        [DataMember]
        public Int64 cod_causa { get; set; }
        [DataMember]
        public Int64 cod_probabilidad { get; set; }
        [DataMember]
        public Int64 cod_impacto { get; set; }
        [DataMember]
        public Int64 cod_control { get; set; }
        [DataMember]
        public Int64 cod_monitoreo { get; set; }
        [DataMember]
        public Int64 cod_alerta { get; set; }
        [DataMember]
        public Int64 forma { get; set; }
        [DataMember]
        public Int64 ejecucion { get; set; }
        [DataMember]
        public Int64 documentacion { get; set; }
        [DataMember]
        public Int64 complejidad { get; set; }
        [DataMember]
        public Int64 fallas { get; set; }
        [DataMember]
        public Int64 nivel_reduccion { get; set; }
        [DataMember]
        public Int64 clase { get; set; }
        [DataMember]
        public int valor_control { get; set; }
        [DataMember]
        public Int64 valor_rinherente { get; set; }
        [DataMember]
        public Int64 valor_rresidual { get; set; }
        [DataMember]
        public string nivel { get; set; }
        [DataMember]
        public string nivel_impacto { get; set; }
        [DataMember]
        public string desc_factor { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string desc_probabilidad { get; set; }
        [DataMember]
        public string desc_impacto { get; set; }
        [DataMember]
        public string desc_control { get; set; }
        [DataMember]
        public string desc_clase { get; set; }
        [DataMember]
        public string desc_forma { get; set; }
        [DataMember]
        public string desc_alerta { get; set; }
        [DataMember]
        public string desc_area { get; set; }
        [DataMember]
        public string desc_cargo { get; set; }
        [DataMember]
        public string desc_causa { get; set; }
        [DataMember]
        public string abreviatura { get; set; }
        [DataMember]
        public string sigla { get; set; }
        [DataMember]
        public string desc_sistema { get; set; }
        [DataMember]
        public string frecuencia { get; set; }
        [DataMember]
        public string impacto_reputacional { get; set; }
        [DataMember]
        public string impacto_legal { get; set; }
        [DataMember]
        public string impacto_operativo { get; set; }
        [DataMember]
        public string impacto_contagio { get; set; }
        [DataMember]
        public Int32 calificacion { get; set; }
        [DataMember]
        public List<Matriz> lstDetalle { get; set; }
        [DataMember]
        public Int32 rango_minimo { get; set; }
        [DataMember]
        public Int32 rango_maximo { get; set; }
    }
}
