using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Seguridad.Entities
{
    /// <summary>
    /// Entidad Proceso
    /// </summary>
    [DataContract]
    [Serializable]
    public class Proceso
    {
        [DataMember] 
        public Int64 cod_proceso { get; set; }
        [DataMember] 
        public Int64 cod_modulo { get; set; }
        [DataMember] 
        public string nombre { get; set; }

    }
}