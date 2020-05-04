using System;
using System.Runtime.Serialization;

namespace Xpinn.Sincronizacion.Entities
{
    [DataContract]
    [Serializable]
    public class Persona
    {
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public string tipo_persona { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public int? digito_verificacion { get; set; }
        [DataMember]
        public int codtipoidentificacion { get; set; }
        [DataMember]
        public DateTime? fechaexpedicion { get; set; }
        [DataMember]
        public Int64? codciudadexpedicion { get; set; }
        [DataMember]
        public string sexo { get; set; }
        [DataMember]
        public string primer_nombre { get; set; }
        [DataMember]
        public string segundo_nombre { get; set; }
        [DataMember]
        public string primer_apellido { get; set; }
        [DataMember]
        public string segundo_apellido { get; set; }
        [DataMember]
        public string razon_social { get; set; }
        [DataMember]
        public DateTime? fechanacimiento { get; set; }
        [DataMember]
        public Int64? codciudadnacimiento { get; set; }
        [DataMember]
        public int? codestadocivil { get; set; }
        [DataMember]
        public int? codescolaridad { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public Int64? codciudadresidencia { get; set; }
        [DataMember]
        public string celular { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string empresa { get; set; }
        [DataMember]
        public string telefonoempresa { get; set; }
        [DataMember]
        public int? codtipocontrato { get; set; }
        [DataMember]
        public Int64? cod_oficina { get; set; }
        [DataMember]
        public Int64? cod_usuario { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public Int64? cod_zona { get; set; }
        [DataMember]
        public int? estrato { get; set; }        
        [DataMember]
        public DateTime? fechacreacion { get; set; }
        [DataMember]
        public string usuariocreacion { get; set; }
        [DataMember]
        public DateTime? fecultmod { get; set; }
        [DataMember]
        public string usuultmod { get; set; }
        [DataMember]
        public int es_afiliado { get; set; }
    }
}
