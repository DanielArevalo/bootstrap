using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Icetex.Entities
{
    [DataContract]
    [Serializable]
    public class CreditoIcetex
    {
        [DataMember]
        public Int64 numero_credito { get; set; }
        [DataMember]
        public int cod_convocatoria { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public string identific_asoc { get; set; }
        [DataMember]
        public string nom_asoc { get; set; }
        [DataMember]
        public DateTime fecha_solicitud { get; set; }
        [DataMember]
        public string tipo_beneficiario { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public int? codtipoidentificacion { get; set; }
        [DataMember]
        public string primer_nombre { get; set; }
        [DataMember]
        public string segundo_nombre { get; set; }
        [DataMember]
        public string primer_apellido { get; set; }
        [DataMember]
        public string segundo_apellido { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public int? estrato { get; set; }
        [DataMember]
        public string cod_universidad { get; set; }
        [DataMember]
        public string cod_programa { get; set; }
        [DataMember]
        public int tipo_programa { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public decimal? periodos { get; set; }
        [DataMember]
        public string estado { get; set; }

        //ADICIONADO
        [DataMember]
        public string nom_tipo_beneficiario { get; set; }
        [DataMember]
        public string nom_tipoidentificacion { get; set; }
        [DataMember]
        public string nom_tipo_programa { get; set; }
        [DataMember]
        public string nom_universidad { get; set; }
        [DataMember]
        public string nom_programa_univ { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string nom_estado { get; set; }
        [DataMember]
        public string observacion { get; set; }
        [DataMember]
        public DateTime fecha_inscripcion { get; set; }
        [DataMember]
        public int esconforme { get; set; }
        [DataMember]
        public int tipo_aprobacion { get; set; }
    }
}
