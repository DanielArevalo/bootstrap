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
    public class Estado
    {
        [DataMember]
        public Int64 IdEstado { get; set; } //Corresponde al consecutivo de la tabla usuarios que es asignado al asesor  al ser registrado en el sistema  
        [DataMember]
        public string Descripcion { get; set; }//Corresponde al primer nombre del asesor o ejecutivo 
    }
}
