using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Xpinn.Asesores.Entities
{
    public class AudAfiliacion
    { 
        #region columnasBaseDatos
        [DataMember]
        public Int64 IdEjecutivo { get; set; } //Corresponde iusuario que a su vez corresponde al código del usuario que afilio al cliente 
        [DataMember]
        public Int64 IdCliente { get; set; } //Corresponde icodigo que a su vez Es el código del cliente, que pertenece a campo del mismo nombre en la tabla asclientes  
        [DataMember]
        public DateTime FechaAfiliacion { get; set; }//Corresponde fafiliacion que a su vez Indica la fecha en que se registro el cliente en la entidad  
        [DataMember]
        public Int64 IdMotivoAfiliacion { get; set; } //Corresponde icodmotivo que a su vez Es el código del cliente, que pertenece a campo del mismo nombre en la tabla asclientes  
        [DataMember]
        public Int64 Observaciones { get; set; } //Corresponde sobservacion que a su vez es Corresponde a la observación registrada por el asesor al ingresar un cliente nuevo   
        #endregion

    }
}
