using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Seguridad.Entities
{
    /// <summary>
    /// Entidad Acceso
    /// </summary>
    [DataContract]
    [Serializable]
    public class PersonaUsuario
    {
        [DataMember]
        public Int64 idacceso { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public string clave { get; set; }
        [DataMember]
        public DateTime fecha_creacion { get; set; }
        [DataMember]
        public DateTime fecultmod { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string apellidos { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public DateTime fechanacimiento { get; set; }
        [DataMember]
        public string clave_sinencriptar { get; set; }
        [DataMember]
        public Boolean rpta { get; set; }
        [DataMember]
        public string mensaje { get; set; }
        [DataMember]
        public string identificacion { get; set; }
    }
}