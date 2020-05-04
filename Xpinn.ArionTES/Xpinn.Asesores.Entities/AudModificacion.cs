using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Xpinn.Asesores.Entities
{
    public class AudModificacion
    {
        #region columnasBaseDatos
        [DataMember]
        public Int64 IdEjecutivo { get; set; } //Corresponde iusuario que a su vez Indica el código de la persona que realizo la modificación de la información del cliente
        [DataMember]
        public Int64 IdCliente { get; set; } //Corresponde icodigo que a su vez es Es el código asignado al cliente potencial, corresponde con el existente en la tabla asclientes
        [DataMember]
        public DateTime FechaAfiliacion { get; set; }//Corresponde fmodificacion que a su vez Corresponde con la fecha en que se modifico la información de cliente 
        [DataMember]
        public Int64 IdMotivoModificacion { get; set; } //Corresponde icodmotivo que a su vez es Indica el código del motivo de modificación de la información del cliente  
        [DataMember]
        public Int64 Observaciones { get; set; } //Corresponde sobservacion que a su vez es Corresponde a la observación registrada por el asesor al modificar la información de un cliente    
        [DataMember]
        public Int64 IdVerificacionInfo { get; set; } //Corresponde iverificacion que a su vez es Permite determinar si se verifico la información del cliente que fue  modificada por un usuario. Es 0 al modificar el registro   
        #endregion
    }
}
