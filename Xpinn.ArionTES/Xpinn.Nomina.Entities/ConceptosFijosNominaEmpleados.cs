using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class ConceptosFijosNominaEmpleados
    {
        [DataMember]
        public long consecutivo { get; set; }
        [DataMember]
        public long codigoempleado { get; set; }
        [DataMember]
        public long codigoconcepto { get; set; }
        [DataMember]
        public string descripcion_concepto { get; set; }
        [DataMember]
        public long codigoIngresoPersonal { get; set; }
        [DataMember]
        public DateTime Fechafin { get; set; }
       
    }
}