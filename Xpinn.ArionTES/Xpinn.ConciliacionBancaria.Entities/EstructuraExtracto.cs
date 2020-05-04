using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.ConciliacionBancaria.Entities
{
    [DataContract]
    [Serializable]
    public class EstructuraExtracto
    {
        [DataMember]
        public int idestructuraextracto { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public int cod_banco { get; set; }
        [DataMember]
        public int estado { get; set; }
        [DataMember]
        public int tipo_archivo { get; set; }
        [DataMember]
        public string delimitador { get; set; }
        [DataMember]
        public int encabezado { get; set; }
        [DataMember]
        public int totales { get; set; }
        [DataMember]
        public string calificador { get; set; }
        [DataMember]
        public string separador_decimal { get; set; }
        [DataMember]
        public string separador_miles { get; set; }
        [DataMember]
        public string formato_fecha { get; set; }
        [DataMember]
        public List<DetEstructuraExtracto> lstDetEstructura { get; set; }


        [DataMember]
        public string nom_banco { get; set; }
        [DataMember]
        public string nom_tipoarchivo { get; set; }
    }
}