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
    public class AseEntitiesEjecutivo
    {
        #region columnasBaseDatos
        [DataMember]
        public Int64 IdEjecutivo { get; set; } //Consecutivo que identifica al asesor en el sistema 
        [DataMember]
        public Int64 IdUsuario { get; set; } //Corresponde al consecutivo de la tabla usuarios que es asignado al asesor  al ser registrado en el sistema  
        [DataMember]
        public string PrimerNombre { get; set; }//Corresponde al primer nombre del asesor o ejecutivo 
        [DataMember]
        public string SegundoNombre { get; set; }//Corresponde al segundo nombre del asesor o ejecutivo 
	    [DataMember]
        public string PrimerApellido { get; set; }//Corresponde al primer apellido del asesor o ejecutivo
        [DataMember]
        public string SegundoApellido { get; set;} //Corresponde al segundo apellido del asesor o ejecutivo
        [DataMember]
        public Int64 TipoDocumento { get; set; }//Corresponde al tipo de identificación del asesor o ejecutivo 
        [DataMember]
        public Int64 NumeroDocumento { get; set; }//Corresponde al número de identificacion del asesor o ejecutivo
        [DataMember]
        public string Direccion { get; set; }//Corresponde la dirección de residencia del asesor o ejecutivo
        [DataMember]
        public string Barrio { get; set; }//Corresponde al nombre del barrio donde reside el asesor o ejecutivo 
        [DataMember]
        public Int64 Telefono { get; set; }//Corresponde al número telefónico de la residencia del asesor o ejecutivo
        [DataMember]
        public string Email { get; set; }//Corresponde al correo electrónico del asesor o ejecutivo 
	    [DataMember]
        public DateTime FechaIngreso { get; set; }//Indica la fecha de ingreso del asesor o ejecutivo a la entidad.
        [DataMember]
        public string Oficina { get; set; }//Indica el código de la oficina a la que pertenece el aseor o ejecutivo, corresponde con las de la tabla oficinas del módulo de contabilidad
        #endregion

        [DataMember]
        public Stream stream { get; set; }


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
