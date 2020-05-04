using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities
{
    [DataContract]
    [Serializable]
    public class Cliente
    {
        #region TablaCliente
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

        public string NombreYApellido
        {
            get
            {
                return string.Format("{0} {1}", PrimerNombre, PrimerApellido);
            }
        }

        [DataMember]
        public string TipoDocumento { get; set; }
        [DataMember]
        public string NumeroDocumento { get; set; }
        [DataMember]
        public string Direccion { get; set; }
        [DataMember]
        public string CodigoZona { get; set; }
        [DataMember]
        public string Telefono { get; set; }
        [DataMember]
        public string Barrio { get; set; }
        [DataMember]
        public string Ciudad { get; set; }
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
        public Int64 CodigoOficina { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public string tipo_cliente { get; set; }
        [DataMember]
        public string observacion { get; set; }


        #endregion TablaCliente

        #region TablaRelacionadas
        [DataMember]
        public string NombreTipoIdentificacion { get; set; }
        [DataMember]
        public string NombreActividad { get; set; }
        [DataMember]
        public string NombreZona { get; set; }
        [DataMember]
        public string NombreOficina { get; set; }
        [DataMember]

        public MotivoAfiliacion MotivoAfiliacion { get; set; }
        [DataMember]
        public MotivoModificacion MotivoModificacion { get; set; }

        #endregion TablaRelacionadas

        #region Seguridad
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
        #endregion Seguridad

        #region TablaAsclientes
        [DataMember]
        public string NombreCompleto { get; set; }
        [DataMember]
        public Int64 Calificacion { get; set; }
        #endregion TablaAsclientes

        #region Asesor
        [DataMember]
        public Int64 cod_asesor { get; set; }
        [DataMember]
        public string NombreAsesor { get; set; }
        #endregion Asesor


        #region Persona
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public Int64 cod_zona { get; set; }
        [DataMember]
        public string nomciudad { get; set; }
        [DataMember]
        public string latitud { get; set; }
        [DataMember]
        public string longitud { get; set; }
        #endregion Persona
    }
}