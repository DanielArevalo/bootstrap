using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class PagosDescuentosFijos
    {
        [DataMember]
        public long consecutivo { get; set; }
        [DataMember]
        public Int64 codigoempleado { get; set; }
        [DataMember]
        public Int64 codigopersona { get; set; }
        [DataMember]
        public long? codigotiponomina { get; set; }
        [DataMember]
        public decimal? valorcuota { get; set; }
        [DataMember]
        public decimal? valortotal { get; set; }
        [DataMember]
        public decimal? acumulado { get; set; }
        [DataMember]
        public DateTime? fecha { get; set; }
        [DataMember]
        public long? controlsaldos { get; set; }
        [DataMember]
        public int? liquidapagodefinitiva { get; set; }
        [DataMember]
        public int? liquidapagoperiodica { get; set; }
        [DataMember]
        public int? descuentoperiocidad { get; set; }
        [DataMember]
        public int? codigoconceptonomina { get; set; }
        [DataMember]
        public long? codigocentrocostos { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre_empleado { get; set; }

        [DataMember]
        public string nombre_tercero { get; set; }
        [DataMember]
        public string motivos { get; set; }
        [DataMember]
        public string tipo_identificacion { get; set; }
        [DataMember]
        public string desc_centro_costo { get; set; }
        [DataMember]
        public string desc_concepto_nomina { get; set; }
        [DataMember]
        public string desc_nomina { get; set; }
        [DataMember]
        public string desc_descuento_periocidad { get; set; }

        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public Int64 clase { get; set; }


        [DataMember]
        public long tipo { get; set; }


        [DataMember]
        public long? cod_proveedor { get; set; }

        [DataMember]
        public string identificacion_tercero { get; set; }
    }
}
