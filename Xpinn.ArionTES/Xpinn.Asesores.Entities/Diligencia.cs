using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities
{
    /// <summary>
    /// Entidad Diligencia
    /// </summary>
    [DataContract]
    [Serializable]
    public class Diligencia

    {
        [DataMember]
        public Int64 codigo_deudor { get; set; }
        [DataMember]
        public Int64 identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public Int64 codigo_diligencia { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public DateTime fecha_diligencia { get; set; }
        [DataMember]
        public string tipo_diligencia_consulta { get; set; }
        [DataMember]
        public string tipo_contacto_consulta { get; set; }
        [DataMember]
     
        public Int64 tipo_diligencia{ get; set; }
        [DataMember]
        public Int64 tipo_contacto { get; set; }
        [DataMember]
        public string atendio { get; set; }
        [DataMember]
        public string respuesta { get; set; }
        [DataMember]
        public Int64 acuerdo { get; set; }
        [DataMember]
        public string acuerdo_consulta { get; set; }
        [DataMember]
        public DateTime fecha_acuerdo { get; set; }
        [DataMember]
        public Int64 valor_acuerdo { get; set; }
        [DataMember]
        public string anexo { get; set; }
        [DataMember]
        public string observacion { get; set; }
        [DataMember]
        public Int64 codigo_usuario_regis { get; set; }
        [DataMember]
        public Int64 codigo_parametro { get; set; }

        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public DateTime fecha_Citacion { get; set; }

        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string identificaciones { get; set; }
        [DataMember]
        public string Num_Celular { get; set; }
        
    }
}