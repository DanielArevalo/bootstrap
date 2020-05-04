using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class PersonaBiometria
    {
        [DataMember]
        public Int64 idbiometria { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string apellidos { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public int? numero_dedo { get; set; }
        [DataMember]
        public byte[] huella { get; set; }              // Aqui se guarda el template de la huella para validar
        [DataMember]
        public byte[] huella_secugen { get; set; }      // Aqui se guarda el template de la huella para validar en secugen
        [DataMember]
        public string template_huella { get; set; }     // Aqui se guarda el template de la huella convertido en string
        [DataMember]
        public byte[] imagen { get; set; }              // Aqui se guarda la imagen de la huella.
        [DataMember]
        public Int64? codusuario { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
    }
}