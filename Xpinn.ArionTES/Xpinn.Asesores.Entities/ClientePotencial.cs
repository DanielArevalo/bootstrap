using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Xpinn.Asesores.Entities.Common;

namespace Xpinn.Asesores.Entities
{
    [DataContract]
    [Serializable]
    public class ClientePotencial
    {
        public ClientePotencial()
        {
            Zona = new Zona();
            Actividad = new Xpinn.Asesores.Entities.Common.Actividad();
            TipoIdentificacion = new TipoIdentificacion();
            MotivoAfiliacion = new MotivoAfiliacion();
            MotivoModificacion = new MotivoModificacion();
        }

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

        [DataMember]
        public TipoIdentificacion TipoIdentificacion { get; set; }
        /*[DataMember]
        public string TipoDocumento { get; set; } 
         */

        [DataMember]
        public Int64 NumeroDocumento { get; set; }
        [DataMember]
        public string Direccion { get; set; }
        /*[DataMember]
        public string CodigoZona { get; set; }*/
        [DataMember]
        public Zona Zona { get; set; }


        [DataMember]
        public string Telefono { get; set; }
        [DataMember]
        public Int64 codasesor { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public DateTime FechaRegistro { get; set; }
        [DataMember]
        public string RazonSocial { get; set; }
        [DataMember]
        public string SiglaNegocio { get; set; }
        [DataMember]
        public Int64 Limpiar { get; set; }

        /*[DataMember]
        public Int64 CodigoActividad { get; set; }*/
        [DataMember]
        public Xpinn.Asesores.Entities.Common.Actividad Actividad { get; set; }


        #endregion TablaCliente

        #region TablaRelacionadas
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
         #region TablaUsuarios
        [DataMember]
        
        public Int64 IdUsuario { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Identificacion { get; set; }
         #endregion TablaUsuarios
    }
}
