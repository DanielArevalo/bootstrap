using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Integracion.Entities
{
    [DataContract]
    [Serializable]
    public class Notificacion
    {
        [DataMember]
        public string descripcion { get; set; }

        [DataMember]
        public long cod_persona { get; set; }

        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string mensaje { get; set; }

        [DataMember]
        public string email { get; set; }

        [DataMember]
        public bool enviar_mensaje { get; set; }

        [DataMember]
        public bool enviar_email { get; set; }

        [DataMember]
        public string celular { get; set; }

        [DataMember]
        public string correo { get; set; }

        [DataMember]
        public string correo_texto { get; set; }

        [DataMember]
        public string mensaje_texto { get; set; }

        [DataMember]
        public string correo_parametros { get; set; }

        [DataMember]
        public string mensaje_parametros { get; set; }

        [DataMember]
        public string key { get; set; }

        [DataMember]
        public string client { get; set; }

        [DataMember]
        public string enviador_email { get; set; }

        [DataMember]
        public string enviador_clave { get; set; }

        [DataMember]
        public string error { get; set; }

        [DataMember]
        public int cod_mensaje { get; set; }

        [DataMember]
        public int cod_email { get; set; }

        [DataMember]
        public int clave_dinamica { get; set; }
    }
}