using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities
{
    [DataContract]
    [Serializable]
    public class EnvioDeCorreoExtracto
    {

        [DataMember]
        public string NombreCorreoOrigen { get; set; }
        [DataMember]
        public string ServidorSMTP { get; set; }
        [DataMember]
        public UInt16 PuertoDeServidorSMTP { get; set; }
        [DataMember]
        public bool UsarSSL { get; set; }
        [DataMember]
        public string Usuario { get; set; }
        [DataMember]
        public string Clave { get; set; }
        [DataMember]
        public string TextoDelAsunto { get; set; }
        [DataMember]
        public string TextoDelMensaje { get; set; }

    }
}