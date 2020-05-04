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
    public class Usuario_Link
    {
        [DataMember]
        public Int64 idlink { get; set; }
        [DataMember]
        public int codusuario { get; set; }
        [DataMember]
        public int cod_opcion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string ruta { get; set; }
    }
}
