using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities
{
    [DataContract]
    [Serializable]
    public class AseEntitiesCliente
    {
        [DataMember]
        public Int64 IdCliente { get; set; }
        [DataMember]
        public string PrimerNombre { get; set; }
        [DataMember]
        public string SegundoNombre { get; set; }
        [DataMember]
        public string PrimerApellido { get; set; }
        [DataMember]
        public string SegundoApellido { get; set; }
        [DataMember]
        public Int64 TipoDocumento { get; set; }
        [DataMember]
        public Int64 NumeroDocumento { get; set; }
        [DataMember]
        public string Direccion { get; set; }
        [DataMember]
        public string Zona { get; set; }
        [DataMember]
        public Int64 Telefono { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public DateTime FechaRegistro { get; set; }
        [DataMember]
        public string RazonSocial { get; set; }
        [DataMember]
        public string SiglaNegocio { get; set; }
        [DataMember]
        public Int64 CodigoActividad { get; set; }

        [DataMember]
        public string UsuarioCrea { get; set; }
        [DataMember]
        public DateTime FechaCrea { get; set; }
        [DataMember]
        public string UsuarioEdita { get; set; }
        [DataMember]
        public DateTime FechaEdita { get; set; }
        [DataMember]
        public string CodigoPrograma { get; set; }
        [DataMember]
        public string GeneraLog { get; set; }

    }
}
