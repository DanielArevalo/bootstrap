using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class NominaEmpleado
    {
        [DataMember]
        public int consecutivo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public long? codigotipocontrato { get; set; }
        [DataMember]
        public long? tiponomina { get; set; }
        [DataMember]
        public long? codigooficina { get; set; }
        [DataMember]
        public string desc_oficina { get; set; }
        [DataMember]
        public string direccion_oficina { get; set; }
        [DataMember]
        public string nombre_empleado { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string desc_tipo_contrato { get; set; }
        [DataMember]
        public string desc_tipo_nomina { get; set; }
        [DataMember]
        public long? codigociudad { get; set; }
        [DataMember]
        public int permite_anticipos { get; set; }

        [DataMember]
        public int permite_anticipos_sub_trans { get; set; }
        [DataMember]
        public string permite_anticipos_mostrar { get; set; }

        [DataMember]
        public string permite_anticipos_sub_mostrar { get; set; }
        [DataMember]
        public long? porcentaje_anticipos { get; set; }
        [DataMember]
        public long? porcentaje_anticipos_sub_trans { get; set; }

        [DataMember]
        public DateTime fecha_ult_liquidacion { get; set; }

        [DataMember]
        public long? periodicidad_anticipos { get; set; }
    }
}