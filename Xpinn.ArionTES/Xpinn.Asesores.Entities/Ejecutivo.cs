using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Xpinn.Asesores.Entities
{
    [DataContract]
    [Serializable]
    public class Ejecutivo
    {
        #region columnasBaseDatos
        [DataMember]
        public Int64 IdEjecutivo { get; set; } //Corresponde al campo (icodigo) que identifica al asesor en el sistema 
        [DataMember]
        public Int64 IdUsuario { get; set; } //Corresponde al campo (iusuario) consecutivo de la tabla usuarios que es asignado al asesor  al ser registrado en el sistema  
        [DataMember]
        public string PrimerNombre { get; set; }//Corresponde al campo (snombre1) primer nombre del asesor o ejecutivo 
        [DataMember]
        public string SegundoNombre { get; set; }//Corresponde al campo (snombre2) segundo nombre del asesor o ejecutivo 
        [DataMember]
        public string PrimerApellido { get; set; }//Corresponde al campo (sapellido1) primer apellido del asesor o ejecutivo
        [DataMember]
        public string SegundoApellido { get; set; } //Corresponde al campo (sapellido2) segundo apellido del asesor o ejecutivo
        [DataMember]
        public Int64 TipoDocumento { get; set; }//Corresponde al campo (itipoiden) tipo de identificación del asesor o ejecutivo 
        [DataMember]
        public string NumeroDocumento { get; set; }//Corresponde al campo (sidentificacion) número de identificacion del asesor o ejecutivo
        [DataMember]
        public string NombreTipoDocumento { get; set; } //Corresponde al campo (nombIdentificacion) 
        [DataMember]
        public string Direccion { get; set; }//Corresponde al campo (sdireccion) dirección de residencia del asesor o ejecutivo
        [DataMember]
        public string Barrio { get; set; }//Corresponde al campo (sbarrio) nombre del barrio donde reside el asesor o ejecutivo 
        [DataMember]
        public Int64 Telefono { get; set; }//Corresponde al campo (stelefono) telefónico de la residencia del asesor o ejecutivo
        [DataMember]
        public Int64 TelefonoCel { get; set; }//Corresponde al campo (stelefono) telefónico de la residencia del asesor o ejecutivo
        [DataMember]
        public string Codigo { get; set; }//Corresponde al campo (semail) correo electrónico del asesor o ejecutivo 
        [DataMember]
        public Int64 IdZona { get; set; }//Corresponde al campo (semail) correo electrónico del asesor o ejecutivo 
        [DataMember]
        public string NombreZona { get; set; }//Corresponde al campo (semail) correo electrónico del asesor o ejecutivo 
        [DataMember]
        public string Email { get; set; }//Corresponde al campo (semail) correo electrónico del asesor o ejecutivo 
        [DataMember]
        public Int64 IdOficina { get; set; }//Corresponde al campo (ioficina) correo electrónico del asesor o ejecutivo 
        [DataMember]
        public string NombreOficina { get; set; }//Corresponde al campo (nombOficina)
        [DataMember]
        public Int64 IdEstado { get; set; }//Corresponde al campo (iestado) correo electrónico del asesor o ejecutivo 
        [DataMember]
        public string NombreEstado { get; set; }//Corresponde al campo (nombEstado)
        [DataMember]
        public string latitud { get; set; }//Corresponde al campo (nombEstado)
        [DataMember]
        public string longitud { get; set; }//Corresponde al campo (nombEstado)
        [DataMember]
        public DateTime FechaIngreso { get; set; }//Corresponde al campo (fingreso) Indica la fecha de ingreso del asesor o ejecutivo a la entidad.
        #endregion

        [DataMember]
        public string NombreCompleto { get; set; }
        [DataMember]
        public Int64 icodciudad { get; set; }
        [DataMember]
        public string nomciudad { get; set; }
        [DataMember]
        public Int64 IOficina { get; set; }
        [DataMember]
        public Int64 icodzona { get; set; }
        [DataMember]
        public Int64 icodigo { get; set; }

        
        [DataMember]
        public string Emaildir { get; set; }//Corresponde al campo (email) correo electrónico del director 
        [DataMember]
        public Int64 IdDirector { get; set; }//Corresponde al campo (codigo) correo electrónico del director 
    
        #region Seguridad
        [DataMember]
        public string CodigoPrograma { get; set; }
        [DataMember]
        public string GeneraLog { get; set; }
        [DataMember]
        public string UsuarioCrea { get; set; }
        [DataMember]
        public DateTime FechaCrea { get; set; }
        [DataMember]
        public string UsuarioEdita { get; set; }
        [DataMember]
        public DateTime FechaEdita { get; set; }
        #endregion

    }
}
