using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class Retroactivo
    {
        [DataMember]
        public long consecutivo { get; set; }
        [DataMember]
        public Int64 codigopersona { get; set; }
        [DataMember]
        public long codigoempleado { get; set; }
        [DataMember]
        public decimal? valor { get; set; }
        [DataMember]
        public long? codigocentrocosto { get; set; }
        [DataMember]
        public DateTime? fechainicio { get; set; }
        [DataMember]
        public DateTime? fechafinal { get; set; }
        [DataMember]
        public DateTime? fechapago { get; set; }
        [DataMember]
        public int? numeropagos { get; set; }
        [DataMember]
        public int? periodicidad { get; set; }
        [DataMember]
        public int? conceptopagoretroactivo { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre_empleado { get; set; }
        [DataMember]
        public string desc_nomina { get; set; }
        [DataMember]
        public string desc_periodo { get; set; }
        [DataMember]
        public string desc_concepto { get; set; }
        [DataMember]
        public string tipo_identificacion { get; set; }
        [DataMember]
        public long? codigo_tipo_nomina { get; set; }
    }
}