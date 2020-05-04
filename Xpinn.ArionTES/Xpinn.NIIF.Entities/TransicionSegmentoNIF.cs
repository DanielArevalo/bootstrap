using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.NIIF.Entities
{
    [DataContract]
    [Serializable]
    public class TransicionSegmentoNIF
    {
        [DataMember]
        public int codsegmento { get; set; }
        [DataMember]
        public string nombre { get; set; }

        //Agregado
        [DataMember]
        public List<TransicionDetalle> lstDetalle { get; set; }

    }



    [DataContract]
    [Serializable]
    public class TransicionDetalle
    {
        [DataMember]
        public int idcondiciontran { get; set; }
        [DataMember]
        public int codsegmento { get; set; }
        [DataMember]
        public string variable { get; set; }
        [DataMember]
        public string operador { get; set; }
        [DataMember]
        public string valor { get; set; }
        [DataMember]
        public string segundo_valor { get; set; }
        [DataMember]
        public string nombre_segmento { get; set; }
        [DataMember]
        public string nombre_criterio { get; set; }
        [DataMember]
        public string valor_historico { get; set; }
        [DataMember]
        public bool llevaMarcaCambio { get; set; }
    }

}