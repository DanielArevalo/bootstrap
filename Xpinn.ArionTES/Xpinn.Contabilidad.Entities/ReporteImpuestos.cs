using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class ReporteImpuestos
    {
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string ciudad { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public int num_comprobante { get; set; }
        [DataMember]
        public string tipo_comprobante { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string nombre_cuenta { get; set; }
        [DataMember]
        public decimal baseimp { get; set; }
        [DataMember]
        public decimal porcentaje { get; set; }
        [DataMember]
        public decimal valor { get; set; }

        //DATOS DE IMPUESTO
        [DataMember]
        public int cod_tipo_impuesto { get; set; }
        [DataMember]
        public string nombre_impuesto { get; set; }
        [DataMember]
        public string principal { get; set; }
        [DataMember]
        public string depende_de { get; set; }
    }    
}
