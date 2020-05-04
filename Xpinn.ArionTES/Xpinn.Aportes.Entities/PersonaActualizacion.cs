using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class PersonaActualizacion
    {
        [DataMember]
        public decimal idconsecutivo { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public string primer_nombre { get; set; }
        [DataMember]
        public string segundo_nombre { get; set; }
        public string Nombres
        {
            get { return string.Format("{0} {1}", primer_nombre, segundo_nombre); }
        }
        [DataMember]
        public string primer_apellido { get; set; }
        [DataMember]
        public string segundo_apellido { get; set; }
        public string Apellidos
        {
            get { return string.Format("{0} {1}", primer_apellido, segundo_apellido); }
        }
        [DataMember]
        public Int64 codciudadresidencia { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string celular { get; set; }
        [DataMember]
        public int? ciudadempresa { get; set; }
        [DataMember]
        public string direccionempresa { get; set; }
        [DataMember]
        public string telefonoempresa { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public int? estado { get; set; }
        [DataMember]
        public string nomciudadresidencia { get; set; }
        [DataMember]
        public string nomciudadempresa { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string cod_nomina { get; set; }
        [DataMember]
        public DateTime fecha_solicitud { get; set; }
        [DataMember]
        public DateTime fecha_aprobacion { get; set; }
        [DataMember]
        public DateTime fecultmod { get; set; }
        [DataMember]
        public string usuultmod { get; set; }
        [DataMember]
        public string asesor { get; set; }

    }
}