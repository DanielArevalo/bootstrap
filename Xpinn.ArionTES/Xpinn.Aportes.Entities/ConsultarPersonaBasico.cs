using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class ConsultarPersonaBasico
    {
        [DataMember]
        public bool result { get; set; }
        [DataMember]
        public decimal idconsecutivo { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public string tipo_persona { get; set; }
        [DataMember]
        public Int64 tipo_identificacion { get; set; }
        [DataMember]
        public string primer_nombre { get; set; }
        [DataMember]
        public string segundo_nombre { get; set; }
        [DataMember]
        public string primer_apellido { get; set; }
        [DataMember]
        public string segundo_apellido { get; set; }
        [DataMember]
        public Int64 codciudadresidencia { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public Int64? ciudadempresa { get; set; }
        [DataMember]
        public string direccionempresa { get; set; }
        [DataMember]
        public string telefonoempresa { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string nomciudadresidencia { get; set; }
        [DataMember]
        public string nomciudadempresa { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public DateTime fecha_nacimiento { get; set; }
        [DataMember]
        public DateTime fechacreacion { get; set; }
        [DataMember]
        public string genero { get; set; }
        [DataMember]
        public Int64 codestadocivil { get; set; }
        [DataMember]
        public string clavesinencriptar { get; set; }
        [DataMember]
        public Int64 cod_zona { get; set; }
        [DataMember]
        public string razon_social { get; set; }
    }
}