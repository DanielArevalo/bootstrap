using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Seguridad.Entities
{
    /// <summary>
    /// Entidad Modulo
    /// </summary>
    [DataContract]
    [Serializable]
    public class Modulo
    {
        [DataMember] 
        public Int64 cod_modulo { get; set; }
        [DataMember] 
        public string nom_modulo { get; set; }
    }
}