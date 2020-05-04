using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad UsuarioAtribuciones
    /// </summary>
    [DataContract]
    [Serializable]
    public class UsuarioAtribuciones
    {
        [DataMember] 
        public Int64 codusuario { get; set; }
        [DataMember] 
        public Int64 tipoatribucion { get; set; }
        [DataMember] 
        public Int64 activo { get; set; }
                
    }
}