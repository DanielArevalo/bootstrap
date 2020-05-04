using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.NIIF.Entities
{
    [DataContract]
    [Serializable]
    public class MatrizRiesgoNIF
    {
        [DataMember]
        public int idmatriz { get; set; }
        [DataMember]
        public Int64? cod_clasifica { get; set; }
        [DataMember]
        public string nom_clasifica { get; set; }
        [DataMember]
        public string tipo_persona { get; set; }
        [DataMember]
        public string nom_tipo_persona { get; set; }
        [DataMember]
        public DateTime fechacreacion { get; set; }
        [DataMember]
        public Int64 usuariocreacion { get; set; }
        [DataMember]
        public DateTime fecultmod { get; set; }
        [DataMember]
        public Int64? usuultmod { get; set; }

        [DataMember]
        public string filtro { get; set; }
    }

    [DataContract]
    [Serializable]
    public class Clasificacion
    {
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public Int64 Codigo { get; set; }
    }

    [DataContract]
    [Serializable]
    public class Parametro
    {

        //Tabla scparametro
        [DataMember]
        public Int64 idparametro { get; set; }
        [DataMember]
        public String tipo { get; set; }
        [DataMember]
        public String nombre { get; set; }
        [DataMember]
        public Int64 idvariable { get; set; }
        [DataMember]
        public String formula { get; set; }
        [DataMember]
        public String sentencia { get; set; }
        [DataMember]
        public String campo { get; set; }
    }

}