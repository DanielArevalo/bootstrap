using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Riesgo.Entities
{
    [DataContract]
    [Serializable]
    public class alertas_ries
    {
        [DataMember]
        public Int64 Cod_Alerta { get; set; }
        [DataMember]
        public String Nom_Alerta { get; set; }
        [DataMember]
        public String Descripcion { get; set; }
        [DataMember]
        public String Periocidad { get; set; }
        [DataMember]
        public String Sentencia_Sql { get; set; }
        [DataMember]
        public String Indicador { get; set; }
    }
}