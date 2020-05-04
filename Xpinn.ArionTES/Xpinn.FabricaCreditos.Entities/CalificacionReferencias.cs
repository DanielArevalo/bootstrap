using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad CalificacionReferencias
    /// </summary>
    [DataContract]
    [Serializable]
    public class CalificacionReferencias
    {
        [DataMember] 
        public Int64 tipocalificacionref { get; set; }
        [DataMember] 
        public string nombre { get; set; }

    }
}