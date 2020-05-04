using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class LibroAuxiliar
    {
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string nombrecuenta { get; set; }
        [DataMember]
        public string naturaleza { get; set; }
        [DataMember]
        public string depende_de { get; set; }
        [DataMember]
        public DateTime? fecha { get; set; }
        [DataMember]
        public Int64? num_comp { get; set; }
        [DataMember]
        public Int64? tipo_comp { get; set; }
        [DataMember]
        public string sop_egreso { get; set; }
        [DataMember]
        public string sop_ingreso { get; set; }
        [DataMember]
        public string detalle { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public Double valor { get; set; }
        [DataMember]
        public Double debito { get; set; }
        [DataMember]
        public Double credito { get; set; }
        [DataMember]
        public Double saldo { get; set; }
        [DataMember]
        public string concepto { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string identific_tercero { get; set; }
        [DataMember]
        public string nombre_tercero { get; set; }
        [DataMember]
        public Int64 centro_costo { get; set; }
        [DataMember]
        public string centro_gestion { get; set; }
        [DataMember]
        public string regimen { get; set; }
        [DataMember]
        public Int64 consecutivo { get; set; }


        //AGREGADO
        [DataMember]
        public decimal base_minima{ get; set; }
        [DataMember]
        public decimal porcentaje_impuesto { get; set; }
        [DataMember]
        public string num_sop { get; set; }

        [DataMember]
        public string observaciones { get; set; }
    }    
}
