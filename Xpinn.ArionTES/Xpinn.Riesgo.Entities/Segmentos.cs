using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Riesgo.Entities
{
    [DataContract]
    [Serializable]
    public class Segmentos
    {
        [DataMember]
        public int codsegmento { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string tipo_variable { get; set; }
        [DataMember]
        public int calificacion_segmento { get; set; }
        [DataMember]
        public string factor_riesgo { get; set; }
        [DataMember]
        public bool valida_alguno { get; set; }

        //Agregado
        [DataMember]
        public List<Segmento_Detalles> lstDetalle { get; set; }
    }

    public class listaMultiple
    {
        [DataMember]
        public int cod_act { get; set; }
        [DataMember]
        public string nombre_act { get; set; }
    }

    public class tipoVariable
    {
        [DataMember]
        public int cod_variable { get; set; }
        [DataMember]
        public string nombre_variable { get; set; }
        [DataMember]
        public int riesgo_bajo { get; set; }
        [DataMember]
        public int riesgo_moderado { get; set; }
        [DataMember]
        public int riesgo_alto { get; set; }
        [DataMember]
        public int riesgo_extremo { get; set; }
        [DataMember]
        public List<tipoVariable> lstVariables { get; set; }
    }

}
