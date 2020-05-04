using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Xpinn.Reporteador.Entities
{
   public class TransaccionEfectivo
    {
        [DataMember]
        public Int64 consecutivo { get; set; }
        [DataMember]
        public DateTime fecha_tran { get; set; }
        [DataMember]
        public Int64 valor_tran { get; set; }
        [DataMember]
        public string tipo_moneda { get; set; }
        [DataMember]
        public string cod_oficina { get; set; }
        [DataMember]
        public string cod_ciudad { get; set; }
        [DataMember]
        public string tipo_producto { get; set; }
        [DataMember]
        public string tipo_tran { get; set; }
        [DataMember]
        public string num_producto { get; set; }
        [DataMember]
        public string tipo_identificacion1 { get; set; }
        [DataMember]
        public string identificacion1 { get; set; }
        [DataMember]
        public string primer_apellido1 { get; set; }
        [DataMember]
        public string segundo_apellido1 { get; set; }
        [DataMember]
        public string primer_nombre1 { get; set; }
        [DataMember]
        public string segundo_nombre1 { get; set; }
        [DataMember]
        public string razon_social1 { get; set; }
        [DataMember]
        public string actividad_economica { get; set; }
        [DataMember]
        public string ingresos { get; set; }
        [DataMember]
        public string tipo_identificacion2 { get; set; }
        [DataMember]
        public string identificacion2 { get; set; }
        [DataMember]
        public string primer_apellido2 { get; set; }
        [DataMember]
        public string segundo_apellido2 { get; set; }
        [DataMember]
        public string primer_nombre2 { get; set; }
        [DataMember]
        public string segundo_nombre2 { get; set; }
        [DataMember]
        public string separador { get; set; }
    }
}
