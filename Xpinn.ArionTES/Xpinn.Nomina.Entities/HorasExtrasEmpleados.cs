using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class HorasExtrasEmpleados
    {
        [DataMember]
        public long consecutivo { get; set; }
        [DataMember]
        public long codigoempleado { get; set; }
        [DataMember]
        public long codigopersona { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public decimal cantidadhoras { get; set; }
        [DataMember]
        public long codigoconceptohoras { get; set; }
        [DataMember]
        public long codigonomina { get; set; }
        [DataMember]
        public string nombre_empleado { get; set; }
        [DataMember]
        public string identificacion_empleado { get; set; }
        [DataMember]
        public string desc_concepto_hora { get; set; }
        [DataMember]
        public string desc_nomina { get; set; }
        [DataMember]
        public string tipo_identificacion { get; set; }

        [DataMember]
        public long? pagadas { get; set; }
    }
}