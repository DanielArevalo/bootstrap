using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class Inactividades
    {
        [DataMember]
        public long consecutivo { get; set; }
        [DataMember]
        public Int64 codigoempleado { get; set; }
        [DataMember]
        public Int64 codigopersona { get; set; }
        [DataMember]
        public int? tipo { get; set; }
        [DataMember]
        public int? clase { get; set; }

        [DataMember]
        public long? cod_concepto { get; set; }

        [DataMember]
        public DateTime? fechainicio { get; set; }
        [DataMember]
        public DateTime? fechaterminacion { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int? remunerada { get; set; }
        [DataMember]
        public long? codigotipocontrato { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string tipo_identificacion { get; set; }
        [DataMember]
        public string nombre_empleado { get; set; }
        [DataMember]
        public long? codigo_tipo_nomina { get; set; }
        [DataMember]
        public string desc_inactividad_remunerada { get; set; }
        [DataMember]
        public int dias { get; set; }
        [DataMember]
        public string desc_clase { get; set; }
        [DataMember]
        public string desc_tipo { get; set; }
        [DataMember]
        public string desc_contrato { get; set; }
        [DataMember]
        public string desc_nomina { get; set; }

        [DataMember]
        public int? aplicada { get; set; }
    }
}