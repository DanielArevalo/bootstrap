using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Seguridad.Entities
{
    /// <summary>
    /// Entidad Opcion
    /// </summary>
    [DataContract]
    [Serializable]
    public class Contenido
    {
        [DataMember] 
        public Int64 cod_opcion { get; set; }
        [DataMember] 
        public string nombre { get; set; }
        [DataMember]
        public string html { get; set; }
        [DataMember]         
        public Int64 mostrarOficina { get; set; }        
    }
}