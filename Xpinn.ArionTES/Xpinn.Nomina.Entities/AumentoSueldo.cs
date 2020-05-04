using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class AumentoSueldo
    {
        [DataMember]
        public long consecutivo { get; set; }
        [DataMember]
        public Int64 codigopersona { get; set; }
        [DataMember]
        public long codigoempleado { get; set; }
        [DataMember]
        public decimal? nuevosueldo { get; set; }
        [DataMember]
        public decimal? valorparaaumentar { get; set; }
        [DataMember]
        public decimal? porcentajeaumentar { get; set; }
        [DataMember]
        public DateTime? fecha { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre_empleado { get; set; }
        [DataMember]
        public decimal? sueldoanterior { get; set; }
        [DataMember]
        public decimal? sueldo { get; set; }
        [DataMember]
        public string tipo_identificacion { get; set; }
        [DataMember]
        public long consecutivo_empleado { get; set; }
        [DataMember]
        public long codigonomina { get; set; }
        [DataMember]
        public long codigotipocontrato { get; set; }

        [DataMember]
        public List<AumentoSueldo> lista { get; set; }
    }
}