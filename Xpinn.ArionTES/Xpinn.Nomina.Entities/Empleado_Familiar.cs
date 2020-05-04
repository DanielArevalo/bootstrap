using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class Empleado_Familiar
    {
    

        [DataMember]
        public Int64 consecutivo { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public int codigoempleado { get; set; }
        [DataMember]
        public string parentezco { get; set; }
        [DataMember]
        public string identificacionfamiliar { get; set; }
        [DataMember]
        public string tipoidentificacion { get; set; }
        [DataMember]
        public string nombrefamiliar { get; set; }
        [DataMember]
        public string profesion { get; set; }
        [DataMember]
        public string convivefamiliar { get; set; }

    }
}

